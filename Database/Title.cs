using System;
using MongoDB.Bson;

namespace Arkiv
{
	public class Title : IEquatable<Title>
	{
        public string name { get; set; }
        public string note { get; set; }
        public int year { get; set; }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals (this, other))
                return true;
            var entity = other as Title;
            return name == entity.name;
        }

        public bool Equals(Title other)
        {
            return Equals (other as object);
        }

        public override int GetHashCode ()
        {
            return base.GetHashCode ();
        }
	}
}

