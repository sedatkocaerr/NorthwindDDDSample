using EventStore.ClientAPI;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.WorkerServices
{
    public class CreateAndUpdatePointDocumentIndex
    {
        private readonly IElasticSearchService _elasticSearchService;

        public CreateAndUpdatePointDocumentIndex(
            IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public async Task<Position> addPointDocument(string indexname,string aliasname,string positionname)
        {
            var position = new Position();
            if (!await _elasticSearchService.CheckIndex(indexname))
            {
                await _elasticSearchService.CretaeIndex<CheckPointEventDocument>(indexname, aliasname);
                position = Position.Start;
                await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>(indexname, aliasname,
                    new CheckPointEventDocument(positionname, position));
            }
            else
            {
                var response = await _elasticSearchService.SimpleSearchAsync<CheckPointEventDocument>(indexname,
                    new Nest.SearchDescriptor<CheckPointEventDocument>().Query(x => x.Term(p => p.Key, 1)));
                if (response.Documents.Count >= 1)
                    position = response.Documents.First().Position.Value;

            }
            return position;
        }

        public async Task UpdatePointDocument(string indexname, string aliasname, string positionname, Position position)
        {

            await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>(indexname,
                aliasname, new CheckPointEventDocument(positionname, position));
        }
    }
}
