using AutoMapper;
using IMDB_API.Helpers;
using IMDB_API.Helpers.Validators;
using IMDB_API.Models.Db;
using IMDB_API.Models.Requests;
using IMDB_API.Models.Responses;
using IMDB_API.Repository;
using IMDB_API.Repository.Interfaces;
using IMDB_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorService(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorResponse>> Get()
        {
            var actors = await _actorRepository.Get();
            return _mapper.Map<IEnumerable<ActorResponse>>(actors);
        }

        public async Task<ActorResponse> Get(int id)
        {
            ValidationHelper.ValidatePositiveInt(id, "Actor Id");
            var actor = await _actorRepository.Get(id);
            ValidationHelper.ValidateNotFound(actor, "id");

            return _mapper.Map<ActorResponse>(actor);
        }

        public async Task<IEnumerable<ActorResponse>> Get(IEnumerable<int> ids)
        {
            List<int> actorIds = ids.ToList();
            List<Actor> actors = (await _actorRepository.Get(actorIds)).ToList();

            ActorValidator.ValidateIds(actorIds, actors);

            return _mapper.Map<IEnumerable<ActorResponse>>(actors);
        }

        public async Task<int> Add(ActorRequest request)
        {
            ActorValidator.ValidateRequest(request);

            var actor = _mapper.Map<Actor>(request);
            actor.Id  = await _actorRepository.Add(actor);

            return actor.Id;
        }

        public async Task Update(int id, ActorRequest request)
        {
            ValidationHelper.ValidatePositiveInt(id, "Actor Id");
            ActorValidator.ValidateRequest(request);
            await Get(id);

            var Actor = _mapper.Map<Actor>(request);
            await _actorRepository.Update(id, Actor);
        }

        public async Task Delete(int id)
        {
            ValidationHelper.ValidatePositiveInt(id, "Actor Id");
            await Get(id);

            await _actorRepository.Delete(id);
        }
    }
}
