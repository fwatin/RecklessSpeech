using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RecklessSpeech.Front.WPF.App.Converters
{
    public class GreenIfNotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {   
                return Brushes.PaleGreen;
            }

            return Brushes.WhiteSmoke;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
