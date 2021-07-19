using System;
using System.Collections.Generic;

namespace Movies.Catalog.Service.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set; }
        public string Description { get; set; }
        public int NumberOwned { get; set; }
        public int NumberAvailableToRent { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}