namespace CruzTareaMVVM.Views;

public partial class AboutPageSC : ContentPage
{
	public AboutPageSC()
	{
        InitializeComponent();
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.AboutSC about)
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }
    }
}