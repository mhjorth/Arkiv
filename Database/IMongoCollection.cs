using System;

namespace Arkiv
{
    public interface IMongoCollection
    {
        string DbCollectionName{ get; }
    }
}

