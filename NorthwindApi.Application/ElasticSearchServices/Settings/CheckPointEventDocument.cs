using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Application.ElasticSearchServices.Settings
{
    public class CheckPointEventDocument:BaseEntityElastic
    {
        public CheckPointEventDocument()
        {

        }
        public CheckPointEventDocument(string key, Position position)
        {
            Key = key;
            Position = position;
        }

        public string Key { get; set; }
        public Position? Position { get; set; }
    }
}
