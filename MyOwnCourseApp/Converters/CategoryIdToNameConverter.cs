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
    public class CategoryIdToNameConverter : IValueConverter
    {
        private readonly SqlConnectionBase _connection;
        private readonly ISQLiteAsyncConnection _database;
        public CategoryIdToNameConverter()
        {
            _connection = new SqlConnectionBase();
            _database = _connection.CreateConnection();
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int CategoryId)
            {
                var SearchedCategory = _database.Table<Role>().Where(x => x.Id == CategoryId).FirstOrDefaultAsync();
                if (SearchedCategory != null)
                {
                    Role FoundCategory = (Role)SearchedCategory.Result;
                    return FoundCategory.Name;
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
