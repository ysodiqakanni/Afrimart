using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Carts;
using Afrimart.Services;
using Afrimart.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHelper.Requests;

namespace Afrimart.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly IRequestManager _requestManager;
        private readonly ISessionService _sessionService;


        public ShoppingCartSummary(IRequestManager requestManager, ISessionService sessionService)
        {
            _requestManager = requestManager;
            _sessionService = sessionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        { 
            var model = new CartPartialViewModel();
            string cartId = _sessionService.GetCartIdIfExists();

            if (string.IsNullOrWhiteSpace(cartId) == false)
            {
                var apiResponse = await _requestManager.Send<string, BaseApiResponseDto<ShoppingCartResponseDto>>($"/api/Cart/{cartId}", null,
                    HttpMethod.Get);

                model = new CartPartialViewModel()
                {
                    CartItems = apiResponse.Data.CartItems,
                    CartAmount = apiResponse.Data.CartAmount
                };
            } 
            return View(model);
        } 
    }
}
