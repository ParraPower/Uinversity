using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityData.Entites;

namespace UniversityData.EntityConfiguration
{
    public class CourseEntityConfiguration: IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(t => t.CourseID)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasData(
                new Course { CourseID = 1050, Title = "Chemistry", Credits = 3, },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3, },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3, },
                new Course { CourseID = 1045, Title = "Calculus", Credits = 4, },
                new Course { CourseID = 3141, Title = "Trigonometry", Credits = 4, },
                new Course { CourseID = 2021, Title = "Composition", Credits = 3, },
                new Course { CourseID = 2042, Title = "Literature", Credits = 4, }
                );
        }
    }
}
