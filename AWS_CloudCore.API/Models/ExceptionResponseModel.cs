using System.Net;
namespace AWS_CloudCore.API.Models
{
    public class ExceptionResponseModel
    {
        public string LogMessage { get; set; }
        public string ReponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
