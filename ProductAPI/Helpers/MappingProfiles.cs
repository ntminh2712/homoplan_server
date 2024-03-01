using AutoMapper;
using SeminarAPI.Data.Dto;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
          
           // CreateMap<OrderDto, Orders>()
           //.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
           //.ForMember(d => d.Status_payment, o => o.MapFrom(s => s.Status_payment))
           //.ForMember(d => d.Payment_amount, o => o.MapFrom(s => s.Payment_amount))
           //.ForMember(d => d.Created_at, o => o.MapFrom(s => s.Created_at))
           //.ForMember(d => d.Banks_id, o => o.MapFrom(s => s.Banks_id))
           //.ForMember(d => d.Description, o => o.MapFrom(s => s.Description));

        }
    }
}
