using System;
using System.Globalization;
using System.Windows.Data;

namespace ArhitecturaCPU.GUI.Helper
{
    class ShortToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var number = (short)value;

            return System.Convert.ToString(number, 2).PadLeft(16, '0');
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            return System.Convert.ToInt16(value.ToString(), 2);
        }

    }
}
