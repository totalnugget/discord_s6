using UserService.Domain.Base;
using UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserService.Data.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(be => be.Id);
            builder.Property(be => be.Id).HasIdentityOptions(startValue: 100);
            builder.Property(be => be.CreatedAt).IsRequired();
            builder.Property(be => be.LastUpdatedAt).IsRequired();
        }
    }
}