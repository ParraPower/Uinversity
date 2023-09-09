using MediatR;
using University.Interfaces;
using University.Models.Validation;

namespace University.Implementations
{
    public class CommandValidatorEngine<TCommandValidatorLogic, TQuery> : ICommandValidatorEngine<TCommandValidatorLogic, TQuery> where TQuery : IBaseRequest where TCommandValidatorLogic : ICommandValidatorLogic<TQuery>
    {
        private readonly TCommandValidatorLogic _commandValidatorLogic;
        private readonly TQuery _query;
        private readonly string _exceptionMessage = "Error in Api request occured";

        public CommandValidatorEngine(TCommandValidatorLogic validatorLogic, TQuery query) 
        {
            _commandValidatorLogic = validatorLogic;
            _query = query;
        }

        public async Task<ValidationResponse> Handle()
        {
            var validation = await _commandValidatorLogic.RunAsync(_query);
            
            if (validation == null || validation.Success) 
            {
                return validation ?? new ValidationResponse(true);
            }

            throw validation.ApiException ?? new InvalidRequestApiException(_exceptionMessage);
        }
    }
}
