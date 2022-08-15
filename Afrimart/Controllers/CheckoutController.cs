using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.Dto;
using Afrimart.Dto.Addresses;
using Afrimart.Dto.Carts;
using Afrimart.Services;
using Afrimart.ViewModels.Addresses;
using Afrimart.ViewModels.Orders;
using Afrimart.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using ServiceHelper.Requests;

namespace Afrimart.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IRequestManager _requestManager;
        private readonly ISessionService _sessionService;

        public CheckoutController(IAfrimartAuthorizationService authorizationService, IRequestManager requestManager, ISessionService sessionService)
        {
            _authorizationService = authorizationService;
            _requestManager = requestManager;
            _sessionService = sessionService;
        }

        [Route("checkout")]
        public async Task<IActionResult> Index()
        {
            // if user doesn't have anything in cart, redirect to Cart page
            // Todo: fetch the shipping address and name, fetch cart items too


            // Todo: check what shipping methods the order qualifies for
            // This means we must set shipping methods for each product

            // NOTE: for now, we use just 2 static methods

            string cartId = _sessionService.GetCartIdIfExists();
            if (cartId == null) return RedirectToAction("Index", "ShoppingCart");

            // if user doesn't have shipping and billing addresses, navigate to address page
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<CheckoutPageDataDto>>($"/api/Cart/checkout/{cartId}", null,
                HttpMethod.Get);

            var shippingAddress = apiResponse.Data.AddressData.ShippingAddress;
            var cartSummary = apiResponse.Data.CartSummary;
            if (shippingAddress == null)
            {
                return RedirectToAction("UpdateAddress", new {hasAddress=false, returnUrl = "/Checkout/Index" });
            }

            if (!cartSummary.CartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

           
            var model = new OrderCheckoutViewModel()
            {
                AddressInfo =
                    $"{shippingAddress.FirstName} {shippingAddress.LastName}, {shippingAddress.AddressLine1}, {shippingAddress.City}...",
                CartSummary = new CartPartialViewModel()
                {
                    CartItems = cartSummary.CartItems,
                    CartAmount = cartSummary.CartAmount
                }
            };

            return View(model);
        }

        [Route("checkout")]
        [HttpPost]
        public async Task<IActionResult> Index(OrderCheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Todo: call api
                // get the price displayed to the user
                // recalculate the price in backend and compare
                // throw error if there's a difference

                // on success delete the cart from the db and session

                return RedirectToAction("OrderSubmitted");
            }

            var cartId = _sessionService.GetCartIdIfExists();
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<CheckoutPageDataDto>>($"/api/Cart/checkout/{cartId}", null,
                HttpMethod.Get);

            var shippingAddress = apiResponse.Data.AddressData.ShippingAddress;
            var cartSummary = apiResponse.Data.CartSummary;
            if (shippingAddress == null)
            {
                return RedirectToAction("UpdateAddress", new { hasAddress = false, returnUrl = "/Checkout/Index" });
            }

            if (!cartSummary.CartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            model.AddressInfo =
                $"{shippingAddress.FirstName} {shippingAddress.LastName}, {shippingAddress.AddressLine1}, {shippingAddress.City}...";
            model.CartSummary = new CartPartialViewModel()
            {
                CartItems = cartSummary.CartItems,
                CartAmount = cartSummary.CartAmount
            };
            return View(model);
        }

        public async Task<IActionResult> OrderSubmitted()
        {
            return View();
        }

        public async Task<IActionResult> UpdateAddress(string returnUrl, bool hasAddress = true)
        {
            var model = new SaveOrUpdateAddressViewModel(){SameAsShipping = true};
            if (hasAddress == true)
            {
                // address exists, so pull and display existing 
                var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<AddressListResponseDto>>($"/api/Shopper/address", null,
                    HttpMethod.Get);

                if (apiResponse.Success && apiResponse.Data.ShippingAddress != null && apiResponse.Data.BillingAddress != null)
                {
                    model.ShippingAddress = apiResponse.Data.ShippingAddress;
                    model.BillingAddress = apiResponse.Data.BillingAddress;

                    //model.ShippingAddress = new AddressViewModel()
                    //{
                    //    LastName = apiResponse.Data.ShippingAddress.LastName,
                    //    FirstName = apiResponse.Data.ShippingAddress.FirstName,
                    //    AddressLine1 = apiResponse.Data.ShippingAddress.AddressLine1,
                    //    AddressLine2 = apiResponse.Data.ShippingAddress.AddressLine2,
                    //    City = apiResponse.Data.ShippingAddress.City,
                    //    State = apiResponse.Data.ShippingAddress.State,
                    //    ZipCode = apiResponse.Data.ShippingAddress.ZipCode,
                    //    AddressType = apiResponse.Data.ShippingAddress.AddressType
                    //};
                    //model.BillingAddress = new AddressViewModel()
                    //{
                    //    LastName = apiResponse.Data.BillingAddress.LastName,
                    //    FirstName = apiResponse.Data.BillingAddress.FirstName,
                    //    AddressLine1 = apiResponse.Data.BillingAddress.AddressLine1,
                    //    AddressLine2 = apiResponse.Data.BillingAddress.AddressLine2,
                    //    City = apiResponse.Data.BillingAddress.City,
                    //    State = apiResponse.Data.BillingAddress.State,
                    //    ZipCode = apiResponse.Data.BillingAddress.ZipCode,
                    //    AddressType = apiResponse.Data.BillingAddress.AddressType
                    //};

                    model.IsEditMode = true;
                    model.SameAsShipping = false;
                }
            } 

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress(SaveOrUpdateAddressViewModel model)
        { 
            if (ModelState.IsValid)
            {
                if (model.SameAsShipping == false && model.BillingAddress == null)
                {
                    ModelState.AddModelError("", "Billing address is required!");
                    return View(model);
                }

                var payload = new SaveOrUpdateAddressDto()
                {
                    SameAsShipping = model.SameAsShipping,
                    ShippingAddress = model.ShippingAddress,
                    BillingAddress = model.BillingAddress
                };
                var apiResponse = await _requestManager.Send<SaveOrUpdateAddressDto, BaseApiResponseDto<string>>($"/api/Shopper/address", payload,
                    HttpMethod.Post);
                if (apiResponse.Success)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
               
            }
             
            return View(model);
        }

        public IActionResult GetBillingAddressPartial()
        {
            return PartialView("_BillingAddressPartial", new SaveOrUpdateAddressViewModel());
        }
    }

    
}
