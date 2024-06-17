namespace CruzTareaMVVM.Views;

public partial class AllNoteSC : ContentPage
{
    public AllNoteSC()
    {
        InitializeComponent();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}