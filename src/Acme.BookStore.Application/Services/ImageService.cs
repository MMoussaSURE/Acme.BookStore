
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Acme.BookStore.Services
{
    public class ImageService : IImageService
    {
        public async Task<(bool isImageUploaded, string? filepath)> UploadImage(string base64Data, string rootPath, string folder)
        {

            if(string.IsNullOrEmpty(base64Data) || string.IsNullOrWhiteSpace(base64Data))
                return (false, null);
            try
            {

                var fileName = $"{Guid.NewGuid()}.png";

                var folderFullPath = $@"{ReplaceBackslashes(rootPath)}/{folder}";
                if (!Directory.Exists(folderFullPath))
                    Directory.CreateDirectory(folderFullPath);
                

                var imageStr = base64Data.Replace("data:image/png;base64,", string.Empty);
                using Image<Rgba32> image = (Image<Rgba32>)Image.Load(Convert.FromBase64String(imageStr));
                image.Save($@"{folderFullPath}/{fileName}");
                return (true, $@"{folderFullPath}/{fileName}");
            }
            catch
            {
                return (false, null);
            }

        }

        static string ReplaceBackslashes(string input)
        {
            return input.Replace(@"\", "/");
        }
    }
}
