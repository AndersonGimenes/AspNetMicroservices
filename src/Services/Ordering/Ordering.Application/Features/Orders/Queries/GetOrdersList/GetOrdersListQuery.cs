using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<IEnumerable<OrdersVm>>
    {
        public string UserName { get; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
