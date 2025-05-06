using FinalProject.WebApi.ApplicationServices.Dtos.PersonDtos;

namespace FinalProject.WebApi.ApplicationServices.Contracts
{
    public interface IPersonService : IService<GetPersonServiceDto, GetAllPersonServiceDto, PostPersonServiceDto, PutPersonServiceDto, DeletePersonServiceDto>
    {
    }
}
