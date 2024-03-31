using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.ViewModel.Converter
{
    public class RadioButtonToIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            int radioButtonValue = int.Parse(parameter.ToString());
            return intValue == radioButtonValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return int.Parse(parameter.ToString());
            }
            return Binding.DoNothing;
        }
    }
}
