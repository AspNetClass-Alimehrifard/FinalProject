using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;

namespace FinalProject.WebApi.ApplicationServices.Contracts
{
    public interface IService<TGet, TGetAll, TPost, TUpdate, TDelete>
    {
        Task<IResponse<TGetAll>> GetAll();
        Task<IResponse<TGet>> Get(TGet dto);
        Task<IResponse<TPost>> Post(TPost dto);
        Task<IResponse<TUpdate>> Put(TUpdate dto);
        Task<IResponse<TDelete>> Delete(TDelete dto);
    }
}
