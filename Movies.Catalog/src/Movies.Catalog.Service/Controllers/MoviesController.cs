using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies.Catalog.Service.Dtos;

namespace Movies.Catalog.Service.Controllers
{
    [ApiController]
    [Route("movies")] //https:localhost:5001/movies
    public class MoviesController : ControllerBase
    {
        //temporary list instead of database
        private static readonly List<MovieDto> movies = new()
        {
            new MovieDto(Guid.NewGuid(), "Avengers", new List<string>() { "Action" }, "Superheroes team up to save the day.", DateTimeOffset.UtcNow),
            new MovieDto(Guid.NewGuid(), "The Mighty Ducks", new List<string>() { "Sports", "Comedy" }, "Lawyer has to teach kids hockey after getting a DUI.", DateTimeOffset.UtcNow),
            new MovieDto(Guid.NewGuid(), "Sherlock Holmes", new List<string>() { "Drama", "Thriller", "Comedy", "Mystery" }, "Super detective solves crime mystery.", DateTimeOffset.UtcNow),
            new MovieDto(Guid.NewGuid(), "The Dark Knight", new List<string>() { "Action" }, "Batman saves the day.", DateTimeOffset.UtcNow),
            new MovieDto(Guid.NewGuid(), "Whacky Movie", new List<string>() { "Comedy" }, "Comedy movie made for testing.", DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public IEnumerable<MovieDto> Get()
        {
            return movies;
        }

        [HttpGet("{id}")]  // GET /movies/{id}
        public ActionResult<MovieDto> GetById(Guid id)
        {
            var item = movies.Where(item => item.Id == id).SingleOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("/genre={genre}")]  // GET /movies/{genre}
        public ActionResult<IEnumerable<MovieDto>> GetByGenre(string genre)
        {
            var items = movies.Where(item => item.Genres.Contains(genre, StringComparer.OrdinalIgnoreCase));

            if (!items.Any())
            {
                return NotFound();
            }
            return items.ToList();
        }

        [HttpPost] //POST /movies
        public ActionResult<MovieDto> Post(CreateMovieDto createMovieDto)
        {
            var item = new MovieDto(Guid.NewGuid(), createMovieDto.Title, createMovieDto.Genres, createMovieDto.Description, DateTimeOffset.UtcNow);
            movies.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]  //PUT /movies/{id}
        public IActionResult Put(Guid id, UpdateMovieDto updateMovieDto)
        {
            var exhistingItem = movies.Where(item => item.Id == id).SingleOrDefault();

            if (exhistingItem == null)
            {
                return NotFound();
            }

            var updatedItem = exhistingItem with
            {
                Title = updateMovieDto.Title,
                Genres = updateMovieDto.Genres,
                Description = updateMovieDto.Description
            };

            var index = movies.FindIndex(exhistingItem => exhistingItem.Id == id);
            movies[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id}")] //DELETE /movies/{id}
        public IActionResult Delete(Guid id)
        {
            var index = movies.FindIndex(exhistingItem => exhistingItem.Id == id);

            if (index < 0)
            {
                return NotFound();
            }

            movies.RemoveAt(index);
            return NoContent();
        }
    }
}