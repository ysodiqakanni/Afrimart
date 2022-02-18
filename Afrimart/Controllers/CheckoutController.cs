using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.Dto;
using Afrimart.Dto.Addresses;
using Afrimart.ViewModels.Addresses;
using Afrimart.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using ServiceHelper.Requests;

namespace Afrimart.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IRequestManager _requestManager;

        public CheckoutController(IAfrimartAuthorizationService authorizationService, IRequestManager requestManager)
        {
            _authorizationService = authorizationService;
            _requestManager = requestManager;
        }
        public async Task<IActionResult> Index()
        {
            // if user doesn't have anything in cart, redirect to Cart page
            
            // if user doesn't have shipping and billing addresses, navigate to address page
            var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<AddressStatusResponseDto>>($"/api/Shopper/address/status", null,
                HttpMethod.Get);

            var hasAddress = apiResponse.Data.AddressExists; 
            if (hasAddress == false)
            {
                return RedirectToAction("UpdateAddress", new {hasAddress=false});
            }

            // Todo: check what shipping methods the order qualifies for
            // This means we must set shipping methods for each product

            // NOTE: for now, we use just 2 static methods

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
                    model.ShippingAddress = new AddressViewModel()
                    {
                        LastName = apiResponse.Data.ShippingAddress.LastName,
                        FirstName = apiResponse.Data.ShippingAddress.FirstName,
                        AddressLine1 = apiResponse.Data.ShippingAddress.AddressLine1,
                        AddressLine2 = apiResponse.Data.ShippingAddress.AddressLine2,
                        City = apiResponse.Data.ShippingAddress.City,
                        State = apiResponse.Data.ShippingAddress.State,
                        ZipCode = apiResponse.Data.ShippingAddress.ZipCode,
                        AddressType = apiResponse.Data.ShippingAddress.AddressType
                    };
                    model.BillingAddress = new AddressViewModel()
                    {
                        LastName = apiResponse.Data.BillingAddress.LastName,
                        FirstName = apiResponse.Data.BillingAddress.FirstName,
                        AddressLine1 = apiResponse.Data.BillingAddress.AddressLine1,
                        AddressLine2 = apiResponse.Data.BillingAddress.AddressLine2,
                        City = apiResponse.Data.BillingAddress.City,
                        State = apiResponse.Data.BillingAddress.State,
                        ZipCode = apiResponse.Data.BillingAddress.ZipCode,
                        AddressType = apiResponse.Data.BillingAddress.AddressType
                    };
                    model.IsEditMode = true;
                    model.SameAsShipping = false;
                }
            } 

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress(SaveOrUpdateAddressViewModel model)
        {
            var payload = new SaveOrUpdateAddressDto();

            if (ModelState.IsValid)
            {
                if (model.SameAsShipping == false && model.BillingAddress == null)
                {
                    ModelState.AddModelError("", "Billing address is required!");
                    return View(model);
                }

                return RedirectToAction("Index");
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
