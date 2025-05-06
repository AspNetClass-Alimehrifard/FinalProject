using FinalProject.WebApi.FrameWorks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;
using FinalProject.WebApi.Models.DomainModel.ProductAggregates;
using FinalProject.WebApi.Models.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FinalProject.WebApi.Models.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProjectDbContext _context;

        #region [-ctor-]
        public ProductRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [-SelectAll-]
        public async Task<IResponse<IEnumerable<Product>>> SelectAll()
        {
            try
            {
                var products = await _context.Products.AsNoTracking().ToListAsync();
                if (products is null)
                {
                    return new Response<IEnumerable<Product>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                return new Response<IEnumerable<Product>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, products);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Select-]
        public async Task<IResponse<Product>> Select(Product obj)
        {
            try
            {
                var responseValue = new Product();
                responseValue = await _context.Products.FindAsync(obj.Id);

                if (responseValue is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                return new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, responseValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Insert-]
        public async Task<IResponse<Product>> Insert(Product obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                if (obj.Id == Guid.Empty)
                {
                    obj.Id = Guid.NewGuid();
                }
                await _context.AddAsync(obj);
                await _context.SaveChangesAsync();
                var response = new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Update-]
        public async Task<IResponse<Product>> Update(Product obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var response = new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Delete-]
        public async Task<IResponse<Product>> Delete(Product obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Product>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                _context.Remove(obj);
                await _context.SaveChangesAsync();
                var response = new Response<Product>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
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
