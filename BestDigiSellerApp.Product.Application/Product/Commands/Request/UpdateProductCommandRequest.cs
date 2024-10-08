﻿using BestDigiSellerApp.Product.Entity.Dto;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Commands.Request
{
    public record UpdateProductCommandRequest : ProductForUpdateDto, IRequest<Result>
    {

    }
}
