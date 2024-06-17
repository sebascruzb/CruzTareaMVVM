namespace CruzTareaMVVM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.NotePageSC), typeof(Views.NotePageSC));
        }
    }
}
