using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Arkiv
{
    public class ArtistService
    {
        private MongoDb _db;
        private MongoCollection<Artist> _artistCollection;
        private IEnumerable<Artist> _artist = new HashSet<Artist>{};
        public ArtistService (MongoDb db)
        {
            _db = db;
            _artistCollection = _db.getCollection<Artist> ();
        }

        public void FindAll(){
           _artist = _artistCollection.FindAll ().ToList ().OrderBy (x => x.name);
        }

        public int Count(){
            return _artist.Count ();
        }

        public bool Any(){
            return _artist.Any ();
        }

        public IEnumerable<Artist> ArtistSelection{
            get {return _artist;}
        }

    }

}

