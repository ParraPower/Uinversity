using AutoMapper;
using Azure;
using LoggerService.Interfaces;
using MediatR;
using University.Implementations;
using University.Interfaces;
using University.Models.Validation;
using UniversityData.Interfaces.Managers;

namespace University.Commands.Enrollment
{
    public class GetEnrollmentQuery : IRequest<GetEnrollmentResponse>
    {
        public readonly int EnrollmentID;

        public GetEnrollmentQuery(int enrollmentID) : base()
        {
            EnrollmentID = enrollmentID;
        }
    }

    public record class GetEnrollmentResponse(Models.Facade.Enrollment Enrollment);

    public class GetEnrollmentCommand : IRequestHandler<GetEnrollmentQuery, GetEnrollmentResponse>, ICommandValidatorLogic<GetEnrollmentQuery>
    {
        private readonly IEnrollmentDataManager _dataManager;
        private readonly IEnrollmentRepository _dataRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public GetEnrollmentCommand(IMapper mapper, IEnrollmentDataManager enrollmentDataManager, IEnrollmentRepository enrollmentRepository, ILoggerManager logger)
        {
            _dataManager = enrollmentDataManager;
            _dataRepository = enrollmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetEnrollmentResponse> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
        {
            await new CommandValidatorEngine<GetEnrollmentCommand, GetEnrollmentQuery>(this, request).Handle();

            var responseFacade = 
                await _dataRepository.GetEnrollment(request.EnrollmentID) ??
                _mapper.Map(await _dataManager.GetEnrollmentAsync(request.EnrollmentID), typeof(UniversityData.Entites.Enrollment), typeof(Models.Facade.Enrollment)) as Models.Facade.Enrollment;

            if (responseFacade == null)
            {
                throw new NotFoundApiException("Enrollment could not be found");
            }

            if (responseFacade != null)
            {
                await _dataRepository.UpdateEnrollment(responseFacade);
            }

            _logger.LogDebug("test");

            return new GetEnrollmentResponse(responseFacade);
        }

        public Task<ValidationResponse> RunAsync(GetEnrollmentQuery query)
        {
            var validEnrollmentId = query.EnrollmentID > UniversityCore.Constants.Constants.Zero;

            if (validEnrollmentId) 
            {
                return Task.FromResult(new ValidationResponse(true));
            }

            var errorMessage = "Error getting Enrollment. ";

            if (!validEnrollmentId)
            {
                errorMessage += " Invalid Enrollment Id provided. Id must be greater than Zero";
            }

            return Task.FromResult(new ValidationResponse(false, new InvalidRequestApiException(errorMessage)));
        }
    }
}
