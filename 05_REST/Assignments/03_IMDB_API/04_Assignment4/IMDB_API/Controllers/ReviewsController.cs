using IMDB_API.Models.Requests;
using IMDB_API.Models.Responses;
using IMDB_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_Controllers.Controllers
{
    [Authorize]
    [Route("api/movies/{movieId}/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]int movieId)
        {
            var reviews = await _reviewService.Get(movieId);

            return Ok(new ApiResponse<IEnumerable<ReviewResponse>>
            {
                Data = reviews
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int movieId, [FromRoute] int id)
        {
            var review = await _reviewService.Get(movieId, id);

            return Ok(new ApiResponse<ReviewResponse>
            {
                Data = review
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] int movieId, [FromBody] ReviewRequest request)
        {
            var id = await _reviewService.Add(movieId, request);

            return CreatedAtAction(
                nameof(Get),
                new { movieId = movieId, id = id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int movieId, [FromRoute] int id, [FromBody] ReviewRequest request)
        {
            await _reviewService.Update(movieId, id, request);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int movieId, [FromRoute] int id)
        {
            await _reviewService.Delete(movieId, id);
            return NoContent();
        }
    }
}
