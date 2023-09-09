using AutoMapper;
using UniversityCore.Constants;

namespace University.Mappings
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Models.Facade.Enrollment, UniversityData.Entites.Enrollment>()
                .ForMember((dest) => dest.EnrollmentID, opt => opt.MapFrom(src => src.EnrollmentID ?? Constants.Zero))
                .ReverseMap();

        }
    }
}
