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
			var arkiv = new MongoDb ("arkiv");
            var artists = new ArtistService (arkiv);

            artists.FindAll ();
            ArkivWindow win = new ArkivWindow (artists.ArtistSelection);
            win.Show ();

			Application.Run ();
		}
	}
}
