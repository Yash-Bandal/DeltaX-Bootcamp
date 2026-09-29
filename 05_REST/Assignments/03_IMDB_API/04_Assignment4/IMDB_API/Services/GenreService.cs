using AutoMapper;
using IMDB_API.Helpers;
using IMDB_API.Helpers.Validators;
using IMDB_API.Models.Db;
using IMDB_API.Models.Requests;
using IMDB_API.Models.Responses;
using IMDB_API.Repository;
using IMDB_API.Repository.Interfaces;
using IMDB_API.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GenreResponse>> Get()
        {
            var genres = await _genreRepository.Get();
            return _mapper.Map<IEnumerable<GenreResponse>>(genres);
        }


        public async Task<GenreResponse> Get(int id)
        {
            ValidationHelper.ValidatePositiveInt(id, "Genre Id");
            var genre = await _genreRepository.Get(id);
            ValidationHelper.ValidateNotFound(genre, "Genre");

            return _mapper.Map<GenreResponse>(genre);
        }

        public async Task<IEnumerable<GenreResponse>> Get(IEnumerable<int> ids)
        {
            List<int> genreIds = ids.ToList();
            List<Genre> genres = (await _genreRepository.Get(genreIds)).ToList();

            GenreValidator.ValidateIds(genreIds, genres);
       
            return _mapper.Map<IEnumerable<GenreResponse>>(genres);
        }

        public async Task<int> Add(GenreRequest request)
        {
            GenreValidator.ValidateRequest(request);

            var genre = _mapper.Map<Genre>(request);
            genre.Id = await _genreRepository.Add(genre);

            return genre.Id;
        }

        public async Task Update(int id, GenreRequest request)
        {
            ValidationHelper.ValidatePositiveInt(id, "Genre Id");
            GenreValidator.ValidateRequest(request);
            await Get(id);

            var genre = _mapper.Map<Genre>(request);
            await _genreRepository.Update(id, genre);
        }

        public async Task Delete(int id)
        {
            ValidationHelper.ValidatePositiveInt(id, "Genre Id");
            await Get(id);

            await _genreRepository.Delete(id);
        }
    }
}
