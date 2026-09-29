using IMDB_API.Models.Db;
using IMDB_API.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IMDB_API.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> connectionString)
            : base (connectionString.Value.IMDBConnection)
        {
        }

        public async Task<IEnumerable<Genre>> Get()
        {
            const string query = @"
                SELECT
                    Id,
                    Name
                FROM Foundation.Genres";

            return await QueryAsync(query);
        }

        public async Task<Genre> Get(int id)
        {
            const string query = @"
                SELECT
                    Id,
                    Name
                FROM Foundation.Genres
                WHERE Id = @Id";

            return await QuerySingleAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Genre>> Get(IEnumerable<int> genreIds)
        {
            const string query = @"
                SELECT
                    Id,
                    Name
                FROM Foundation.Genres
                WHERE Id IN @GenreIds";

            return await QueryAsync(query, new { GenreIds = genreIds });
        }

        public async Task<int> Add(Genre genre)
        {
            const string query = @"
                INSERT INTO Foundation.Genres
                (
                    Name
                )
                VALUES
                (
                    @Name
                )
                
                SELECT CAST(SCOPE_IDENTITY() AS INT)";


            return await ExecuteScalarAsync<int>(query, genre);
        }

        public async Task Update(int id, Genre genre)
        {
            const string query = @"
                UPDATE Foundation.Genres
                SET
                    Name = @Name
                WHERE Id = @Id";

            await ExecuteAsync(
                query,
                new
                {
                    Id = id,
                    genre.Name
                }
            );
        }

        public async Task Delete(int id)
        {
            const string query = @"
                DELETE FROM Foundation.Genres
                WHERE Id = @Id";

            await ExecuteAsync(query, new { Id = id });
        }
    }
}
