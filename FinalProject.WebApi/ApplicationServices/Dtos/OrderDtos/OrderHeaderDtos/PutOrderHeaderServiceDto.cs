using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos;

namespace FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderHeaderDtos
{
    public class PutOrderHeaderServiceDto
    {
        public Guid? Id { get; set; }
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public List<PutOrderDetailServiceDto>? OrderDetails { get; set; }
    }
}
