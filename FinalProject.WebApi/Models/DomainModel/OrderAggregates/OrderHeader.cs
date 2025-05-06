using FinalProject.WebApi.Models.DomainModel.PersonAggregates;

namespace FinalProject.WebApi.Models.DomainModel.OrderAggregates
{
    public class OrderHeader
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public Person? Seller { get; set; }
        public Person? Buyer { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
