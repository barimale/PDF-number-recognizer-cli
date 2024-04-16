using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using TR.Engine.Contract;
using static System.Net.Mime.MediaTypeNames;

namespace TR.Engine.Services
{
    public class ToDbService : IToDbService
    {
        private readonly ElasticsearchClient _client;
        public ToDbService(Uri uri)
        {
            _client = new ElasticsearchClient(uri);
        }

        public async Task<bool> AddEntity<T>(T content, Id fileName)
        {
            var response = await _client.IndexAsync<T>(content, fileName);

            if (response.IsValidResponse)
            {
                Console.WriteLine($"Index document with ID {response.Id} succeeded.");
            }

            return response.IsValidResponse;
        }
    }
}
