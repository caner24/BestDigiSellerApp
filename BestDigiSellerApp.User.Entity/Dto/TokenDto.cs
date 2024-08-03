using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record TokenDto
    {
        public DateTime ExpireTime { get; init; }
        public String AccessToken { get; init; }
        public String RefreshToken { get; init; }
    }
}
