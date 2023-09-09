using University.Interfaces;

namespace University.Models.Validation
{
    public class ValidationResponse {
        
        public ValidationResponse(bool success, ApiException? apiException = null) 
        {
            Success = success;
            ApiException = apiException;
        }

        public ApiException? ApiException { get; set; } = null;
        public bool Success { get; set; }
    }
}
