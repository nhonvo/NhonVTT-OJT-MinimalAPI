using AutoMapper;

namespace NhonVTT_OJT_MinimalAPI
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}