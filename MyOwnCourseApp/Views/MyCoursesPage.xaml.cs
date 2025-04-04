using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
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
    public async void CourseClicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null)
        {
            Course selectedCourse = (Course)e.CurrentSelection[0];
            await Navigation.PushAsync(new CourseReviewPage(_apiClient, selectedCourse), false);
        }
    }
}