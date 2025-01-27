using AutoMapper;
using ABCIgnite.DTO;
using ABCIgnite.Models;

namespace ABCIgnite
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Class, ClassDTO>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToDateTime(new TimeOnly())))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToDateTime(new TimeOnly())))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()));

            CreateMap<ClassDTO, Class>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EndDate)))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)));




            CreateMap<Booking, BookingDTO>()
                 .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
                 .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.MemberName))
                 .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
                 .ForMember(dest => dest.ParticipationDate, opt => opt.MapFrom(src => src.ParticipationDate));

            CreateMap<BookingDTO, Booking>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.MemberName))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.ParticipationDate, opt => opt.MapFrom(src => src.ParticipationDate));

           /* CreateMap<Booking, BookingDTO>()
           .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
           .ForMember(dest => dest.ClassStartTime, opt => opt.MapFrom(src => src.Class.StartTime));*/



        }
    }
}

