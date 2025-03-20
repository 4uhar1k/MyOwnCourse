//using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging.Abstractions;
using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyOwnCourseApp.ViewModels
{
    public class SignUpNInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly MOCApiClientService _apiClient;
        private readonly SqlConnectionBase _connection;
        private readonly ISQLiteAsyncConnection _database;
        public string login, password, name, surname, logincallback;
        public ICommand LogInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public SignUpNInViewModel(MOCApiClientService apiClient)
        {
            _apiClient = apiClient;
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
            LogInCommand = new Command(() =>
            {
                LogInMethod(Login, Password);
            }, () => Login != null & Login != "" & Password != "" & Password != null);
            //LogInCommand = new AsyncRelayCommand(LogInMethod(Login, Password));
            SignUpCommand = new Command(() =>
            {
                SignUpTask(Login, Password, Name, Surname);
            }, ()=> Login != null & Login != "" & Password != "" & Password != null & Name != null & Name != "" & Surname != "" & Surname != null);
        }
        public async Task LogInMethod(string login, string password)
        {
            var SearchedUser = await _apiClient.GetUserByLoginNPassword(Login, Password);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (SearchedUser != null)
                {
                    User FoundUser = SearchedUser;
                    _database.InsertAsync(FoundUser);
                    LoginCallback = $"Welcome, {FoundUser.Name} {FoundUser.Surname}";
                    //await DisplayAlert("", $"Welcome, {FoundUser.Name} {FoundUser.Surname}", "OK");
                    //await Navigation.PopModalAsync(false);
                }
                else
                {
                    LoginCallback = "User not found";
                    //await DisplayAlert("", "No user found", "OK");
                }
            });
            
                
        }
        public async Task SignUpTask(string login, string password, string name, string surname)
        {
            User newUser = new User() {Name = name, Surname = surname , Login = login, Password = password, Role = 1 };
            await _apiClient.PostUser(newUser);
            await _database.InsertAsync(newUser);
        }

        public string Login
        {
            get => login;
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LoginCallback
        {
            get => logincallback;
            set
            {
                if (logincallback != value)
                {
                    logincallback = value;
                    OnPropertyChanged(nameof(LoginCallback));
                }
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)LogInCommand).ChangeCanExecute();
            ((Command)SignUpCommand).ChangeCanExecute();
        }
    }
}
