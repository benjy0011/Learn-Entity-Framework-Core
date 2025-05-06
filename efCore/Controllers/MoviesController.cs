using efCore.API.Data;
using efCore.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{

    private readonly MoviesContext _context;

    public MoviesController(MoviesContext context) 
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        //throw new NotImplementedException();
        return Ok(await _context.Movies.ToListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        //var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        //var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

        //var movie = await _context.Movies.FindAsync(id);

        var movie = await _context.Movies
            .Include(movie => movie.Genre)
            .SingleOrDefaultAsync(m => m.Id == id);


        return movie == null
            ? NotFound() 
            : Ok(movie);
    }

    [HttpGet("by-year/{year:int}")]
    [ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByYear([FromRoute] int year)
    {
        // IQueryable only executes when asking for result (exp: ToList(), FirstOrDefault() etc.)
        // Ienumerable is diff, it is a real list, unlike IQueryable will kinda translate to SQL

        //var allMovies = _context.Movies;
        //IQueryable<Movie> allMovies = _context.Movies; // define only, but not execute yet

        //var filteredMovies = allMovies.Where(m => m.ReleaseDate.Year == year);
        //IQueryable<Movie> filteredMovies = allMovies.Where(m => m.ReleaseDate.Year == year);


        // Different way of writing, same meaning

        //var filteredMovies = _context.Movies
        //    .Where(m => m.ReleaseDate.Year == year);

        //var filteredMovies = 
        //    from movie in _context.Movies
        //    where movie.ReleaseDate.Year == year
        //    select movie;


        //List<MovieTitle> filteredTitles = new();

        //foreach (var movie in filteredMovies)
        //{
        //    filteredTitles.Add(new MovieTitle { Id = movie.Id, Title = movie.Title });
        //}


        // Using projection to get data needed only
        // more efficient query
        var filteredTitles = await _context.Movies
            .Where(movie => movie.ReleaseDate.Year == year)
            .Select(movie => new MovieTitle { Id = movie.Id, Title = movie.Title })
            .ToListAsync();


        //return Ok(await filteredMovies.ToListAsync());

        return Ok(filteredTitles);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        await _context.Movies.AddAsync(movie);

        // movie has no ID
        await _context.SaveChangesAsync();
        // movie has an ID after SaveChanges

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
    {
        var existingMovie = await _context.Movies.FindAsync(id);

        if (existingMovie is null)
        {
            return NotFound();
        }

        existingMovie.Title = movie.Title;
        existingMovie.Synopsis = movie.Synopsis;
        existingMovie.ReleaseDate = movie.ReleaseDate;

        await _context.SaveChangesAsync();

        return Ok(existingMovie);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var existingMovie = await _context.Movies.FindAsync(id);

        if (existingMovie is null)
        {
            return NotFound();
        }

        _context.Movies.Remove(existingMovie); // this one is the cleanest
        //_context.Remove(existingMovie);
        //_context.Movies.Remove(new Movie { Id = id }); // this one can ignore finding that record

        await _context.SaveChangesAsync();

        return Ok();
    }
}