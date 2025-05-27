namespace HomeRentApp_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Mostrar login primero
            MainPage = new NavigationPage(new Views.LoginPage());
        }

        // Llamado desde LoginPage después de éxito
        public void GoToMainShell()
        {
            MainPage = new AppShell(); // contiene el TabBar
        }
    }
}
