using FinalProject.WebApi.Models.DomainModel.OrderAggregates;

namespace FinalProject.WebApi.Models.Services.Contracts
{
    public interface IOrderRepository : IRepository<OrderHeader, IEnumerable<OrderHeader>>
    {
    }
}
