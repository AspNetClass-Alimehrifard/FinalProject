﻿using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;

namespace FinalProject.WebApi.Models.Services.Contracts
{
    public interface IRepository<T, TCollection>
    {
        Task<IResponse<TCollection>> SelectAll();
        Task<IResponse<T>> Select(T obj);
        Task<IResponse<T>> Insert(T obj);
        Task<IResponse<T>> Update(T obj);
        Task<IResponse<T>> Delete(T obj);
    }
}
