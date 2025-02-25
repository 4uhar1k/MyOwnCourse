using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;

namespace MyOwnCourseApp
{
    public partial class MainPage : ContentPage
    {
        private readonly MOCApiClientService _apiClient;
        public List<User> Users { get; set; }

        public MainPage(MOCApiClientService apiClient)
        {
            _apiClient = apiClient;
            Users = new List<User>();
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var allusers = await _apiClient.GetUsers();
            if (allusers !=null)
            {
                foreach (var user in allusers)
                {
                    Users.Add(user);
                }
            }
            UsersCollection.ItemsSource = Users;
        }
    }

}
