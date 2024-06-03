using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using TR.Engine.Contract;
using static System.Net.Mime.MediaTypeNames;

namespace TR.Engine.Services
{
    public class ToDbService : IToDbService
    {
        private readonly ElasticsearchClient _client;

        public ToDbService(Uri uri, string username, string password)
        {
            var settings = new ElasticsearchClientSettings(uri)
                .Authentication(new BasicAuthentication(username, password));
            settings.ServerCertificateValidationCallback((sender, cert, chain, sslPolicyErrors) => { return true; });

            _client = new ElasticsearchClient(settings);
        }

        public async Task<bool> AddDocument<T>(T content, string indexName)
        {
            var response = await _client.IndexAsync<T>(content, indexName, null, (p) => { }, default);

            return response.IsValidResponse;
        }
    }
}
