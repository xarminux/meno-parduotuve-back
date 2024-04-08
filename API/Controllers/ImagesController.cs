using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly FileService _fileService;

        public ImagesController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdloadImage(IFormFile image)
        {
            var imageId = Guid.NewGuid().ToString();
            var ext = Path.GetExtension(image.FileName);
            var fileName = imageId+ext;

            await _fileService.SaveFileAsync(image.OpenReadStream(), fileName);
            return Ok(new { filePath = fileName } );
        }

        [HttpGet("{filePath}")]
        public async Task<IActionResult> DownloadImage(string filePath)
        {
            var stream = await _fileService.GetFileAsync(filePath);
            if (stream == null)
                return NotFound();

            return File(stream, "application/octet-stream");
        }

        [HttpDelete]
        public IActionResult DeleteFile(string fileName)
        {
            var result = _fileService.DeleteFile(fileName);
            if (result)
                return Ok();

            return NotFound();
        }
    }
}
