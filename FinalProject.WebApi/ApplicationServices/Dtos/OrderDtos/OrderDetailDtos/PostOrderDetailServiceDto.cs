namespace FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos
{
    public class PostOrderDetailServiceDto
    {
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
