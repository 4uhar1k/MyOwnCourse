using MyOwnCourseApiClient;

namespace MyOwnCourseApp;

public partial class SignUpPage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
	public SignUpPage(MOCApiClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
	}

	public async void LogIn(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}