using Elastic.Clients.Elasticsearch;

namespace TR.Engine.Contract
{
    public interface IToDbService
    {
        Task<bool> AddEntity<T>(T content, Id fileName);
    }
}