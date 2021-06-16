using System;
using System.Collections.Generic;
using System.Text;
using UserService.Domain.Entities;

namespace UserService.Domain.DTOs
{
    public class UserCreate
    {

        public Guid KeycloakGUID { get; set; }

        public string Name { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Name = Name,
                KeycloakGUID = KeycloakGUID
            };
        }
    }
}
