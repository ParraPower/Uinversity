using System.Net;

namespace University.Models.Validation
{
    public class NotFoundApiException : ApiException
    {
        
        public NotFoundApiException(string message) : base(message, HttpStatusCode.NotFound)
        {

        }
    }
}
