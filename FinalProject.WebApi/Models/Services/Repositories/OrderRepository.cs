using FinalProject.WebApi.FrameWorks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks;
using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;
using FinalProject.WebApi.Models.DomainModel.OrderAggregates;
using FinalProject.WebApi.Models.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace FinalProject.WebApi.Models.Services.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProjectDbContext _context;

        #region [-ctor-]
        public OrderRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [-SelectAll-]
        public async Task<IResponse<IEnumerable<OrderHeader>>> SelectAll()
        {
            try
            {
                var orders = await _context.OrderHeaders.Include(o => o.OrderDetails).AsNoTracking().ToListAsync();
                if (orders is null)
                {
                    return new Response<IEnumerable<OrderHeader>>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                await _context.OrderHeaders.AsNoTracking().ToListAsync();
                return new Response<IEnumerable<OrderHeader>>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, orders);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Select-]
        public async Task<IResponse<OrderHeader>> Select(OrderHeader obj)
        {
            try
            {
                var order = await _context.OrderHeaders.Include(o => o.OrderDetails)
                    .Include(o => o.Seller)
                    .Include(o => o.Buyer)
                    .FirstOrDefaultAsync(o => o.Id == obj.Id);

                if (order is null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }
                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, order);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Insert-]
        public async Task<IResponse<OrderHeader>> Insert(OrderHeader obj)
        {
            try
            {
                if (obj == null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }

                if (obj.Id == Guid.Empty)
                {
                    obj.Id = Guid.NewGuid();
                }

                var buyerExists = await _context.Persons.AnyAsync(p => p.Id == obj.BuyerId);
                var sellerExists = await _context.Persons.AnyAsync(p => p.Id == obj.SellerId);

                if (!buyerExists || !sellerExists)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.BadRequest, "Buyer or Seller not found.", null);
                }

                obj.OrderDetails = obj.OrderDetails?
                    .Where(d => d.ProductId != Guid.Empty && d.UnitPrice > 0 && d.Amount > 0)
                    .ToList();

                if (obj.OrderDetails == null || !obj.OrderDetails.Any())
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.BadRequest, "OrderDetails is required and must contain valid items.", null);
                }

                foreach (var detail in obj.OrderDetails)
                {
                    detail.OrderHeaderId = obj.Id;
                }

                _context.OrderHeaders.Add(obj);
                await _context.SaveChangesAsync();

                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
            }
            catch (Exception ex)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
        #endregion

        #region [-Update-]
        public async Task<IResponse<OrderHeader>> Update(OrderHeader obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }

                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region [-Delete-]
        public async Task<IResponse<OrderHeader>> Delete(OrderHeader obj)
        {
            try
            {
                var response = await _context.OrderHeaders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == obj.Id);

                if (response is null)
                {
                    return new Response<OrderHeader>(false, HttpStatusCode.UnprocessableContent, ResponseMessages.NullInput, null);
                }

                _context.OrderHeaders.Remove(response);
                await _context.SaveChangesAsync();

                return new Response<OrderHeader>(true, HttpStatusCode.OK, ResponseMessages.SuccessfulOperation, response);
            }
            catch (Exception ex)
            {
                return new Response<OrderHeader>(false, HttpStatusCode.InternalServerError, ex.Message, null);
            }
        }
        #endregion
    }
}
