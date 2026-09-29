using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface ISupabaseStorageService
    {
        Task<string> UploadMoviePoster(int movieId, IFormFile file);
    }
}