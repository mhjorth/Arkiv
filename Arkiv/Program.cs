using System;
using Gtk;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Driver.Builders;
using System.Text;

namespace Arkiv
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
            var win = new ArkivWindow ();
			var arkiv = new MongoDb ("arkiv");
            var artists = new ArtistService (arkiv);
            //Register events
            var artistQueryHandler = new EventHandler (artists.ArtistQueryActivated);
            var artistSelectionChangedHandler = new EventHandler (win.ArtistSelectionChangedEvent);
            win.RegisterEvents (artistQueryHandler);
            artists.RegisterEvents (artistSelectionChangedHandler);

            artists.FindAll ();
            win.Show ();

			Application.Run ();
		}
	}
}
