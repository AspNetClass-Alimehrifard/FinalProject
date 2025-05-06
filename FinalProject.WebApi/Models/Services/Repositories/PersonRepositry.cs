using FinalProject.WebApi.FrameWorks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;
using FinalProject.WebApi.Models.DomainModel.PersonAggregates;
using FinalProject.WebApi.Models.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FinalProject.WebApi.Models.Services.Repositories
{
    public class PersonRepositry : IPersonRepository
    {
        private readonly ProjectDbContext _context;

        #region [-ctor-]
        public PersonRepositry(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [-SelectAll-]
        public async Task<IResponse<IEnumerable<Person>>> SelectAll()
        {
            try
            {
                var persons = await _context.Persons.AsNoTracking().ToListAsync();
                if (persons is null)
                {
                    return new Response<IEnumerable<Person>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                return new Response<IEnumerable<Person>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, persons);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Select-]
        public async Task<IResponse<Person>> Select(Person obj)
        {
            try
            {
                var responseValue = new Person();
                if (obj.Id.ToString() == "")
                {
                    responseValue = await _context.Persons.Where(c => c.Email == obj.Email).FirstOrDefaultAsync();
                }
                else
                {
                    responseValue = await _context.Persons.FindAsync(obj.Id);
                }
                return responseValue is null ?
                    new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null) :
                    new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, responseValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Insert-]
        public async Task<IResponse<Person>> Insert(Person obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                if (obj.Id == Guid.Empty)
                {
                    obj.Id = Guid.NewGuid();
                }
                await _context.Persons.AddAsync(obj);
                await _context.SaveChangesAsync();
                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Update-]
        public async Task<IResponse<Person>> Update(Person obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Delete-]
        public async Task<IResponse<Person>> Delete(Person obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Person>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Persons.Remove(obj);
                await _context.SaveChangesAsync();
                var response = new Response<Person>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
