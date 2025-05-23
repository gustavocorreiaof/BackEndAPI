﻿using AutoMapper;
using Core.Domain.DTOs;
using Core.Domain.Entities;

namespace Core.Infrastructure.Mappers
{
    public class ApiMapper
    {
        private readonly IMapper _mapper;

        public ApiMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId.GetValueOrDefault()));
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
