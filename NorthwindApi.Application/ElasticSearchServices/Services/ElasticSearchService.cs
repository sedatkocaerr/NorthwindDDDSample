using Nest;
using NorthwindApi.Application.ElasticSearchServices;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.ElasticSearhServices.Services
{
    public class ElasticSearchService : IElasticSearchService
    {

        public IElasticClient ElasticSearchClient { get; set; }
        private readonly IElasticSearchSettings _elasticSearchSettings;
        
        public ElasticSearchService(IElasticSearchSettings elasticSearchSettings)
        {
            _elasticSearchSettings = elasticSearchSettings;
            ElasticSearchClient = CreateElasticSearchClient.GetClient(_elasticSearchSettings);
        }
       

        public async Task CretaeIndex<T>(string IndexName,string aliasName) where T : class ,new()
        {
            var exists = await ElasticSearchClient.Indices.ExistsAsync(IndexName);
            if(exists.Exists)
            {
                return;
            }

            var response = await ElasticSearchClient.Indices.CreateAsync(IndexName,
                    index => index.Map<T>(
                        x => x.AutoMap())
                    .Aliases(a=>a.Alias(aliasName)));

            if (response.Acknowledged)
            {
                return;
            }
            throw new Exception(response.ServerError.Error.Reason);
        }

        public async Task<bool> CheckIndex(string IndexName)
        {
            var exists = await ElasticSearchClient.Indices.ExistsAsync(IndexName);
            if (!exists.Exists)
            {
                return false;
            }

            return true;
        }

        public async Task<ISearchResponse<T>> SimpleSearchAsync<T>(string indexName, SearchDescriptor<T> query) where T : class, new()
        {
            query.Index(indexName);
            var response = await ElasticSearchClient.SearchAsync<T>(query);
            return response;
        }

        public async Task AddOrUpdateIndex<T>(string IndexName, string aliasName, T model) where T : class, new()
        {
            var checkDocument = ElasticSearchClient.DocumentExists(DocumentPath<T>.Id(new Id(model)), d => d.Index(IndexName));

            if(checkDocument.Exists)
            {
                var response = await ElasticSearchClient.UpdateAsync(DocumentPath<T>.Id(new Id(model)), 
                    d => d.Index(IndexName).Doc(model));
                if (response.ServerError != null)
                    throw new Exception("Elastic Server Update Error "+ response.ServerError.Error);
            }
            else
            {
               var response = await ElasticSearchClient.IndexAsync(model, d => d.Index(IndexName));
                if (response.ServerError != null)
                    throw new Exception("Elastic Server Add Error " + response.ServerError.Error);
            }
        }

        public async Task<T> GetId<T>(string IndexName,  Guid id) where T : class, new()
        {
            var getResponse = await ElasticSearchClient.GetAsync<T>(id, g => g.Index(IndexName));

            var fetchedDocument = getResponse.Source;
            return fetchedDocument;
        }

        public async Task Delete<T>(string IndexName, Guid Id) where T : class, new()
        {
            var res = await ElasticSearchClient.DeleteAsync<T>(Id, d => d.Index(IndexName));
            if (res.ServerError != null) 
                throw new Exception("Elastic Server Add Error " + res.ServerError.Error);
        }
    }
}
