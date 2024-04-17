using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using TR.Engine.Contract;

namespace TR.Engine.Services
{
    public class ToDbService : IToDbService
    {
        private readonly ElasticsearchClient _client;

        public ToDbService(Uri uri, string username, string password)
        {
            var settings = new ElasticsearchClientSettings(uri)
                .Authentication(new BasicAuthentication(username, password));

            _client = new ElasticsearchClient(settings);
        }

        public async Task<bool> AddDocument<T>(T content)
        {
            var response = await _client.IndexAsync<T>(content);

            return response.IsValidResponse;
        }
    }
}
