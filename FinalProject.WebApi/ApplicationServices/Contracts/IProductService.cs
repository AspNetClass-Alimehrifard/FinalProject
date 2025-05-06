using FinalProject.WebApi.ApplicationServices.Dtos.ProductDtos;

namespace FinalProject.WebApi.ApplicationServices.Contracts
{
    public interface IProductService : IService<GetProductServiceDto, GetAllProductServiceDto, PostProductServiceDto, PutProductServiceDto, DeleteProductServiceDto>
    {
    }
}
