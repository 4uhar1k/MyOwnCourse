using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;

namespace MyOwnCourseApp;

public partial class LoginPage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
	private readonly SqlConnectionBase _connection;
	private readonly ISQLiteAsyncConnection _database;
	public LoginPage(MOCApiClientService apiClient)
	{
		_apiClient = apiClient;
		_connection = new SqlConnectionBase();
		_database = _connection.CreateConnection();
		InitializeComponent();
	}
	public async void LogUser(object sender, EventArgs e)
	{
		var SearchedUser = await _apiClient.GetUserByLoginNPassword(LoginEntry.Text, PassEntry.Text);
		if (SearchedUser != null)
		{
			User FoundUser = SearchedUser;
			await _database.InsertAsync(FoundUser);
			await DisplayAlert("", $"Welcome, {FoundUser.Name} {FoundUser.Surname}", "OK");
            await Navigation.PopAsync();
		}
		else
		{
			await DisplayAlert("", "No user found", "OK");
		}
	}
}