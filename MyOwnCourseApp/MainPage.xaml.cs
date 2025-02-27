using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;

namespace MyOwnCourseApp
{
    public partial class MainPage : ContentPage
    {
        private readonly MOCApiClientService _apiClient;
        private readonly SqlConnectionBase _connection;
        private readonly ISQLiteAsyncConnection _database;
        public List<UserDto> Users { get; set; }

        public MainPage(MOCApiClientService apiClient)
        {
            _apiClient = apiClient;
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
            Users = new List<UserDto>();
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            /* var allusers = await _apiClient.GetUsers();
             if (allusers !=null)
             {
                 foreach (var user in allusers)
                 {
                     Users.Add(user);
                 }
             }*/
            UserDto testlocaluser = new UserDto() { Id = 1, Login = "vovas0712", Password = "111", Name = "Vladimir", Surname = "Vassilyev", Role = 0 };
            await _database.InsertAsync(testlocaluser);

            UsersCollection.ItemsSource = await _database.Table<UserDto>().ToListAsync();
        }
    }

}
