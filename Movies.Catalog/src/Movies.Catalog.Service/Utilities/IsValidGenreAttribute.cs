using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movies.Catalog.Service.Utilities
{
    public class IsValidGenreAttribute : ValidationAttribute
    {
        private readonly List<string> matchList = new()
        {
            "Action",
            "Comedy",
            "Horror",
            "Drama",
            "Romance",
            "Mystery",
            "Historical",
            "Science Fiction",
            "Western",
            "Animation",
            "Kids",
            "Satire",
            "Other"
        };

        public string GetErrorMessage() =>
        $"Any genres have to be one of the following: {String.Join(" ", matchList)}.";

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            foreach (var genre in value as IEnumerable<string>)
            {
                if (!matchList.Contains(genre, StringComparer.OrdinalIgnoreCase))
                // if (!matchList.Contains(genre))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
    }
}