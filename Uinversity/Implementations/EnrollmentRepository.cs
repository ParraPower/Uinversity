using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using University.Interfaces;
using University.Models.Data;
using University.Models.Facade;

namespace University.Implementations
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private const string ENROLLMENT_KEY_PREX = "enrollment_";
        private readonly IDistributedCache _redisCache;
        private static string GetKey(int enrollmentId)
        {
            return $"{ENROLLMENT_KEY_PREX}{enrollmentId}";
        }

        private static string GetKey(Enrollment enrollment)
        {
            if (!enrollment.EnrollmentID.HasValue)
            {
                throw new Exception("Enrollment Id is null");
            }

            return GetKey(enrollment.EnrollmentID.Value);
        }
        public EnrollmentRepository(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<Enrollment?> GetEnrollment(int enrollmentId)
        {
            var entity = await _redisCache.GetStringAsync(GetKey(enrollmentId));
            if (string.IsNullOrEmpty(entity))
                return null;
            return JsonConvert.DeserializeObject<Enrollment>(entity);
        }

        public async Task<Enrollment> UpdateEnrollment(Enrollment enrollment)
        {
            if (!enrollment.EnrollmentID.HasValue)
            {
                throw new Exception("Enrollment Id is null");
            }

            await _redisCache.SetStringAsync(GetKey(enrollment), JsonConvert.SerializeObject(enrollment));
            return await GetEnrollment(enrollment.EnrollmentID.Value) ?? throw new Exception("Unable to insert/update enrollment");
        }
        public async Task DeleteEnrollment(int enrollmentId)
        {
            await _redisCache.RemoveAsync(GetKey(enrollmentId));
        }
    }
}
