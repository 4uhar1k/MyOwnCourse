using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using MyOwnCourseApp.ViewModels;
using SQLite;

namespace MyOwnCourseApp
{
    public partial class MainPage : ContentPage
    {
        private readonly MOCApiClientService _apiClient;
        private readonly SqlConnectionBase _connection;
        private readonly ISQLiteAsyncConnection _database;
        public CourseViewModel thisContext { get; set; }
        public List<User> Users { get; set; }

        public MainPage(MOCApiClientService apiClient)
        {
            InitializeComponent();
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
            CheckIfLoggedIn();
            _apiClient = apiClient;
            thisContext = new CourseViewModel(_apiClient);
            BindingContext = thisContext;
            Users = new List<User>();
            

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
            //User testlocaluser = new User() { Id = 2, Login = "ivan0712", Password = "111", Name = "Ivan", Surname = "Vassilyev", Role = 0 };
            //await _database.InsertAsync(testlocaluser);

            UsersCollection.ItemsSource = await _database.Table<User>().ToListAsync();
        }

        public async void CheckIfLoggedIn()
        {
            List<LocalUserDto> loggeduser = await _database.Table<LocalUserDto>().ToListAsync();
            if (loggeduser.Count == 0)
                await Navigation.PushModalAsync(new LoginPage(_apiClient));
        }
        public async void LogOut(object sender, EventArgs e)
        {
            await _database.DeleteAllAsync<User>();
            await Navigation.PushModalAsync(new LoginPage(_apiClient), false);
            //await _database.DropTableAsync<User>();
            //await _database.CreateTableAsync<User>();
        }
    }

}
