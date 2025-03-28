using MyOwnCourseApiClient;
using MyOwnCourseApp.ViewModels;

namespace MyOwnCourseApp.Views;

public partial class MyCoursesPage : ContentPage
{
    private readonly MOCApiClientService _apiClient;
	public CourseViewModel thisContext { get; set; }
    public MyCoursesPage(MOCApiClientService apiClient)
	{
		InitializeComponent();
        _apiClient = apiClient;
		thisContext = new CourseViewModel(_apiClient);
		BindingContext = thisContext;
    }

	public async void CreateCourse(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddCoursePage(_apiClient));
	}
}