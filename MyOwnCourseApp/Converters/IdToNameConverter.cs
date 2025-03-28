using CommonServiceLocator;
using MyOwnCourseApiClient;
using MyOwnCourseApiClient.Models;
using MyOwnCourseApiClient.Models.ApiModels;
using MyOwnCourseApp.LocalDatabase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnCourseApp.Converters
{
    public class IdToNameConverter : IValueConverter
    {
        private readonly SqlConnectionBase _connection;
        private readonly ISQLiteAsyncConnection _database;
        public IdToNameConverter()
        {
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int Id)
            {
                var SearchedUser = _database.Table<User>().Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (SearchedUser.Result != null)
                {
                    User FoundUser = SearchedUser.Result;
                    return $"{FoundUser.Name} {FoundUser.Surname}";
                }
                return null;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
