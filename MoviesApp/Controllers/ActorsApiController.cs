using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services.Actor;
using MoviesApp.Services.Actor.Dto;

namespace MoviesApp.Controllers;
[Route("api/actors")]
[ApiController]
public class ActorsApiController:ControllerBase
{
    private readonly IActorService _service;

    public ActorsApiController(IActorService service)
    {
        _service = service;
    }
    [HttpGet] // GET: /api/actors
    [ProducesResponseType(200, Type = typeof(IEnumerable<ActorDto>))]  
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<ActorDto>> GetMovies()
    {
        return Ok(_service.GetAllActors());
    }
        
    [HttpGet("{id}")] // GET: /api/actor/5
    [ProducesResponseType(200, Type = typeof(ActorDto))]  
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        var movie = _service.GetActor(id);
        if (movie == null) return NotFound();  
        return Ok(movie);
    }
        
    [HttpPost] // POST: api/movies
    public ActionResult<ActorDto> PostActor(ActorDto inputDto)
    {
        var movie = _service.AddActor(inputDto);
        return CreatedAtAction("GetById", new { id = movie.Id }, movie);
    }
        
    [HttpPut("{id}")] // PUT: api/movies/5
    public IActionResult UpdateActor(int id, ActorDto editDto)
    {
        var actor = _service.UpdateActor(editDto);

        if (actor==null)
        {
            return BadRequest();
        }

        return Ok(actor);
    }
        
    [HttpDelete("{id}")] // DELETE: api/movie/5
    public ActionResult<ActorDto> DeleteMovie(int id)
    {
        var movie = _service.DeleteActor(id);
        if (movie == null) return NotFound();  
        return Ok(movie);
    }
}
