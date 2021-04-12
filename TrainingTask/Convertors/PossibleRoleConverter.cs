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
    public class PossibleRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                foreach (var i in (List<UserRole>)value)
                    //if (Enum.GetName(typeof(UserRole),i).ToString() == parameter.ToString())
                    if (i.ToString() == parameter.ToString())
                        return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
