using Dapper;
using IMDB_API.Models.Db;
using IMDB_API.Models.Filters;
using IMDB_API.Repository.Interfaces;
using IMDBSample.Models.Db;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBConnection)
        {
        }

        public async Task<IEnumerable<Movie>> Get(MovieFilter filter)
        {
            const string query = @"
                SELECT
                    Id,
                    Name,
                    YearOfRelease,
                    Plot,
                    CoverImage,
                    ProducerId
                FROM Foundation.Movies
                WHERE (@Year IS NULL OR YearOfRelease = @Year)";

            return await QueryAsync(
                query,
                new
                {
                    Year = filter.Year
                });
        }

        public async Task<Movie> Get(int id)
        {
            const string query = @"
                SELECT
                    Id,
                    Name,
                    YearOfRelease,
                    Plot,
                    CoverImage,
                    ProducerId
                FROM Foundation.Movies
                WHERE Id = @Id";

            return await QuerySingleAsync(
                query,
                new { Id = id });
        }

        public async Task<int> Add(
            Movie movie,
            List<int> actorIds,
            List<int> genreIds)
        {
            const string procedure = "Foundation.usp_AddMovie";

            return await ExecuteStoredProcedureAsync<int>(
                procedure,
                new
                {
                    movie.Name,
                    movie.YearOfRelease,
                    movie.Plot,
                    movie.CoverImage,
                    movie.ProducerId,
                    ActorIds = string.Join(",", actorIds),
                    GenreIds = string.Join(",", genreIds)
                });
        }


        public async Task Update(int id, Movie movie, List<int> actorIds, List<int> genreIds)
        {
            const string procedure = "Foundation.usp_UpdateMovie";

            await ExecuteStoredProcedureNonQueryAsync(
                procedure,
                new
                {
                    Id = id,
                    movie.Name,
                    movie.YearOfRelease,
                    movie.Plot,
                    movie.CoverImage,
                    movie.ProducerId,
                    ActorIds = string.Join(",", actorIds),
                    GenreIds = string.Join(",", genreIds)
                });
        }

        public async Task Delete(int id)
        {
            const string query = @"
                DELETE FROM Foundation.Actor_Movies
                WHERE MovieId = @Id;

                DELETE FROM Foundation.Genre_Movies
                WHERE MovieId = @Id;

                DELETE FROM Foundation.Reviews
                WHERE MovieId = @Id;

                DELETE FROM Foundation.Movies
                WHERE Id = @Id;";

            await ExecuteAsync(
                query,
                new { Id = id });
        }

        public async Task<IEnumerable<int>> GetActorIds(int movieId)
        {
            const string query = @"
                SELECT ActorId
                FROM Foundation.Actor_Movies
                WHERE MovieId = @MovieId";

            return await QueryAsync<int>(
                query,
                new { MovieId = movieId });
        }

        public async Task<IEnumerable<int>> GetGenreIds(int movieId)
        {
            const string query = @"
                SELECT GenreId
                FROM Foundation.Genre_Movies
                WHERE MovieId = @MovieId";

            return await QueryAsync<int>(
                query,
                new { MovieId = movieId });
        }

        public async Task UpdateCoverImage(int movieId, string imageUrl)
        {
            const string query = @"
                UPDATE Foundation.Movies
                SET CoverImage = @ImageUrl
                WHERE Id = @MovieId";

            await ExecuteAsync(
                query,
                new
                {
                    MovieId = movieId,
                    ImageUrl = imageUrl
                });
        }
    }
}