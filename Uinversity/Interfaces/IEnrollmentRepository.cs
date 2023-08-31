
using University.Models.Facade;

namespace University.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment?> GetEnrollment(int enrollmentId);
        Task<Enrollment> UpdateEnrollment(Enrollment basket);
        Task DeleteEnrollment(int enrollmentId);
    }
}
