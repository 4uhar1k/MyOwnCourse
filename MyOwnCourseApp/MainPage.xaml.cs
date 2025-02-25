using MyOwnCourseApiClient;

namespace MyOwnCourseApp
{
    public partial class MainPage : ContentPage
    {
        private readonly MOCApiClientService _apiClient;

        public MainPage(MOCApiClientService apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            
        }
    }

}
