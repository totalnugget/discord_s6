using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Domain.Entities;

namespace UserService.Data.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // base values
            var time = DateTime.UtcNow;

            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.KeycloakGUID).IsRequired();

            builder.HasData(new User
            { 
                Id = 1,
                LastUpdatedAt = time,
                KeycloakGUID = new Guid("cf9f68cb-78f0-4dd4-b203-8e520b422374"),
                Name = "harry",
                CreatedAt = time,
                

            }
            );

                
            
        }
    }
}

