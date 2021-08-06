using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Models
{
    public enum State
    {
        Active,
        Suspended,
        Terminated
    }
    public class Owner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Channels { get; set; }

        //public State state { get; set; }
        public string State { get; set; }
    }
}
