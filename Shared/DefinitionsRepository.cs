using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BlazorApp.Shared
{
    public class DefinitionsRepository
    {

        private readonly IMongoCollection<Definition> _definitionsCollection;
        public DefinitionsRepository(MongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration["AZURE_COSMOS_DATABASE_NAME"]);
            _definitionsCollection = database.GetCollection<Definition>("definition");
        }
        public async Task<List<Definition>> GetDefinitionsAsync()
        {
            return await _definitionsCollection.Find(_ => true).ToListAsync();

        }
        public async Task<Definition> GetDefinitionAsync(string id)
        {
            //var query_id = ObjectId.Parse(id);
            return await _definitionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Definition newDefinition)
        {
            await _definitionsCollection.InsertOneAsync(newDefinition);
        }
        public async Task UpdateAsync(string id, Definition updatedDefinition)
        {

            await _definitionsCollection.ReplaceOneAsync(x => x.Id == id, updatedDefinition);
        }
        public async Task RemoveAsync(string id)
        {
            await _definitionsCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}

