using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _context.Actors
                .Select(actor => new ActorDto
                {
                    Id = actor.ID,
                    Name = actor.Name,
                    BirthYear = actor.BirthYear
                })
                .ToListAsync();

            return actors;
        }

        [HttpGet("{id}")]
public async Task<ActionResult<ActorDto>> GetActor(int id)
{
    var actor = await _context.Actors
        .Where(actor => actor.ID == id)
        .Select(actor => new ActorDto
        {
            Id = actor.ID,
            Name = actor.Name,
            BirthYear = actor.BirthYear
        })
        .FirstOrDefaultAsync();

    if (actor == null)
    {
        return NotFound();
    }

    return actor;
}
[HttpPost]
public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto input)
{
    var actor = new Actor
    {
        Name = input.Name,
        BirthYear = input.BirthYear
    };

    _context.Actors.Add(actor);
    await _context.SaveChangesAsync();

    var result = new ActorDto
    {
        Id = actor.ID,
        Name = actor.Name,
        BirthYear = actor.BirthYear
    };

    return CreatedAtAction(nameof(GetActor), new { id = result.Id }, result);
}
[HttpPut("{id}")]
public async Task<IActionResult> PutActor(int id, ActorUpdateDto input)
{
    var actor = await _context.Actors.FindAsync(id);

    if (actor == null)
    {
        return NotFound();
    }

    actor.Name = input.Name;
    actor.BirthYear = input.BirthYear;

    await _context.SaveChangesAsync();

    return NoContent();
}

        
    }
}