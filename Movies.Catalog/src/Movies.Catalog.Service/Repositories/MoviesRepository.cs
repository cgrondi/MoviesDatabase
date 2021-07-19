using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Movies.Catalog.Service.Entities;

namespace Movies.Catalog.Service.Repositories
{

    public class MoviesRepository : IMoviesRepository
    {
        private const string collectionName = "movies";
        private readonly IMongoCollection<Movie> dbCollection;
        private readonly FilterDefinitionBuilder<Movie> filterBuilder = Builders<Movie>.Filter;

        public MoviesRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Movie>(collectionName);
        }

        public async Task<IReadOnlyCollection<Movie>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Movie> GetAsync(Guid id)
        {
            FilterDefinition<Movie> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Movie>> GetAsync(string genre)
        {
            //filter where entity.Genres contains a string genre that has the first letter forced capital
            FilterDefinition<Movie> filter = filterBuilder.Where(entity => entity.Genres.Contains(char.ToUpper(genre[0]) + genre.Substring(1)));

            // bool contains = title.Contains("string", StringComparison.OrdinalIgnoreCase);
            //  Contains(genre, StringComparison.OrdinalIgnoreCase)
            return await dbCollection.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Movie entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Movie entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Movie> filter = filterBuilder.Eq(exhistingEntity => exhistingEntity.Id, entity.Id);

            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Movie> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}