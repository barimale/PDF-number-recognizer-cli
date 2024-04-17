using Elastic.Clients.Elasticsearch;

namespace TR.Engine.Contract
{
    public interface IToDbService
    {
        Task<bool> AddDocument<T>(T content);
    }
}