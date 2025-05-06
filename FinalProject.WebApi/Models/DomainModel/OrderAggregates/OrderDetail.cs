using FinalProject.WebApi.Models.DomainModel.ProductAggregates;

namespace FinalProject.WebApi.Models.DomainModel.OrderAggregates
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public OrderHeader? OrderHeader { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get { return UnitPrice * Amount; } }
    }
}
