using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using System.Linq.Expressions;
using Gtk;
using System.Text.RegularExpressions;

namespace Arkiv
{
    public class ArtistService
    {
        private readonly MongoDb _db;
        private readonly MongoCollection<Artist> _artistCollection;
        private IEnumerable<Artist> _artistSelection = new HashSet<Artist>{};

        public ArtistService (MongoDb db)
        {
            _db = db;
            _artistCollection = _db.getCollection<Artist>();
        }

        public void RegisterEvents(EventHandler selectionChangedEvent){
            ArtistSelectionChanged += selectionChangedEvent;
        }

        public void FindAll(){
            _artistSelection = _artistCollection.FindAll().ToList().OrderBy(x => x.name);
            if (ArtistSelectionChanged != null) {
                ArtistSelectionChanged (_artistSelection, EventArgs.Empty);
            }
        }

        public void Find(Expression<Func<Artist,bool>> expr){
            var q = Query<Artist>.Where(expr);
            _artistSelection = _artistCollection.Find(q).ToList().OrderBy(x => x.name);
            if (ArtistSelectionChanged != null) {
                ArtistSelectionChanged (_artistSelection, EventArgs.Empty);
            }
        }

        public void ArtistQueryActivated(object o, EventArgs a)
        {
            var query = (o as Entry).Text;
            Expression<Func<Artist,bool>> expr = null;
            var pattern = "^" + query.Replace('_', '.').Replace("%", ".*") + "$";
            expr = x => Regex.IsMatch(x.name, pattern, RegexOptions.IgnoreCase);
            if (expr == null) {
                FindAll ();
            } else {
                Find (expr);
            }
        }

        public event EventHandler ArtistSelectionChanged;

    }

}

