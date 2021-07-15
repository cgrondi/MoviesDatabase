using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Movies.Catalog.Service.Utilities;

namespace Movies.Catalog.Service.Dtos
{
    public record MovieDto(Guid Id, string Title, List<string> Genres, string Description, DateTimeOffset CreatedDate);

    public record CreateMovieDto([Required] string Title, [IsValidGenre][IsNotRepeated] List<string> Genres, [Required] string Description);

    public record UpdateMovieDto([Required] string Title, [IsValidGenre][IsNotRepeated] List<string> Genres, [Required] string Description);


}