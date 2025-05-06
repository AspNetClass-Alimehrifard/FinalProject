namespace FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos
{
    public class PutOrderDetailServiceDto
    {
        public Guid? Id { get; set; }
        public Guid? OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
