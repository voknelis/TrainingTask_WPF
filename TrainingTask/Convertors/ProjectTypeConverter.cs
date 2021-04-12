using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrainingTask.Model;

namespace TrainingTask.ViewModel
{
    public class ProjectTypeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            // only of type is Enum
            if (!value.GetType().IsEnum)
                return false;

            // only if type is userRole
            var enumName = value.GetType().FullName;
            if (typeof(ProjectType).FullName != enumName)
                return false;

            return Enum.GetValues(typeof(ProjectType)).Cast<ProjectType>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.ToObject(targetType, System.Convert.ToInt32(value));
            
        }

    }
}
