using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.File.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class FileController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(List<IFormFile> files)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var uploadedFiles = new List<object>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                uploadedFiles.Add(new
                {
                    file = file.FileName,
                    path = path,
                    size = file.Length
                });
            }
        }
        return Ok(uploadedFiles);
    }

    [HttpGet("getPhoto")]
    public IActionResult GetPhoto(string fileName)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Media", fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("Dosya bulunamadý.");

        return Ok(filePath);
    }
}
