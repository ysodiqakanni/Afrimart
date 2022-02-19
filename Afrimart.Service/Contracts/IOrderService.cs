using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.Dto.Orders;

namespace Afrimart.Service.Contracts
{
    public interface IOrderService
    {
        Task<Tuple<bool, string>> CreateOrder(CreateOrderRequestDto orderRequest, string email);
    }
}
