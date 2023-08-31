using AutoMapper;
using MediatR;
using University.Commands.Cart;
using University.Interfaces;
using UniversityData.Interfaces.Managers;

namespace University.Commands.Enrollment
{
    public class CreateEnrollmentCommand : IRequestHandler<CreateEnrollmentQuery, CreateEnrollmentResponse>
    {
        private readonly IEnrollmentDataManager _dataManager;
        private readonly IEnrollmentRepository _dataRepository;
        private readonly IMapper _mapper;

        public CreateEnrollmentCommand(IMapper mapper, IEnrollmentDataManager enrollmentDataManager, IEnrollmentRepository enrollmentRepository)
        {
            _dataManager = enrollmentDataManager;
            _dataRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<CreateEnrollmentResponse> Handle(CreateEnrollmentQuery request, CancellationToken cancellationToken)
        {
            var dataModel = (UniversityData.Entites.Enrollment)_mapper.Map(request.Enrollment, typeof(Models.Facade.Enrollment), typeof(UniversityData.Entites.Enrollment));

            var response = await _dataManager.CreateEnrollmentAsync(dataModel);

            var responseFacade = (Models.Facade.Enrollment)_mapper.Map(response, typeof(UniversityData.Entites.Enrollment), typeof(Models.Facade.Enrollment));

            await _dataRepository.UpdateEnrollment(responseFacade);

            return new CreateEnrollmentResponse(response.EnrollmentID, response.CourseID, response.StudentID);
        }
    }
}
