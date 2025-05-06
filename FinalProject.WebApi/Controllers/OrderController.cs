using FinalProject.WebApi.ApplicationServices.Contracts;
using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderDetailDtos;
using FinalProject.WebApi.ApplicationServices.Dtos.OrderDtos.OrderHeaderDtos;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        #region [-Fields-]
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        #endregion

        #region [-Guard_OrderService()-]
        private ObjectResult Guard_OrderService()
        {
            if (_orderService is null)
            {
                return Problem($"{nameof(_orderService)} is null");
            }
            return null;
        }
        #endregion

        #region [-ctor-]
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        #endregion

        #region [-GetAll-]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            Guard_OrderService();
            var getAllResponse = await _orderService.GetAll();
            var response = getAllResponse.Value.GetOrderHeaderServiceDtos;
            return new JsonResult(response);
        }
        #endregion

        #region [-Get-]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Guard_OrderService();
            var dto = new GetOrderHeaderServiceDto() { Id = id };
            var getResponse = await _orderService.Get(dto);
            var response = getResponse.Value;
            if (response is null)
            {
                return new JsonResult("NotFound");
            }
            return new JsonResult(response);
        }
        #endregion

        #region [-Post-]
        [HttpPost(Name = "PostOrderHeader")]
        public async Task<IActionResult> Post([FromBody] PostOrderHeaderServiceDto dto)
        {
            Guard_OrderService();
            var postDto = new GetOrderHeaderServiceDto() { SellerId = dto.SellerId, BuyerId = dto.BuyerId };
            var getResponse = await _orderService.Get(postDto);

            switch (ModelState.IsValid)
            {
                case true when getResponse.Value is null:
                    {
                        var postResponse = await _orderService.Post(dto);
                        return postResponse.IsSuccessful ? Ok() : BadRequest();
                    }
                case true when getResponse.Value is not null:
                    return Conflict(dto);
                default:
                    return BadRequest();
            }
        }
        #endregion

        #region [-Put-]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromBody] PutOrderHeaderServiceDto dto)
        {
            Guard_OrderService();
            var putDto = new GetOrderHeaderServiceDto() { Id = dto.Id, SellerId = dto.SellerId, BuyerId = dto.BuyerId };

            if (ModelState.IsValid)
            {
                var putResponse = await _orderService.Put(dto);
                return putResponse.IsSuccessful ? Ok() : BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region [-Delete-]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderHeaderServiceDto dto)
        {
            Guard_OrderService();
            var deleteResponse = await _orderService.Delete(dto);
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion
    }
}