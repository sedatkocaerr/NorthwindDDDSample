using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindApi.Application.ElasticSearchServices.Settings
{
    public static class CreateElasticSearchClient
    {
        public static ElasticClient GetClient(IElasticSearchSettings elasticSearchSettings)
        {
            var str = elasticSearchSettings.ConnectionString;
            var strs = str.Split('|');
            var nodes = strs.Select(s => new Uri(s)).ToList();

            var connectionString = new ConnectionSettings(new Uri(str))
                .DisablePing()
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false);

            if (!string.IsNullOrEmpty(elasticSearchSettings.AuthUserName) && !string.IsNullOrEmpty(elasticSearchSettings.AuthPassWord))
                connectionString.BasicAuthentication(elasticSearchSettings.AuthUserName, elasticSearchSettings.AuthPassWord);

            return new ElasticClient(connectionString);
        }
    }
}
