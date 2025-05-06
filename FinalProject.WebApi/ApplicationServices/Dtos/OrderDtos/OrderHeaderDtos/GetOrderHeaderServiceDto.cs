
namespace FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos
{
    public class GetOrderHeaderServiceDto
    {
        public Guid? Id { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? BuyerId { get; set; }
        public List<GetOrderDetailServiceDto> OrderDetails { get; set; }
    }
}
