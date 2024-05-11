using System.Net;

namespace AutoPiter.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode httpStatusCode,
                            string message = null,
                            string userFriendlyMessage = null)
        {
            HttpStatusCode = httpStatusCode;
            ErrorMessage = string.IsNullOrEmpty(message) ? "Invalid input data" : message;
            UserFriendlyMessage = userFriendlyMessage;
        }

        public ApiException(string message = null,
                            string userFriendlyMessage = null) : this(HttpStatusCode.BadRequest, message, userFriendlyMessage)
        {
        }

        public HttpStatusCode HttpStatusCode { get; }
        public string ErrorMessage { get; }
        public string UserFriendlyMessage { get; }

        public override string ToString()
        {
            return $"{nameof(HttpStatusCode)}={HttpStatusCode}, {nameof(ErrorMessage)}={ErrorMessage}, {nameof(UserFriendlyMessage)}={UserFriendlyMessage}";
        }
    }
}
