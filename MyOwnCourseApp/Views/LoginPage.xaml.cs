//using Java.Nio.Channels;
using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using MyOwnCourseApp.ViewModels;
using SQLite;

namespace MyOwnCourseApp;

public partial class LoginPage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
	private readonly SqlConnectionBase _connection;
	private readonly ISQLiteAsyncConnection _database;
	private SignUpNInViewModel _thisContext;
	public LoginPage(MOCApiClientService apiClient)
	{
		_apiClient = apiClient;
		_connection = new SqlConnectionBase();
		_database = _connection.CreateConnection();
		_thisContext = new SignUpNInViewModel(_apiClient);
		BindingContext = _thisContext;
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
			await Navigation.PopModalAsync(false);
		}
		else
		{
			await DisplayAlert("", "No user found", "OK");
		}
		//await DisplayAlert("", CallBackLabel.Text, "OK");
		//if (CallBackLabel.Text != "User not found")
		//{
		//	await Navigation.PopModalAsync(false);
		//}
	}
	public async void CreateUser(object sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new SignUpPage(_apiClient), false);
	}
}