using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Orders;
using Afrimart.Service.Contracts;
using System.Linq;
using Afrimart.DataAccess;
using Afrimart.Dto;

namespace Afrimart.Service.Implementations
{
    public class OrderService: IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IShopperProfileService _shopperProfileService;
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _uow;

        public OrderService(ICartService cartService, IShopperProfileService shopperProfileService, IPaymentService paymentService, IUnitOfWork uow)
        {
            _cartService = cartService;
            _shopperProfileService = shopperProfileService;
            _paymentService = paymentService;
            _uow = uow;
        }
        public async Task<Tuple<bool, string>> CreateOrder(CreateOrderRequestDto orderRequest, string email)
        {

            // confirm the user has a cart with the same cart id we have
            // pull the cart items
            // create the order
            // verify every other things
            // charge payment
            // on success
            // create new order record for user
            // create new payment record
            // for each product, increment sales count and earnings.
            // maybe for each store, increment the sales count and earning? or pull that from the prod table always?

            var cart = _cartService.GetCart(orderRequest.CartId);
            if (cart == null || !cart.CartItems.Any())
            {
                return new Tuple<bool, string>(false, "cart items not found");
            }

            var lineItems = new List<LineItem>();
            foreach (var cartItem in cart.CartItems)
            {
                lineItems.Add(new LineItem()
                {
                    Product = cartItem.Product,
                    Quantity = cartItem.Quantity,
                    SoldPricePerQuantity = cartItem.Product.SellingPrice
                });
            }

            var subTotal = lineItems.Sum(l => l.Product.SellingPrice);
            var tax = subTotal * AfrimartConstants.TEXAS_TAX_PERCENTAGE;
            var shippingCost = orderRequest.ShippingMethod == ShippingMethod.StorePickup
                ? 0
                : AfrimartConstants.MINIMUM_STANDARD_SHIPPING_COST;

            var totalCost = subTotal + tax + shippingCost;
            if (totalCost != orderRequest.TotalCost)
            {
                // Todo: email a support person immediately
                return new Tuple<bool, string>(false,
                    "An error occurred with your cart Items, we're currently looking into this issue");
            }

            var orderIdentifier = "";
            
            // call payment gateway
            var paymentSubmitted = _paymentService.ChargePayment(orderRequest.CardDetails, email, orderIdentifier);
            if (paymentSubmitted == false)
            {
                return new Tuple<bool, string>(false, "Payment declined! Please contact your bank");
            }

            // IF WE GOT THIS FAR, PAYMENT WAS SUCCESSFUL
            var paymentAudit = new OrderPaymentAudit()
            {
                PayerEmail = email,
                OrderIdentifier = orderIdentifier,
                AmountPaid = totalCost,
                TransactionFee = 0, // Todo: fill this in
                PaymentReference = "" // Todo: fill this in
            };

            var address = _shopperProfileService.GetUserAddresses(email).First(x => x.AddressType == AddressType.Shipping);
            var order = new Order()
            {
                Identifier = orderIdentifier, 
                LineItems = lineItems,
                SubTotal = subTotal,
                ShippingMethod = orderRequest.ShippingMethod,
                ShippingCost = shippingCost,
                Tax = tax,
                Status = OrderStatus.InProgress,
                TotalAmountPaid = totalCost,
                ShippedToPersonName = $"{address.FirstName} {address.LastName}",
                ShippedToAddress = $"{address.AddressLine1} {address.AddressLine2}, {address.City}, {address.State}, {address.ZipCode}"
            };
            // Todo: save order to db

            // Todo: save payment to db

            return new Tuple<bool, string>(true, "Success");
        }
    }

}
