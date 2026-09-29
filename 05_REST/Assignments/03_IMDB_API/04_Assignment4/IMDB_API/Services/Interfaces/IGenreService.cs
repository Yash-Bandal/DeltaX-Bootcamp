using IMDB_API.Models.Db;
using IMDB_API.Models.Requests;
using IMDB_API.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponse>> Get();
        Task<GenreResponse> Get(int id);
        Task<IEnumerable<GenreResponse>> Get(IEnumerable<int> ids);

        Task<int> Add(GenreRequest request);
        Task Update(int id, GenreRequest request);
        Task Delete(int id);
    }
}