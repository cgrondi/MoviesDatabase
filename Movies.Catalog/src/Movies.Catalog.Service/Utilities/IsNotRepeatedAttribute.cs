using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movies.Catalog.Service.Utilities
{
    public class IsNotRepeatedAttribute : ValidationAttribute
    {
        private List<string> repeatedEntries;


        public string GetErrorMessage() =>
        $"Cannot have repeated genres: {String.Join(" ", repeatedEntries)}.";

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var listOfGenres = value as List<string>;
            bool isNotRepeated = true;

            repeatedEntries = listOfGenres.GroupBy(item => item.ToLower(), StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(y => y.Key)
                    .ToList();

            if (listOfGenres.Count != listOfGenres.Distinct(StringComparer.OrdinalIgnoreCase).Count())
            {

                isNotRepeated = false;
            }
            if (isNotRepeated)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());
        }
    }
}