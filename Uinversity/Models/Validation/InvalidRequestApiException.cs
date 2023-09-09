namespace University.Models.Validation
{
    public class InvalidRequestApiException : ApiException
    {
        public InvalidRequestApiException(string message) : base(message, System.Net.HttpStatusCode.BadRequest)
        {
        }
    }
}
