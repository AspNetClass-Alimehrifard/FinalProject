using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos;

namespace FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderHeaderDtos
{
    public class PostOrderHeaderServiceDto
    {
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public List<PostOrderDetailServiceDto> OrderDetails { get; set; }
    }
}
