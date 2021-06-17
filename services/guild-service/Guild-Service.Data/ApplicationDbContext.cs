using System.Collections.Generic;
using GuildService.Data.Configurations;
using GuildService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using GuildService.Domain.Base;
using System;

namespace GuildService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Guild> Guild { get; set; }

        public DbSet<GuildUser> GuildUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GuildConfiguration()) // guild config
            .ApplyConfiguration(new ChannelPosConfiguration()) // channel config
            .ApplyConfiguration(new GuildUserConfiguration()); // user config
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).LastUpdatedAt = now;
            }
            this.ChangeTracker.DetectChanges();
        }
    }
}