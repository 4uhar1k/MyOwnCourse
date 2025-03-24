namespace MyOwnCourseApp.Views;

public partial class MyCoursesPage : ContentPage
{
	public MyCoursesPage()
	{
		InitializeComponent();
	}

	public async void CreateCourse(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddCoursePage());
	}
}