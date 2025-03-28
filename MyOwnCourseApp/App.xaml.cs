using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;

namespace MyOwnCourseApp
{
    public partial class App : Application
    {
        private readonly SqlConnectionBase _connection;
        public App(SqlConnectionBase connection)
        {
            InitializeComponent();
            _connection = connection;
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            ISQLiteAsyncConnection database = _connection.CreateConnection();
            await database.CreateTablesAsync<User, LocalUserDto, Role>();
            //await database.CreateTableAsync<LocalUserDto>();
            base.OnStart();
        }
    }
}
