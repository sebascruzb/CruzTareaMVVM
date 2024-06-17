namespace CruzTareaMVVM.Views;

public partial class AllNoteSC : ContentPage
{
	public AllNoteSC()
    {
        InitializeComponent();

        BindingContext = new Models.AllNotesSC();
    }

    protected override void OnAppearing()
    {
        ((Models.AllNotesSC)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePageSC));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.NoteSC)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(NotePageSC)}?{nameof(NotePageSC.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}