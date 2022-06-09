using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Request;
using AlkemyChallengeDisney.Response;
using AutoMapper;

namespace AlkemyChallengeDisney.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegisterRequest, Usuario>();
            CreateMap<Usuario, UserResponse>()
                .ForMember(dest => dest.UserName , opt => opt.MapFrom(src => src.Name));
            CreateMap<Personaje, PersonajeResponse>();
        }
    }
}
