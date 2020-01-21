using AutoMapper;

namespace StudentsAPI
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<V1.Models.Student, V2.Models.Student>();
            CreateMap<V2.Models.Student, V1.Models.Student>();
            CreateMap<V1.Models.Filter, V2.Models.Filter>();
            CreateMap<V2.Models.Filter, V1.Models.Filter>();
        }
    }
}
