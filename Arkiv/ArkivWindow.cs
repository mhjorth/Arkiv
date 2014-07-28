using System;
using Gtk;
using GLib;
using System.Collections.Generic;
using System.Linq;

namespace Arkiv
{
    public class ArkivWindow : Window
    {	

        private VBox _vbox1;
        private HBox _artistBox;
        private TextView _artist;
        private TextView _textview1;
        private Table _table1;

        private Artist[] _artistSelection;
        private int _currentArtistIndex;
        private int _artistMaxIndex;


        public ArkivWindow (IEnumerable<Artist> artistSelection) : base(WindowType.Toplevel)
        {
            _artistSelection = artistSelection.ToArray(); 
            _artistMaxIndex = _artistSelection.Count () - 1;
            Build ();
            ShowFirstArtist ();
        }

        private void Build ()
        {
            // Widget ArkivWindow
            Name = "ArkivWindow";
            Title = Mono.Unix.Catalog.GetString ("Arkiv");
            WindowPosition = ((WindowPosition)(4));
            // Container child ArkivWindow.Gtk.Container+ContainerChild
            _vbox1 = new VBox ();
            _vbox1.Name = "vbox1";
            _vbox1.Spacing = 6;

            _artistBox = new HBox (false, 0);
            _vbox1.PackStart (_artistBox, false, false, 5);

            var artistLabel = new Label ("Artist");
            _artistBox.PackStart (artistLabel, false, true, 5);
            var buf = new TextBuffer (new TextTagTable ());
            _artist = new TextView (buf);
            _artist.CanFocus = true;
            _artist.Editable = false;
            _artistBox.PackStart (_artist, false, true, 5);

            _textview1 = new TextView ();
            _textview1.CanFocus = true;
            _textview1.Editable = false;
            _vbox1.PackStart (_textview1, false, false, 5);

            _table1 = new Table (((uint)(5)), ((uint)(3)), false);
            _table1.Name = "table1";
            _table1.RowSpacing = ((uint)(6));
            _table1.ColumnSpacing = ((uint)(6));
            _vbox1.Add (_table1);
            Box.BoxChild w3 = ((Box.BoxChild)(_vbox1 [_table1]));
            w3.Position = 2;
            Add (_vbox1);
            if ((Child != null)) {
                Child.ShowAll ();
            }
            DefaultWidth = 717;
            DefaultHeight = 300;
            Show ();
            DeleteEvent += new DeleteEventHandler (OnDeleteEvent);
            _artist.KeyReleaseEvent += new KeyReleaseEventHandler (ArtistKeyReleaseEvent);
        }

        private void ShowArtist(int index){
            var artist = _artistSelection [_currentArtistIndex];
            _artist.Buffer.Text = artist.name;

            if (string.IsNullOrWhiteSpace (artist.note)) {
                _textview1.Hide ();
            } else {
                _textview1.Buffer.Text = artist.note;
                _textview1.Show ();
            }
        }
        public void ShowFirstArtist ()
        {
            if (_artistMaxIndex < 0) {
                return;
            }
            _currentArtistIndex = 0;
            ShowArtist (_currentArtistIndex);
        }

        public void ShowNextArtist(){
            _currentArtistIndex = _currentArtistIndex < _artistMaxIndex ? _currentArtistIndex + 1 : _artistMaxIndex;
            ShowArtist (_currentArtistIndex);
        }

        public void ShowPreviousArtist(){
            _currentArtistIndex = _currentArtistIndex >= 1 ? _currentArtistIndex - 1 : 0;
            ShowArtist (_currentArtistIndex);
        }

	    protected void OnDeleteEvent (object sender, DeleteEventArgs a)
        {
            Application.Quit ();
            a.RetVal = true;
        }

        protected void ArtistKeyReleaseEvent(object o, KeyReleaseEventArgs a)
        {
            if (a.Event.Key == Gdk.Key.Up) {
                ShowPreviousArtist ();
            }
            if (a.Event.Key == Gdk.Key.Down) {
                ShowNextArtist ();
            }
        }

    }
}
