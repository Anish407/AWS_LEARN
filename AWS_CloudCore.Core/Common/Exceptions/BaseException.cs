using AWS_CloudCore.Core.Common.Enums;
using System.Net;

namespace AWS_CloudCore.Core.Common.Exceptions
{
    public class BaseException : Exception
    {
        public ExceptionStatus Status { get; set; } = ExceptionStatus.none;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string Details { get; set; }

        public BaseException(string message)
            : base(message)
        {

        }
    }
}
