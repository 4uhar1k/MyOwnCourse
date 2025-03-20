using MyOwnCourseApiClient;
using MyOwnCourseApp.ViewModels;

namespace MyOwnCourseApp;

public partial class SignUpPage : ContentPage
{
	private readonly MOCApiClientService _apiClient;
	public SignUpNInViewModel thisContext { get; set; }
	public SignUpPage(MOCApiClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		thisContext = new SignUpNInViewModel(_apiClient);
		BindingContext = thisContext;
	}
    public async void LogIn(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(false);
    }
    public async void SignUp(object sender, EventArgs e)
	{
		//Shell.Current.Navigation.InsertPageBefore(new MainPage(_apiClient), Shell.Current.Navigation.NavigationStack[0]);
		//await Shell.Current.Navigation.PopToRootAsync(false);
		await Navigation.PopModalAsync(false);
        await Navigation.PopModalAsync(false);
    }
	public void CheckIfSame(object sender, TextChangedEventArgs e)
	{
		if (PassEntry.Text != null & PassEntry.Text != "")
		{
            ErrorLabel.IsVisible = (RepeatPassEntry.Text != PassEntry.Text);
            SignUpBtn.IsEnabled = (RepeatPassEntry.Text == PassEntry.Text);

        }
            
    }
}