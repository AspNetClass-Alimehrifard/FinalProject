using FinalProject.WebApi.ApplicationServices.Contracts;
using FinalProject.WebApi.ApplicationServices.Dtos.PersonDtos;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        #region [-Fields-]
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;
        #endregion

        #region [-Guard_PersonService()-]
        private ObjectResult Guard_PersonService()
        {
            if (_personService is null)
            {
                return Problem($"{nameof(_personService)}is null.");
            }
            return null;
        }
        #endregion

        #region [-ctor-]
        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }
        #endregion

        #region [-GetAll-]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            Guard_PersonService();
            var getAllResponse = await _personService.GetAll();
            var response = getAllResponse.Value.GetPersonServiceDtos;
            return new JsonResult(response);
        }
        #endregion

        #region [-Get-]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Guard_PersonService();
            var dto = new GetPersonServiceDto() { Id = id };
            var getResponse = await _personService.Get(dto);
            var response = getResponse.Value;
            if (response is null)
            {
                return new JsonResult("Not Found");
            }
            return new JsonResult(response);
        }
        #endregion

        #region [-Post-]
        [HttpPost(Name = "PostPerson")]
        public async Task<IActionResult> Post([FromBody] PostPersonServiceDto dto)
        {
            Guard_PersonService();
            var postDto = new GetPersonServiceDto() { Email = dto.Email };
            var getResponse = await _personService.Get(postDto);

            switch (ModelState.IsValid)
            {
                case true when getResponse.Value is null:
                    {
                        var postResponse = await _personService.Post(dto);
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
        public async Task<IActionResult> Put([FromBody] PutPersonServiceDto dto)
        {
            Guard_PersonService();
            var putDto = new GetPersonServiceDto() { Email = dto.Email };
            if (ModelState.IsValid)
            {
                var putResponse = await _personService.Put(dto);
                return putResponse.IsSuccessful ? Ok() : BadRequest();
            }
            return BadRequest();
        }
        #endregion

        #region [-Delete-]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromBody] DeletePersonServiceDto dto)
        {
            Guard_PersonService();
            var deleteResponse = await _personService.Delete(dto);
            return deleteResponse.IsSuccessful ? Ok() : BadRequest();
        }
        #endregion
    }
}