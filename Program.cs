using System;
using Gtk;

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
