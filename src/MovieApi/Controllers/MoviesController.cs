using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi;
using MovieApi.Models;
using MovieApi.Dtos;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
{
    var movies = await _context.Movies
        .Select(movie => new MovieDto
        {
            Id = movie.ID,
            Title = movie.Title,
            Year = movie.Year,
            Genre = movie.Genre,
            Duration = movie.Duration
        })
        .ToListAsync();

    return movies;
}

[HttpGet("{id}")]
public async Task<ActionResult<Movie>> GetMovie(int id)
{
    var movie = await _context.Movies.FindAsync(id);

    if (movie == null)
    {
        return NotFound();
    }

    return movie;
}

[HttpGet("{id}/details")]
public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
{
    var details = await _context.Movies
        .Where(movie => movie.ID == id)
        .Select(movie => new MovieDetailDto
        {
            Id = movie.ID,
            Title = movie.Title,
            Year = movie.Year,
            Genre = movie.Genre,
            Duration = movie.Duration,
            Details = movie.Details,
            Reviews = movie.Reviews.Select(review => new ReviewDto
            {
                Id = review.ID,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating
            }).ToList(),
            Actors = movie.Actors.Select(actor => new ActorDto
            {
                Id = actor.ID,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (details == null)
    {
        return NotFound();
    }

    return details;
}

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
public async Task<IActionResult> PutMovie(int id, MovieUpdateDto input)
{
    var movie = await _context.Movies.FindAsync(id);

    if (movie == null)
    {
        return NotFound();
    }

    movie.Title = input.Title;
    movie.Year = input.Year;
    movie.Genre = input.Genre;
    movie.Duration = input.Duration;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException) when (!MovieExists(id))
    {
        return NotFound();
    }

    return NoContent();
}

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPost]
public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto input)
{
    var movie = new Movie
    {
        Title = input.Title,
        Year = input.Year,
        Genre = input.Genre,
        Duration = input.Duration
    };

    _context.Movies.Add(movie);
    await _context.SaveChangesAsync();

    var result = new MovieDto
    {
        Id = movie.ID,
        Title = movie.Title,
        Year = movie.Year,
        Genre = movie.Genre,
        Duration = movie.Duration
    };

    return CreatedAtAction(nameof(GetMovie), new { id = result.Id }, result);
}

[HttpPost("{movieId}/actors/{actorId}")]
public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
{
    var movie = await _context.Movies
        .Include(movie => movie.Actors)
        .FirstOrDefaultAsync(movie => movie.ID == movieId);

    if (movie == null)
    {
        return NotFound("Movie not found.");
    }

    var actor = await _context.Actors.FindAsync(actorId);

    if (actor == null)
    {
        return NotFound("Actor not found.");
    }

    if (movie.Actors.Any(existingActor => existingActor.ID == actorId))
    {
        return NoContent();
    }

    movie.Actors.Add(actor);
    await _context.SaveChangesAsync();

    return NoContent();
}

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
