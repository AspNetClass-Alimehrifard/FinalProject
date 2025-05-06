using FinalProject.WebApi.Models.DomainModel.PersonAggregates;

namespace FinalProject.WebApi.Models.Services.Contracts
{
    public interface IPersonRepository : IRepository<Person, IEnumerable<Person>>
    {
    }
}
