using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReviewsController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet("movies/{movieId}/reviews")]
        public async Task<ActionResult<List<ReviewDto>>> GetMovieReviews(int movieId)
        {
            if (!await _context.Movies.AnyAsync(movie => movie.ID == movieId))
            {
                return NotFound();
            }

            return await _context.Reviews
                .Where(review => review.MovieId == movieId)
                .Select(review => new ReviewDto
                {
                    Id = review.ID,
                    ReviewerName = review.ReviewerName,
                    Comment = review.Comment,
                    Rating = review.Rating
                })
                .ToListAsync();
        }

        [HttpPost("movies/{movieId}/reviews")]
        public async Task<ActionResult<ReviewDto>> PostMovieReview(
            int movieId, ReviewCreateDto input)
        {
            if (!await _context.Movies.AnyAsync(movie => movie.ID == movieId))
            {
                return NotFound();
            }

            var review = new Review
            {
                MovieId = movieId,
                ReviewerName = input.ReviewerName,
                Comment = input.Comment,
                Rating = input.Rating
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var result = new ReviewDto
            {
                Id = review.ID,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating
            };

            return StatusCode(201, result);
        }

        [HttpDelete("reviews/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}