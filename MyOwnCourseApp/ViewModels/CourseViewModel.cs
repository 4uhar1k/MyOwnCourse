using Microsoft.Maui.Controls.Internals;
using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string name, category, statusstring;//,creatorname;
        public bool iscreator, isnotcreator;
        public ObservableCollection<Course> AllCourses { get; set; }
        public ObservableCollection<Course> MyCourses { get; set; }
        public ObservableCollection<Course> FollowedCourses { get; set; }

        public ObservableCollection<User> Users { get; set; }
        public ICommand AddCourseCommand { get; set; }
        public ICommand AddConnectionCommand { get; set; }
        public CourseViewModel(MOCApiClientService apiClient)
        {
            _apiClient = apiClient;
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
            AllCourses = new ObservableCollection<Course>();
            MyCourses = new ObservableCollection<Course>();
            FollowedCourses = new ObservableCollection<Course>();
            LoadAllCourses();
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
                var localUser = await _database.Table<LocalUserDto>().FirstOrDefaultAsync();
                if (localUser != null)
                {
                    Course newCourse = new Course() { Name = Name, Category = Category, Creator = localUser.Id, Status = Status };
                    await _apiClient.PostCourse(newCourse);

                    int lastId = await _apiClient.GetLastId();
                    Connection newCreator = new Connection() { UserId = localUser.Id, CourseId = lastId, Type = 6 };
                    await _apiClient.PostConnection(newCreator);
                }
                
                
            });

            AddConnectionCommand = new Command(async () =>
            {
                var localUser = await _database.Table<LocalUserDto>().FirstOrDefaultAsync();
                if (localUser != null)
                {
                    Connection newFollower = new Connection() { UserId = localUser.Id, CourseId = Id, Type = 7 };
                    await _apiClient.PostConnection(newFollower);
                }
            });
        }
        public async void LoadAllCourses()
        {
            var allcourses = await _apiClient.GetCourses();
            if (allcourses != null)
            {
                //var localUser = await _database.Table<LocalUserDto>().FirstOrDefaultAsync();
                foreach (var course in allcourses)
                {
                    AllCourses.Add(course);
                    /*if (localUser != null)
                    {
                        if (course.Creator == localUser.Id) 
                            MyCourses.Add(course);
                        var connections = await _apiClient.GetConnectionsByCourseId(course.Id);
                        List<Connection> connectionsList = connections.ToList().Where(x => x.UserId == localUser.Id).FirstOrDefault();

                    }*/
                    //User? creator = await _apiClient.GetUserById(course.Creator);
                    //if (creator != null)
                    //{
                    //    CreatorName = $"{creator.Name} {creator.Surname}";
                    //}
                }
            }
            var localUser = await _database.Table<LocalUserDto>().FirstOrDefaultAsync();
            var coursesofcuruser = await _apiClient.GetConnectionsByUserId(localUser.Id);
            if (coursesofcuruser != null)
            {
                foreach (Connection connection in coursesofcuruser)
                {
                    var coursebyid = await _apiClient.GetCourseById(connection.CourseId);
                    if (coursebyid != null)
                    {
                        if (connection.Type == 6) // if current user is a creator of course
                        {
                            MyCourses.Add(coursebyid);
                        }

                        else if (connection.Type == 7)
                        {
                            FollowedCourses.Add(coursebyid);
                        }
                    }
                }
            }
            var allusers = await _apiClient.GetUsers();
            if (allusers!=null)
            {
                await _database.CreateTableAsync<User>();
                await _database.InsertAllAsync(allusers);
            }
            var allroles = await _apiClient.GetRoles();
            if (allroles != null)
            {
                await _database.CreateTableAsync<Role>();
                await _database.InsertAllAsync(allroles);
            }

        }

        public async void GetCurCourseInfo()
        {
            LocalUserDto curUser = await _database.Table<LocalUserDto>().FirstOrDefaultAsync();
            if (curUser != null)
            {
                IsCreator = (curUser.Id == Creator);
                IsNotCreator = !IsCreator;
            }
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
        //public string CreatorName
        //{
        //    get => creatorname;
        //    set
        //    {
        //        creatorname = value;
        //        OnPropertyChanged(nameof(CreatorName));
        //    }
        //}
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
        public bool IsCreator
        {
            get => iscreator;
            set
            {
                if (iscreator != value)
                {
                    iscreator = value;
                    OnPropertyChanged(nameof(IsCreator));
                }
            }
        }
        public bool IsNotCreator
        {
            get => isnotcreator;
            set
            {
                if (isnotcreator != value)
                {
                    isnotcreator = value;
                    OnPropertyChanged(nameof(IsNotCreator));
                }
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
