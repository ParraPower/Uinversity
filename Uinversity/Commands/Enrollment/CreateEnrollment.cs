using AutoMapper;
using MediatR;
using Microsoft.VisualBasic;
using University.Commands;
using UniversityCore.Constants;
using University.Implementations;
using University.Interfaces;
using University.Models.Validation;
using UniversityData.Interfaces.Managers;
using EventBusModule.NotificationService;
using SDK.EventBus.Events;

namespace University.Commands.Enrollment
{
    public class CreateEnrollmentQuery : IRequest<CreateEnrollmentResponse>
    {
        public readonly Models.Facade.Enrollment Enrollment;

        public CreateEnrollmentQuery(Models.Facade.Enrollment enrollment) : base()
        {
            Enrollment = enrollment;
        }
    }

    public record class CreateEnrollmentResponse(int EnrollmentId, int CourseId, int StudentId);

    public class CreateEnrollmentCommand : IRequestHandler<CreateEnrollmentQuery, CreateEnrollmentResponse>, ICommandValidatorLogic<CreateEnrollmentQuery>
    {
        private readonly IEnrollmentDataManager _dataManager;
        private readonly IEnrollmentRepository _dataRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public CreateEnrollmentCommand(IMapper mapper, IEnrollmentDataManager enrollmentDataManager, IEnrollmentRepository enrollmentRepository, INotificationService notificationService)
        {
            _dataManager = enrollmentDataManager;
            _dataRepository = enrollmentRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<CreateEnrollmentResponse> Handle(CreateEnrollmentQuery request, CancellationToken cancellationToken)
        {
            await new CommandValidatorEngine<CreateEnrollmentCommand, CreateEnrollmentQuery>(this, request).Handle();

            var dataModel = (UniversityData.Entites.Enrollment)_mapper.Map(request.Enrollment, typeof(Models.Facade.Enrollment), typeof(UniversityData.Entites.Enrollment));

            var response = await _dataManager.CreateEnrollmentAsync(dataModel);

            var responseFacade = (Models.Facade.Enrollment)_mapper.Map(response, typeof(UniversityData.Entites.Enrollment), typeof(Models.Facade.Enrollment));

            await _dataRepository.UpdateEnrollment(responseFacade);

            var eventMessage =
            StudentActitvityEventMessageFactory.Generate(
                "1.1", "CreateEnrollment", Guid.NewGuid(), StudentActitvityEventType.Enrolled, request.Enrollment.StudentID, 1);

            await _notificationService.SendMessage(eventMessage);

            return new CreateEnrollmentResponse(response.EnrollmentID, response.CourseID, response.StudentID);
        }

        public Task<ValidationResponse> RunAsync(CreateEnrollmentQuery query)
        {
            var validStudentId = query.Enrollment.StudentID > UniversityCore.Constants.Constants.Zero;
            var validCourseId = query.Enrollment.CourseID > UniversityCore.Constants.Constants.Zero;
            var validEnrollmentId = !query.Enrollment.EnrollmentID.HasValue;

            if (validCourseId && validStudentId && validEnrollmentId) 
            {
                return Task.FromResult(new ValidationResponse(true));
            }

            var errorMessage = "Error creating Enrollment. ";

            if (!validCourseId)
            {
                errorMessage += " Invalid Course Id provided. Id must be greater than Zero.";
            }

            if (!validStudentId)
            {
                errorMessage += " Invalid Student Id provided. Id must be greater than Zero.";
            }

            if (!validEnrollmentId)
            {
                errorMessage += " Invalid Enrollment Id provided. Id must be null";
            }

            return Task.FromResult(new ValidationResponse(false, new InvalidRequestApiException(errorMessage)));
        }
    }
}
