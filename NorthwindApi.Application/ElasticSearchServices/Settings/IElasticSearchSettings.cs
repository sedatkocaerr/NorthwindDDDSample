using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.ElasticSearchServices.Settings
{
    public interface IElasticSearchSettings
    {
        string ConnectionString { get; }

        string AuthUserName { get; }

        string AuthPassWord { get; }
    }
}
