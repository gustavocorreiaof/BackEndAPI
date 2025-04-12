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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber));
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber));
            });

            _mapper = config.CreateMapper();
        }

        public object MapToEntityOrDTO(object input)
        {
            if (input is User user)
            {
                return _mapper.Map<UserDTO>(user);
            }
            else if (input is UserDTO userDTO)
            {
                return _mapper.Map<User>(userDTO);
            }
            else
            {
                throw new ArgumentException("Invalid input type");
            }
        }
    }
}
