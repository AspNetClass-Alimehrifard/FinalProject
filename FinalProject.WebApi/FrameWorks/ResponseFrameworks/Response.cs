using FinalProject.WebApi.FrameWorks.ResponseFrameworks.Contracts;
using System.Net;

namespace FinalProject.WebApi.FrameWorks.ResponseFrameworks
{
    public class Response<T> : IResponse<T>
    {
        #region [-ctor-]
        public Response()
        {
        }

        public Response(bool isSuccessful, HttpStatusCode status, string? message, T? value)
        {
            IsSuccessful = isSuccessful;
            Status = status;
            Message = message;
            Value = value;
        }
        #endregion

        public bool IsSuccessful { get; set; }
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
