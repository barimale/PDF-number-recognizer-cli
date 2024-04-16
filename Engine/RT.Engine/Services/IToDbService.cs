using Elastic.Clients.Elasticsearch;

namespace TR.Engine.Services
{
    public interface IToDbService
    {
        Task<bool> AddEntity<T>(T content, Id fileName);
    }
}