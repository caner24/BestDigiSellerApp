using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Data.Concrete.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var userId = "294b1b78-3268-4226-8e86-87c63a6868b0"; 
            var roleId = "c33bab16-7a80-4c43-b680-e02cc314af71"; 

            builder.HasData(new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = roleId
            });
        }
    }
}
