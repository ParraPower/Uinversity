using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityData.Entites;

namespace UniversityData.Interfaces.Managers
{
    public interface IEnrollmentDataManager
    {
        public Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment);
    }
}
