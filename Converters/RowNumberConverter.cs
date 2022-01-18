
using System;
using System.Globalization;
using System.Windows.Data;

namespace ECN.Converters
{
    public class RowNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CollectionViewSource collectionView = parameter as CollectionViewSource;

            int counter = 1;
            foreach (object item in collectionView.View)
            {
                if (item == value)
                {
                    return counter.ToString();
                }
                counter++;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
