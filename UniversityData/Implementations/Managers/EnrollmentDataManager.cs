using Microsoft.EntityFrameworkCore;
using UniversityData.Context;
using UniversityData.Entites;
using UniversityData.Interfaces.Managers;

namespace UniversityData.Implementations.Managers
{
    public class EnrollmentDataManager : IEnrollmentDataManager
    {
        private readonly UniversityContext _context;

        public EnrollmentDataManager(UniversityContext universityContext)
        {
            _context = universityContext;
        }

        public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new ArgumentNullException(nameof(enrollment));
            }

            var response = await _context.Enrollments.AddAsync(enrollment);

            await _context.SaveChangesAsync();

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return response.Entity;
        }

        public async Task<Enrollment?> GetEnrollmentAsync(int enrollmentID)
        {
            return await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentID == enrollmentID);
        }
    }
}
