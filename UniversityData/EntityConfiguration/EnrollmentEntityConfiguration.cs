using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniversityData.Entites;

namespace UniversityData.EntityConfiguration
{
    public class ErollmentEntityConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(t => t.EnrollmentID);
            builder.Property(t => t.EnrollmentID).UseIdentityColumn();

            builder.HasData(
                    new Enrollment { StudentID = 1, CourseID = 1050, Grade = Grade.A, EnrollmentID = 1 },
                    new Enrollment { StudentID = 1, CourseID = 4022, Grade = Grade.C, EnrollmentID = 2 },
                    new Enrollment { StudentID = 1, CourseID = 4041, Grade = Grade.B, EnrollmentID = 3 },
                    new Enrollment { StudentID = 2, CourseID = 1045, Grade = Grade.B, EnrollmentID = 4 },
                    new Enrollment { StudentID = 2, CourseID = 3141, Grade = Grade.F, EnrollmentID = 5 },
                    new Enrollment { StudentID = 2, CourseID = 2021, Grade = Grade.F, EnrollmentID = 6 },
                    new Enrollment { StudentID = 3, CourseID = 1050, EnrollmentID = 7 },
                    new Enrollment { StudentID = 4, CourseID = 1050, EnrollmentID = 8 },
                    new Enrollment { StudentID = 4, CourseID = 4022, Grade = Grade.F, EnrollmentID = 9 },
                    new Enrollment { StudentID = 5, CourseID = 4041, Grade = Grade.C, EnrollmentID = 10 },
                    new Enrollment { StudentID = 6, CourseID = 1045, EnrollmentID = 11 },
                    new Enrollment { StudentID = 7, CourseID = 3141, Grade = Grade.A, EnrollmentID = 12 }
                );
        }
    }
}
