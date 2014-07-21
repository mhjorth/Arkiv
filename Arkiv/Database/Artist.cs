using System;
using MongoDB.Bson;

namespace Arkiv
{
	public class Artist : IMongoCollection
	{
        public string DbCollectionName { get { return  "artist"; } }
        public ObjectId Id { get; set; }
        public string name { get; set; }
	}
}

