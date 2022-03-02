using Catalog.API.Configurations;
using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        //private readonly IOptions<DatabaseConfiguration> _databaseConfiguration;
        public CatalogContext(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            //this._databaseConfiguration = databaseConfiguration;

            var client = new MongoClient(databaseConfiguration.Value.ConnectionString);
            var database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);

            this.Products = database.GetCollection<Product>(databaseConfiguration.Value.CollectionName);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
