using MediatR;

namespace University.Commands.Cart
{
    public class CreateEnrollmentQuery : IRequest<CreateEnrollmentResponse>
    {
        public readonly Models.Facade.Enrollment Enrollment;

        public CreateEnrollmentQuery(Models.Facade.Enrollment enrollment) : base()
        {
            Enrollment = enrollment;
        }
    }
}
