using UniversityData.Entites;

namespace University.Models.Facade
{
    public class Enrollment
    {
        public Enrollment() 
        {
            
        }

        public int? EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
    }
}
