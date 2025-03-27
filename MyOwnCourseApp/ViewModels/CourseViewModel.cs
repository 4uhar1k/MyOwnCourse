using Microsoft.Maui.Controls.Internals;
using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyOwnCourseApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly MOCApiClientService _apiClient;
        private readonly SqlConnectionBase _connection;
        private readonly ISQLiteAsyncConnection _database;
        public int id, creator, status;
        public string name, category, statusstring;

        public ICommand AddCourseCommand { get; set; }
        public CourseViewModel(MOCApiClientService apiClient)
        {
            _apiClient = apiClient;
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
            AddCourseCommand = new Command(async () =>
            {
                switch(Statusstring)
                {
                    case "Public":
                        Status = 3;
                        break;
                    case "Private":
                        Status = 4;
                        break;
                    case "Available for subscribers":
                        Status = 5;
                        break;

                }
                var localUser = await _database.Table<User>().FirstOrDefaultAsync();
                if (localUser != null)
                {
                    Course newCourse = new Course() { Name = Name, Category = Category, Creator = localUser.Id, Status = Status };
                    await _apiClient.PostCourse(newCourse);
                }
            });
        }
        
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int Creator
        {
            get => creator;
            set
            {
                if (creator != value)
                {
                    creator = value;
                    OnPropertyChanged(nameof(Creator));
                }
            }
        }
        public int Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public string Statusstring
        {
            get => statusstring;
            set
            {
                statusstring = value;
                OnPropertyChanged(nameof(Statusstring));
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
