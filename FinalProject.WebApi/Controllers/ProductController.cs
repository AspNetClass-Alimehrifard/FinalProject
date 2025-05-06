using FinalProject.WebApi.ApplicationServices.Contracts;
using FinalProject.WebApi.ApplicationServices.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        #region [-Fields-]
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        #endregion

        #region [-Guard_ProductService()-]
        private ObjectResult Guard_ProductService()
        {
            if (_productService is null)
            {
                return Problem($" {nameof(_productService)} is null.");
            }
            return null;
        }
        #endregion

        #region [-ctor-]
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        #endregion

        #region [-GetAll-]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            Guard_ProductService();
            var getAllResponse = await _productService.GetAll();
            var response = getAllResponse.Value.GetProductServiceDtos;
            return new JsonResult(response);
        }
        #endregion

        #region [-Get-]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Guard_ProductService();
            var dto = new GetProductServiceDto() { Id = id };
            var getResponse = await _productService.Get(dto);
            var response = getResponse.Value;
            if (response is null)
            {
                return new JsonResult("Not Found");
            }
            return new JsonResult(response);
        }
        #endregion

        #region [-Post-]
        [HttpPost(Name = "PostProduct")]
        public async Task<IActionResult> Post([FromBody] PostProductServiceDto dto)
        {
            Guard_ProductService();
            var postDto = new GetProductServiceDto() { Title = dto.Title };
            var getResponse = await _productService.Get(postDto);
            switch (ModelState.IsValid)
            {
                case true when getResponse.Value is null:
                    {
                        var postResponse = await _productService.Post(dto);
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
        public async Task<IActionResult> Put([FromBody] PutProductServiceDto dto)
        {
            Guard_ProductService();
            var putDto = new GetProductServiceDto() { Title = dto.Title };

            if (ModelState.IsValid)
            {
                var putResponse = await _productService.Put(dto);
                return putResponse.IsSuccessful ? Ok() : BadRequest();
            }
            return BadRequest();
        }
        #endregion

        #region [-Delete-]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductServiceDto dto)
        {
            Guard_ProductService();
            var deleteResponse = await _productService.Delete(dto);
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion
    }
}