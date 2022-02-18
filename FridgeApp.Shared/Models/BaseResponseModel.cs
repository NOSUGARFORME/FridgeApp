using System.Net;

namespace FridgeApp.Shared.Models
{
    public class BaseResponseModel
    {
        public string ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; }
    }
}