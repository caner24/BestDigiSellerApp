using BestDigiSellerApp.User.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Commands.Response
{
    public record LoginTwoStepCommandResponse : TokenDto
    {
        public bool IsWrong2Step { get; init; }

    }
}
