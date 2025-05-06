using FinalProject.WebApi.ApplicationServices.Contracts;
using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos;
using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderHeaderDtos;
using FinalProject.WebApi.FrameWorks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;
using FinalProject.WebApi.Models.DomainModel.OrderAggregates;
using FinalProject.WebApi.Models.Services.Contracts;
using System.Net;


namespace FinalProject.WebApi.ApplicationServices.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        #region [-ctor-]
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #endregion

        #region [-GetAll-]
        public async Task<IResponse<GetAllOrderHeaderServiceDto>> GetAll()
        {
            var selectAllResponse = await _orderRepository.SelectAll();

            if (selectAllResponse.Value is null)
            {
                return new Response<GetAllOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }
            if (!selectAllResponse.IsSuccessful)
            {
                return new Response<GetAllOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }

            var dto = new GetAllOrderHeaderServiceDto
            {
                GetOrderHeaderServiceDtos = selectAllResponse.Value.Select(o => new GetOrderHeaderServiceDto
                {
                    Id = o.Id,
                    SellerId = o.SellerId,
                    BuyerId = o.BuyerId,
                    OrderDetails = o.OrderDetails?.Select(d => new GetOrderDetailServiceDto
                    {
                        Id = d.Id,
                        OrderHeaderId = d.OrderHeaderId,
                        ProductId = d.ProductId,
                        UnitPrice = d.UnitPrice,
                        Amount = d.Amount
                    }).ToList()
                }).ToList()
            };

            return new Response<GetAllOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, dto);
        }
        #endregion

        #region [-Get-]
        public async Task<IResponse<GetOrderHeaderServiceDto>> Get(GetOrderHeaderServiceDto dto)
        {
            var order = new OrderHeader { Id = dto.Id ?? Guid.Empty };
            var response = await _orderRepository.Select(order);

            if (!response.IsSuccessful)
            {
                return new Response<GetOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, null);
            }
            if (response.Value is null)
            {
                return new Response<GetOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var resultDto = new GetOrderHeaderServiceDto
            {
                Id = response.Value.Id,
                SellerId = response.Value.SellerId,
                BuyerId = response.Value.BuyerId,
                OrderDetails = response.Value.OrderDetails?.Select(d => new GetOrderDetailServiceDto
                {
                    Id = d.Id,
                    OrderHeaderId = d.OrderHeaderId,
                    ProductId = d.ProductId,
                    UnitPrice = d.UnitPrice,
                    Amount = d.Amount
                }).ToList()
            };

            return new Response<GetOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, resultDto);
        }
        #endregion

        #region [-Post-]
        public async Task<IResponse<PostOrderHeaderServiceDto>> Post(PostOrderHeaderServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PostOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var orderId = Guid.NewGuid();

            var order = new OrderHeader
            {
                Id = orderId,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
                OrderDetails = dto.OrderDetails?.Select(d => new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    OrderHeaderId = orderId,
                    ProductId = d.ProductId,
                    UnitPrice = d.UnitPrice,
                    Amount = d.Amount
                }).ToList()
            };

            var insertResponse = await _orderRepository.Insert(order);
            if (!insertResponse.IsSuccessful)
            {
                return new Response<PostOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }

            return new Response<PostOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, dto);
        }
        #endregion

        #region [-Put-]
        public async Task<IResponse<PutOrderHeaderServiceDto>> Put(PutOrderHeaderServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<PutOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var order = new OrderHeader
            {
                Id = dto.Id ?? Guid.Empty,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
                OrderDetails = dto.OrderDetails?.Select(d => new OrderDetail
                {
                    Id = d.Id ?? Guid.NewGuid(),
                    OrderHeaderId = d.OrderHeaderId ?? dto.Id ?? Guid.Empty,
                    ProductId = d.ProductId,
                    UnitPrice = d.UnitPrice,
                    Amount = d.Amount
                }).ToList()
            };

            var updateResponse = await _orderRepository.Update(order);
            if (!updateResponse.IsSuccessful)
            {
                return new Response<PutOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            return new Response<PutOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, dto);
        }
        #endregion

        #region [-Delete-]
        public async Task<IResponse<DeleteOrderHeaderServiceDto>> Delete(DeleteOrderHeaderServiceDto dto)
        {
            if (dto is null)
            {
                return new Response<DeleteOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
            }

            var order = new OrderHeader { Id = dto.Id };
            var response = await _orderRepository.Delete(order);
            if (!response.IsSuccessful)
            {
                return new Response<DeleteOrderHeaderServiceDto>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.Error, dto);
            }
            return new Response<DeleteOrderHeaderServiceDto>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, dto);
        }
        #endregion
    }
}
