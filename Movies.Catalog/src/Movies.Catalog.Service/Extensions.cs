using Movies.Catalog.Service.Dtos;
using Movies.Catalog.Service.Entities;

namespace Movies.Catalog.Service
{
    public static class Extensions
    {
        public static MovieDto AsDto(this Movie movie)
        {
            return new MovieDto(movie.Id, movie.Title, movie.Genres, movie.Description, movie.NumberOwned, movie.NumberAvailableToRent, movie.CreatedDate);
        }
    }
}