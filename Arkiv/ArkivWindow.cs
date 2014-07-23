using System;
using Gtk;
using GLib;

public class ArkivWindow : Window
{	

    private VBox _vbox1;
    private HBox _artistBox;
    private Entry _artist;
    private TextView _textview1;
    private Table _table1;

    protected virtual void Build ()
    {
        // Widget ArkivWindow
        Name = "ArkivWindow";
        Title = Mono.Unix.Catalog.GetString ("Arkiv");
        WindowPosition = ((WindowPosition)(4));
        // Container child ArkivWindow.Gtk.Container+ContainerChild
        _vbox1 = new VBox ();
        _vbox1.Name = "vbox1";
        _vbox1.Spacing = 6;

        _artistBox = new HBox (false,0);
        _vbox1.Add (_artistBox);
        Box.BoxChild w0 = ((Box.BoxChild)(_vbox1 [_artistBox]));
//        w0.Position = 0;
//      w0.Expand = false;
//        w0.Fill = false;
//        w0.PackType = PackType.End;

        _artist = new Entry ();
        _artist.CanFocus = true;
        _artist.Name = "Artist";
        _artist.IsEditable = true;
        _artist.InvisibleChar = '●';
        _artist.Text = "srgwsrg!";
        _artistBox.Add (_artist);
        Box.BoxChild w1 = ((Box.BoxChild)(_artistBox [_artist]));
        w1.Position = 0;
        w1.Expand = false;
        w1.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        _textview1 = new TextView ();
        _textview1.CanFocus = true;
        _textview1.Name = "textview1";
        _vbox1.Add (_textview1);
        Box.BoxChild w2 = ((Box.BoxChild)(_vbox1 [_textview1]));
        w2.Position = 1;
        // Container child vbox1.Gtk.Box+BoxChild
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
        _artist.FocusOutEvent += new FocusOutEventHandler (OnArtistFocusOutEvent);
        _artist.FocusInEvent += new FocusInEventHandler (OnArtistFocusInEvent);
    }

    private string _artistNameOnEntry;

	public ArkivWindow (): base (WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

    protected void OnArtistFocusOutEvent (object o, FocusOutEventArgs args)
    {
        var entry = o as Entry;
        Console.WriteLine (_artistNameOnEntry + " " + entry.Text);
    }

    protected void OnArtistFocusInEvent (object o, FocusInEventArgs args)
    {
        var entry = o as Entry;
        _artistNameOnEntry = entry.Text;
    }
}
