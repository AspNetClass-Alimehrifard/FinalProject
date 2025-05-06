namespace FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos
{
    public class DeleteOrderDetailServiceDto
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
