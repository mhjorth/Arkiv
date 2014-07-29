using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using System.Linq.Expressions;
using Gtk;

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

        public void RegisterEvents(EventHandler selectionChangedEvent){
            ArtistSelectionChanged += selectionChangedEvent;
        }
        public void FindAll(){
           _artist = _artistCollection.FindAll ().ToList ().OrderBy (x => x.name);
            if (ArtistSelectionChanged != null) {
                ArtistSelectionChanged (this.ArtistSelection, EventArgs.Empty);
            }
        }

        public void Find(Expression<Func<Artist,bool>> expr){
            var q = Query<Artist>.Where(expr);
            _artist = _artistCollection.Find (q).ToList ().OrderBy (x => x.name);
        }

        public int Count(){
            return _artist.Count ();
        }

        public IEnumerable<Artist> ArtistSelection{
            get {return _artist;}
        }

        public void ArtistQueryActivated(object o, EventArgs a)
        {
            var query = (o as Entry).Text;
            Console.WriteLine ("QueryActivated");
            Expression<Func<Artist,bool>> expr = null;
            if (query.Contains ("%")) {
                var parts = query.Split ('%');
                for (int i = 0; i < parts.Count(); i++) {
                    Expression<Func<Artist,bool>> partExpr;
                    var part = parts [i];
                    if (!string.IsNullOrWhiteSpace (part)) {
                        if (i == 0) {
                            partExpr = x => x.name.StartsWith (part);
                        } else if (i == parts.Count () - 1) {
                            partExpr = x => x.name.EndsWith (part);
                        } else {
                            partExpr = x => x.name.Contains (part);
                        }
                        if (expr == null) {
                            expr = partExpr;
                        } else {
                            expr = Expression.Lambda<Func<Artist,bool>> (Expression.And (expr.Body, partExpr.Body), expr.Parameters);
                        }
                    }
                }
            } else if (!string.IsNullOrWhiteSpace(query)) {
                expr = x => x.name == query;
            }
            if (expr == null) {
                FindAll ();
            } else {
                Find (expr);
            }
            if (ArtistSelectionChanged != null) {
                ArtistSelectionChanged (this.ArtistSelection, EventArgs.Empty);
            }
        }

        public event EventHandler ArtistSelectionChanged;

    }

}

