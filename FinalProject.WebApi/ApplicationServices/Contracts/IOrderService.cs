using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos;
using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderHeaderDtos;

namespace FinalProject.WebApi.ApplicationServices.Contracts
{
    public interface IOrderService : IService<GetOrderHeaderServiceDto, GetAllOrderHeaderServiceDto, PostOrderHeaderServiceDto, PutOrderHeaderServiceDto, DeleteOrderHeaderServiceDto>
    {
    }
}
