using Microsoft.Extensions.Configuration;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.ElasticSearchServices
{
    public class ElasticSearchSettings: IElasticSearchSettings
    {
        public  IConfiguration Configuration { get; }
        public ElasticSearchSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public  string ConnectionString { 
            get 
            { 
                return Configuration.GetSection("ElasticSearchOptions:ConnectionString:HostUrls").Value;
            }
        }
        public  string AuthUserName {
            get 
            {
                return Configuration.GetSection("ElasticSearchOptions:ConnectionString:UserName").Value; 
            }
        }
        public string AuthPassWord {
            get
            {
                return Configuration.GetSection("ElasticSearchOptions:ConnectionString:Password").Value; 
            }
        }
    }
}
