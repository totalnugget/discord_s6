using System;
using System.Collections.Generic;
using UserService.Domain.Base;

namespace UserService.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid KeycloakGUID { get; set; }

        public string Name { get; set; }

    }
}