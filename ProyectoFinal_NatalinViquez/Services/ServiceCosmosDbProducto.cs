using Microsoft.Azure.Cosmos;
using ProyectoFinal_natalinviquez.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_natalinviquez.Services
{

    public interface ICosmosDBService
    {
        Task<IEnumerable<Producto>> GetItemsAsync(string query);
        Task<Producto> GetItemAsync(string id);
        Task AddItemAsync(Producto item);
        Task UpdateItemAsync(string id, Producto item);
        Task DeleteItemAsync(string id);
    }

    public class ServiceCosmosDbProducto : ICosmosDBService
    {
        private Container _container;

        public ServiceCosmosDbProducto(CosmosClient dbClient, string databaseName, string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }


        public async Task AddItemAsync(Producto item)
        {
            await this._container.CreateItemAsync<Producto>(item, new PartitionKey(item.Id));
        }

        public Task DeleteItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Producto> GetItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Producto>> GetItemsAsync(string queryString)
        {

            var query = this._container.GetItemQueryIterator<Producto>(new QueryDefinition(queryString));
            List<Producto> results = new List<Producto>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public Task UpdateItemAsync(string id, Producto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
