using Elastic.Clients.Elasticsearch;
using TR.Engine.Contract;

namespace TR.Engine.Services
{
    public class ToDbService : IToDbService
    {
        private readonly ElasticsearchClient _client;
        private ToDbService(Uri uri)
        {
            _client = new ElasticsearchClient(uri);
        }

        public ToDbService(Uri uri, string apikey)
            : this(uri)
        {

        }

        public ToDbService(Uri uri, string username, string password)
            :this(uri)
        {

        }

        public async Task<bool> AddDocument<T>(T content)
        {
            var response = await _client.IndexAsync<T>(content);

            return response.IsValidResponse;
        }
    }
}
