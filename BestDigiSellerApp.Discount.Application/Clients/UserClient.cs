using BestDigiSellerApp.Discount.Entity.Dto;
using BestDigiSellerApp.Discount.Entity.Result;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Client
{
    public class UserClient(HttpClient httpClient)
    {
        public async Task<Result<List<string>>> GetUsers(GetAllUserRequestDto getAllUserRequestDto, CancellationToken cancellationToken = default)
        {
            var accessToken = getAllUserRequestDto.Bearer.Split(" ").Last();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetFromJsonAsync<List<string>>("/api/Admin/getAllUserEmail", cancellationToken);

            return Result.Ok(response);
        }
    }
}
