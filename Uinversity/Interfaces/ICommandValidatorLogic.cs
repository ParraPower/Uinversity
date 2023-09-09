using MediatR;
using University.Models.Validation;

namespace University.Interfaces
{
    public interface ICommandValidatorLogic<TQuery> where TQuery : IBaseRequest
    {
        public Task<ValidationResponse> RunAsync(TQuery query);
    }
}
