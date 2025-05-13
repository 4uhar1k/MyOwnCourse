using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.ViewModels;

namespace MyOwnCourseApp;

public partial class CourseReviewPage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
    public Course curCourse { get; set; }
    public CourseViewModel thisContext { get; set; }
    public CourseReviewPage()
	{
		InitializeComponent();
        _apiClient = null;
        curCourse = new Course();
        thisContext = new CourseViewModel(_apiClient);
    }

    public CourseReviewPage(MOCApiClientService apiClient, Course selectedCourse)
    {        
        InitializeComponent();
        _apiClient = apiClient;
        curCourse = selectedCourse;
        thisContext = new CourseViewModel(_apiClient);
        thisContext.Id = curCourse.Id;
        thisContext.Name = curCourse.Name;
        thisContext.Category = curCourse.Category;
        thisContext.Creator = curCourse.Creator;
        thisContext.Status = thisContext.Status;
        thisContext.GetCurCourseInfo();
        BindingContext = thisContext;
    }
}