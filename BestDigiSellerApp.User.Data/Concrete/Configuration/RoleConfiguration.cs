using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Data.Concrete.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var adminRole = new IdentityRole
            {
                Id = "c33bab16-7a80-4c43-b680-e02cc314af71",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            builder.HasData(adminRole);
        }
    }
}
