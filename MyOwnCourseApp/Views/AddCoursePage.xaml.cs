using MyOwnCourseApiClient;
using MyOwnCourseApp.ViewModels;

namespace MyOwnCourseApp.Views;

public partial class AddCoursePage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
    public CourseViewModel thisContext { get; set; } 
    public AddCoursePage(MOCApiClientService apiClient)
	{
		InitializeComponent();
        _apiClient = apiClient;
        thisContext = new CourseViewModel(_apiClient);
        BindingContext = thisContext;
    }
    public async void AddCourseClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}