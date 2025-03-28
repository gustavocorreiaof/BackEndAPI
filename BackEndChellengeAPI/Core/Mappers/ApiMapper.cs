using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Core.Mappers
{
    public class ApiMapper
    {
        private readonly IMapper _mapper;

        public ApiMapper()
        {
            // Configuração do AutoMapper para mapear entre User e UserDTO
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber)); // Mapear CPF para TaxNumber
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber)); // Mapear TaxNumber para CPF
            });

            _mapper = config.CreateMapper();
        }

        public object MapToEntityOrDTO(object input)
        {
            if (input is User user)
            {
                // Mapeia de User para UserDTO
                return _mapper.Map<UserDTO>(user);
            }
            else if (input is UserDTO userDTO)
            {
                // Mapeia de UserDTO para User
                return _mapper.Map<User>(userDTO);
            }
            else
            {
                throw new ArgumentException("Invalid input type");
            }
        }
    }
}
