using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Movies.Catalog.Service.Utilities;

namespace Movies.Catalog.Service.Dtos
{
    public record MovieDto(Guid Id, string Title, List<string> Genres, string Description, int NumberOwned, int NumberAvailableToRent, DateTimeOffset CreatedDate);

    public record CreateMovieDto([Required] string Title, [IsValidGenre][IsNotRepeated] List<string> Genres, [Required] string Description, [Range(1, 10)] int NumberOwned, [Range(0, 10)] int NumberAvailableToRent);

    public record UpdateMovieDto([Required] string Title, [IsValidGenre][IsNotRepeated] List<string> Genres, [Required] string Description, [Range(1, 10)] int NumberOwned, [Range(0, 10)] int NumberAvailableToRent);


}