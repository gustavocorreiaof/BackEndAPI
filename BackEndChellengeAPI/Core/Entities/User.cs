﻿using Core.Common.Entities.Base;
using Core.Common.Enums;

namespace Core.Common.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string TaxNumber { get; set; }

        public string Email { get; set; }

        public UserType Type { get; set; }

        public User(long id, string name, string pwd, string cpf, string email, UserType userType)
        {
            Id = id;
            Name = name;
            Password = pwd;
            TaxNumber = cpf;
            Email = email;
            Type = userType;
        }

        public User() { }
    }
}
