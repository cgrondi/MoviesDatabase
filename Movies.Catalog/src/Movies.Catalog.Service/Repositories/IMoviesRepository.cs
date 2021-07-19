using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Catalog.Service.Entities;

namespace Movies.Catalog.Service.Repositories
{
    public interface IMoviesRepository
    {
        Task CreateAsync(Movie entity);
        Task<IReadOnlyCollection<Movie>> GetAllAsync();
        Task<Movie> GetAsync(Guid id);
        Task<IReadOnlyCollection<Movie>> GetAsync(string genre);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Movie entity);
    }
}