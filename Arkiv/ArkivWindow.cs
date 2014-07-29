using System;
using Gtk;
using GLib;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Arkiv
{
    public class ArkivWindow : Window
    {	

        private VBox _vbox1;
        private HBox _artistBox;
        private TextView _artist;
        private Entry _artistQuery;
        private TextView _artistNote;
        private Table _table1;

        private Artist[] _artistSelection;
        private int _currentArtistIndex;
        private int _artistMaxIndex;


        public ArkivWindow () : base(WindowType.Toplevel)
        {
            Build ();
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

            //Artist box
            _artistBox = new HBox (false, 0);
            _vbox1.PackStart (_artistBox, false, false, 5);

            //Artist name
            var artistLabel = new Label ("Artist");
            _artistBox.PackStart (artistLabel, false, true, 5);
            var buf = new TextBuffer (new TextTagTable ());
            _artist = new TextView (buf);
            _artist.CanFocus = true;
            _artist.Editable = false;
            _artistBox.PackStart (_artist, false, true, 5);

            //Query field
            _artistQuery = new Entry ();
            _artistQuery.CanFocus = true;
            _artistQuery.IsEditable = true;
            _artistQuery.WidthRequest = 100;
            _artistBox.PackEnd (_artistQuery, false, true, 5);
            var queryLabel = new Label ("Search");
            _artistBox.PackEnd (queryLabel, false, true, 5);

            //Artist note
            _artistNote = new TextView ();
            _artistNote.CanFocus = true;
            _artistNote.Editable = false;
            _vbox1.PackStart (_artistNote, false, false, 5);

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
        }

        public void RegisterEvents(EventHandler artistQueryActivatedEvent){
            //Local events
            DeleteEvent += new DeleteEventHandler (OnDeleteEvent);
            _artist.KeyReleaseEvent += new KeyReleaseEventHandler (ArtistKeyReleaseEvent);
            //Handlers in other classes
            _artistQuery.Activated += artistQueryActivatedEvent;
        }

        private void ShowArtist(int index){
            if (index < 0 || index > _artistMaxIndex) {
                _artist.Buffer.Clear ();
                _artistNote.Buffer.Clear ();
                return;
            }
            var artist = _artistSelection [_currentArtistIndex];
            _artist.Buffer.Text = artist.name;

            if (string.IsNullOrWhiteSpace (artist.note)) {
                _artistNote.Hide ();
            } else {
                _artistNote.Buffer.Text = artist.note;
                _artistNote.Show ();
            }
        }
        public void ShowFirstArtist ()
        {
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

        public void SetNewArtistSelection(IEnumerable<Artist> artist){
            _artistSelection = artist.ToArray ();
            _artistMaxIndex = _artistSelection.Count () - 1;

            ShowFirstArtist ();
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

        public void ArtistSelectionChangedEvent(object o, EventArgs e)
        {
            SetNewArtistSelection ((o as IEnumerable<Artist>));
            _artist.GrabFocus ();
            _artistQuery.Text = "";
        }

    }
}
