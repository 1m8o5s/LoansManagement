using Loans.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;

namespace Loans.Database
{
    public class LoansDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        public LoansDbContext(DbContextOptions<LoansDbContext> options, IConfiguration configuration)
            : base(options) {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = _configuration.GetConnectionString("SqlServerConnectionString");

            builder.UseSqlServer(connectionString);

            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            EntityTypeBuilder<User> user = builder.Entity<User>();
            user.Property(e => e.Id).HasColumnName("ID");
            user.Property(e => e.Name).HasMaxLength(100);
            user.Property(e => e.TokenExpires).HasDefaultValue(default(DateTime));
        }
    }
}
