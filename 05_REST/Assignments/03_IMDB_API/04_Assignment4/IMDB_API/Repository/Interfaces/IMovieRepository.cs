using IMDB_API.Models.Db;
using IMDB_API.Models.Filters;
using IMDBSample.Models.Db;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Get(MovieFilter filter);
        Task<Movie> Get(int id);

        Task<int> Add(Movie movie, List<int> actorIds, List<int> genreIds);
        Task Update(int id, Movie movie, List<int> actorIds, List<int> genreIds);
        Task Delete(int id);

        Task<IEnumerable<int>> GetActorIds(int movieId);
        Task<IEnumerable<int>> GetGenreIds(int movieId);

        Task UpdateCoverImage(int movieId, string imageUrl);
    }
}