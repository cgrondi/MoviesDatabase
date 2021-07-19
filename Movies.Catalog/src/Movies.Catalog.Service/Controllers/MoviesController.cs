using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Catalog.Service.Dtos;
using Movies.Catalog.Service.Entities;
using Movies.Catalog.Service.Repositories;

namespace Movies.Catalog.Service.Controllers
{
    [ApiController]
    [Route("movies")] //https:localhost:5001/movies
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetAsync()
        {
            var movies = (await moviesRepository.GetAllAsync())
                        .Select(movie => movie.AsDto());
            return movies;
        }

        [HttpGet("{id}")]  // GET /movies/{id}
        public async Task<ActionResult<MovieDto>> GetByIdAsync(Guid id)
        {
            var item = await moviesRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpGet("/genre={genre}")]  // GET /movies/{genre}
        public async Task<ActionResult<IAsyncEnumerable<MovieDto>>> GetByGenreAsync(string genre)
        {
            var movies = (await moviesRepository.GetAsync(genre))
                        .Select(movie => movie.AsDto());

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpPost] //POST /movies
        public async Task<ActionResult<MovieDto>> PostAsync(CreateMovieDto createMovieDto)
        {
            //take createMovieDto.Genres and capitalize each string
            List<string> capitalizedGenres = createMovieDto.Genres.ConvertAll(d => char.ToUpper(d[0]) + d.Substring(1));
            var movie = new Movie
            {
                Title = createMovieDto.Title,
                Genres = capitalizedGenres,
                Description = createMovieDto.Description,
                NumberOwned = createMovieDto.NumberOwned,
                NumberAvailableToRent = createMovieDto.NumberAvailableToRent,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await moviesRepository.CreateAsync(movie);


            return CreatedAtAction(nameof(GetByIdAsync), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]  //PUT /movies/{id}
        public async Task<IActionResult> PutAsync(Guid id, UpdateMovieDto updateMovieDto)
        {
            var exhistingMovie = await moviesRepository.GetAsync(id);

            if (exhistingMovie == null)
            {
                return NotFound();
            }

            List<string> capitalizedGenres = updateMovieDto.Genres.ConvertAll(d => char.ToUpper(d[0]) + d.Substring(1));

            exhistingMovie.Title = updateMovieDto.Title;
            exhistingMovie.Genres = capitalizedGenres;
            exhistingMovie.Description = updateMovieDto.Description;
            exhistingMovie.NumberOwned = updateMovieDto.NumberOwned;
            exhistingMovie.NumberAvailableToRent = updateMovieDto.NumberAvailableToRent;

            await moviesRepository.UpdateAsync(exhistingMovie);

            return NoContent();
        }

        [HttpDelete("{id}")] //DELETE /movies/{id}
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var movie = await moviesRepository.GetAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            await moviesRepository.RemoveAsync(movie.Id);

            return NoContent();
        }
    }
}