using System;
using MongoDB.Bson;

namespace Arkiv
{
	public class Artist
	{
		public ObjectId Id { get; set; }
		public string name { get; set; }
	}
}

