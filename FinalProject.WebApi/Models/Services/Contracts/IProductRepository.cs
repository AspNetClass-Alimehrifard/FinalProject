using FinalProject.WebApi.Models.DomainModel.ProductAggregates;

namespace FinalProject.WebApi.Models.Services.Contracts
{
    public interface IProductRepository : IRepository<Product, IEnumerable<Product>>
    {
    }
}
