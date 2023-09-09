using UniversityData.Entites;

namespace UniversityData.Interfaces.Managers
{
    public interface IEnrollmentDataManager
    {
        public Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment);
        public Task<Enrollment?> GetEnrollmentAsync(int enrollmentID);
    }
}
