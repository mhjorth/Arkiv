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
			var currentArtistQuery = Query<Artist>.Where (x => x.name == "ddd");
            var currentArtists = artistCollection.Find (currentArtistQuery);
            foreach (var artist in currentArtists) {
                Console.Write (artist.name);
            }
			artistCollection.Remove (currentArtistQuery);
			artistCollection.Insert (newArtist);
			Application.Run ();
		}
	}
}
