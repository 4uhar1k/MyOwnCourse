using MyOwnCourseApiClient;

namespace MyOwnCourseApp.Views;

public partial class MyCoursesPage : ContentPage
{
    private readonly MOCApiClientService _apiClient;
    public MyCoursesPage(MOCApiClientService apiClient)
	{
		InitializeComponent();
        _apiClient = apiClient;
    }

	public async void CreateCourse(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddCoursePage(_apiClient));
	}
}