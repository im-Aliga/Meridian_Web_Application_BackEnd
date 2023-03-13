using Meridian_Web.Contracts.Identity;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Meridian_Web.Database.Configurations
{
    public class UserIdentityConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
        {
          
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });


        }
    }
}
