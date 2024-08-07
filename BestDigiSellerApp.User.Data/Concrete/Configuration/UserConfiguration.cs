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
    public class UserConfiguration : IEntityTypeConfiguration<User.Entity.User>
    {
        public void Configure(EntityTypeBuilder<Entity.User> builder)
        {
            var hasher = new PasswordHasher<User.Entity.User>();

            var user = new User.Entity.User
            {
                Id= "294b1b78-3268-4226-8e86-87c63a6868b0",
                UserName = "admin24",
                NormalizedUserName = "ADMIN24",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@GMAIL.COM"
            };

            user.PasswordHash = hasher.HashPassword(user, "Admin@123");

            builder.HasData(user);

        }
    }
}
