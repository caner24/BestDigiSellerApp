﻿
using BestDigiSellerApp.User.Application.User.Commands.Response;
using BestDigiSellerApp.User.Entity.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Commands.Request
{
    public record RegisterUserCommandRequest : UserForRegistrationDto, IRequest<FluentResults.Result<RegisterUserCommandResponse>>
    {

    }
}
