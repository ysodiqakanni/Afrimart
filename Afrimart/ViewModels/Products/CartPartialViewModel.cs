using Afrimart.Dto.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Afrimart.ViewModels.Products
{
    public class CartPartialViewModel: ShoppingCartResponseDto
    { 
        // get dropdown items for each product's cart quantity
        public Dictionary<string, List<SelectListItem>> QuantitiesByCartItem
        {
            get
            {
                var dictSelectLists = new Dictionary<string, List<SelectListItem>>();
                foreach (var item in CartItems)
                {
                    var listItems = new List<SelectListItem>();
                    // Cannot by more than X units at a time 
                    for (var i = 1; i <= Math.Min(AfrimartConstants.MAXIMUM_PER_PRODUCT_UNITS_PER_CART, item.AvailableUnits); i++)
                    {
                        listItems.Add(new SelectListItem(i.ToString(), i.ToString(), selected: i == item.Quantity));
                    }
                    dictSelectLists.Add(item.ProductPSIN, listItems);
                }

                return dictSelectLists;
            }
        }
    }
     
}
