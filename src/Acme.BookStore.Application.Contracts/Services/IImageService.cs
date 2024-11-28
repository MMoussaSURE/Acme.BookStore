using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Services
{
    public interface IImageService : ISingletonDependency
    {
        Task<(bool isImageUploaded, string? filepath)> UploadImage(string base64Data, string rootPath, string folder);
    }
}
