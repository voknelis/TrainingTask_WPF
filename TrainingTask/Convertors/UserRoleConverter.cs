using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;
using TrainingTask.Model;

namespace TrainingTask.ViewModel
{
    public class UserRoleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // only of type is Enum
            if (!value.GetType().IsEnum)
                return false;

            // only if type is userRole
            var enumName = value.GetType().FullName;
            if (typeof(UserRole).FullName != enumName)
                return false;

            return Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
