using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.ViewModels;

namespace MyOwnCourseApp.Views;

public partial class FollowedCoursesPage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
    public CourseViewModel thisContext { get; set; }
    public FollowedCoursesPage(MOCApiClientService apiClient)
	{
		InitializeComponent();
        _apiClient = apiClient;
        thisContext = new CourseViewModel(_apiClient);
        BindingContext = thisContext;
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