using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.ElasticSearchServices
{
    public abstract class BaseEntityElastic
    {
        public Guid Id { get; set; }
    }
}
