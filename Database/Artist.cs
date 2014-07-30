using System;
using MongoDB.Bson;

namespace Arkiv
{
	public class Artist : IMongoCollection, IEquatable<Artist>
	{
        public string DbCollectionName { get { return  "artist"; } }
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public string note { get; set; }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals (this, other))
                return true;
            var entity = other as Artist;
            return name == entity.name;
        }

        public bool Equals(Artist other)
        {
            return Equals (other as object);
        }

        public override int GetHashCode ()
        {
            return base.GetHashCode ();
        }
	}
}

