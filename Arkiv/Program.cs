using System;
using Gtk;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;

namespace Arkiv
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			var arkiv = new MongoDb ("arkiv");
			var artistCollection = arkiv.getCollection<Artist> ();
			var newArtist = new Artist { 
				name= "ddd" 
			};
			var currentArtists = artistCollection.AsQueryable<Artist> () 
				.Where (x => x.name == "ddd");
			//var mongoQuery = ((MongoQueryable<Artist>)currentArtists).GetMongoQuery ();
			//var currentArtistQuery = Query.EQ ("name", "ddd");
			var currentArtistQuery = Query<Artist>.Where (x => x.name == "ddd");
			artistCollection.Remove (currentArtistQuery);
			artistCollection.Insert (newArtist);
			Application.Run ();
		}
	}
}
