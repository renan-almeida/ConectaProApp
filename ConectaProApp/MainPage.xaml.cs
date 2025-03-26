namespace ConectaProApp
{
    public partial class MainPage : ContentPage
    {
     
        public MainPage()
        {
            InitializeComponent();
        }

       private async void OnNavigateToLogin(object sender, EventArgs e )
        {
            await Shell.Current.GoToAsync("login");
        }
    }

}
