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
			ArkivWindow win = new ArkivWindow ();
			win.Show ();
			var arkiv = new MongoDb ("arkiv");
            var artistCollection = arkiv.getCollection<Artist> ();
            var currentArtists = artistCollection.FindAll ();

/*
            var newName = "ddd";
            newName = "fefe";
            var newArtist = new Artist { 
                name = newName
            };
*/
//			artistCollection.Remove (currentArtistQuery);
			Application.Run ();
		}
	}
}
