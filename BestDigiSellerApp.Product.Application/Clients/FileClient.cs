using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BestDigiSellerApp.Product.Entity.Dto;
using BestDigiSellerApp.Product.Entity.Results;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BestDigiSellerApp.Product.Application.Clients
{
    public class FileClient(HttpClient httpClient)
    {
        public async Task<Result<List<AddedFileDto>>> AddPhotoToFileApi(PhotosForCreateDto photosRequest, CancellationToken cancellationToken = default)
        {
            var accessToken = photosRequest.Bearer.Split(" ").Last();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            using var content = new MultipartFormDataContent();
            foreach (var file in photosRequest.Files)
            {
                var fileStreamContent = new StreamContent(file.OpenReadStream());
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileStreamContent, "files", file.FileName);
            }

            var response = await httpClient.PostAsync("/api/File/upload", content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadFromJsonAsync<List<AddedFileDto>>();
                return Result.Ok(responseJson);
            }
            else
                return Result.Fail(new PhotosCouldNotAddedResult());
        }
    }
}
