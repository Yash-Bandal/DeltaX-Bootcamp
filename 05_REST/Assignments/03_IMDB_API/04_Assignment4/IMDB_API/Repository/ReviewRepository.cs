using IMDB_API.Models.Db;
using IMDB_API.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBConnection)
        {
        }

        public async Task<IEnumerable<Review>> Get(int movieId)
        {
            const string query = @"
                SELECT
                    Id,
                    Message,
                    MovieId
                FROM Foundation.Reviews
                WHERE MovieId = @MovieId";

            return await QueryAsync(query, new { MovieId = movieId });
        }

        public async Task<Review> Get(int movieId, int id)
        {
            const string query = @"
                SELECT
                    Id,
                    Message,
                    MovieId
                FROM Foundation.Reviews
                WHERE Id = @Id
                    AND MovieId = @MovieId";

            return await QuerySingleAsync(
                query,
                new
                {
                    Id = id,
                    MovieId = movieId
                }
            );
        }

        public async Task<int> Add(int movieId, Review review)
        {
            const string query = @"
                INSERT INTO Foundation.Reviews
                (
                    Message,
                    MovieId
                )
                VALUES
                (
                    @Message,
                    @MovieId
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await ExecuteScalarAsync<int>(
                query,
                new
                {
                    review.Message,
                    MovieId = movieId
                }
            );
        }

        public async Task Update(int movieId, int id, Review review)
        {
            const string query = @"
                UPDATE Foundation.Reviews
                SET
                    Message = @Message
                WHERE Id = @Id
                    AND MovieId = @MovieId";

            await ExecuteAsync(
                query,
                new
                {
                    Id = id,
                    MovieId = movieId,
                    review.Message
                }
            );
        }

        public async Task Delete(int movieId, int id)
        {
            const string query = @"
                DELETE FROM Foundation.Reviews
                WHERE Id = @Id
                    AND MovieId = @MovieId";

            await ExecuteAsync(
                query,
                new
                {
                    Id = id,
                    MovieId = movieId
                }
            );
        }

    }
}