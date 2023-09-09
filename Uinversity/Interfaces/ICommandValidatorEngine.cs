using MediatR;
using University.Models.Validation;

namespace University.Interfaces
{
    public interface ICommandValidatorEngine<TCommandValidatorLogic, TQuery> where TQuery : IBaseRequest where TCommandValidatorLogic : ICommandValidatorLogic<TQuery>
    {
        internal Task<ValidationResponse> Handle();
    }
}
 