using Nest;
using NorthwindApi.Application.ElasticSearchServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.ElasticSearhServices.Interfaces
{
    public interface IElasticSearchService
    {
        Task CretaeIndex<T>(string IndexName, string aliasName) where T : class, new();

        Task<bool> CheckIndex(string IndexName);

        Task<ISearchResponse<T>> SimpleSearchAsync<T>(string indexName, SearchDescriptor<T> query) where T:class, new();

        Task AddOrUpdateIndex<T>(string IndexName, string aliasName, T model) where T:class ,new();

        Task<T> GetId<T>(string IndexName, Guid id) where T : class, new();

        Task Delete<T>(string IndexName, Guid Id) where T : class, new();
    }
}
