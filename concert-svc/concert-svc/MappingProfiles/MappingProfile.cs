using AutoMapper;
using concert_svc.Entity;
using concert_svc.Model.Response;

namespace concert_svc.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Concert, ConcertResponse>();
            CreateMap<Ticket, TicketResponse>();
        }
    }
}
