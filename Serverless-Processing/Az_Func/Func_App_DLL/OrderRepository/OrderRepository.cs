using Contracts;
using Microsoft.Azure.Documents.Client;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository
    {
        private readonly DocumentClient _client;
        const string DatabaseName = "order-db";
        const string CollectionName = "orders";
        public OrderRepository(string endpoint, string key)
        {
            _client = new DocumentClient(new Uri(endpoint), key);
        }

        public async Task AddOrder(OrderDto order)
        {
            await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), order);
        }
    }
}
