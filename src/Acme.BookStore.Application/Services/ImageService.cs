

using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Acme.BookStore.Services
{
    public class ImageService : IImageService
    {
        public async Task<(bool isImageUploaded, string? filepath)> UploadImage(IFormFile? imageFile, string rootPath, string folder, string? fileName)
        {
            if (imageFile is null || imageFile.Length == 0)
                return (false, null);
            try
            {

                fileName = fileName == null || (fileName != null && fileName.Length <= 1) ? $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}" : fileName;
                var folderFullPath = Path.Combine(rootPath, folder);
                if (!Directory.Exists(folderFullPath))
                    Directory.CreateDirectory(folderFullPath);
                folderFullPath = Path.Combine(folderFullPath, fileName);
                using (var stream = new FileStream(folderFullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return (true, $@"{folderFullPath}");
            }
            catch
            {
                return (false, null);
            }

        }
    }
}
