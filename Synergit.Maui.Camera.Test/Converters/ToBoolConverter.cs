namespace Synergit.Maui.Camera.Test;
public class ToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var result = false;
        var conversionType = (string)parameter;
        switch (conversionType)
        {
            case "Normal":
                result = (bool)(value ?? false);
                break;
            case "NormalNegated":
                result = !(bool)(value ?? false);
                break;
            case "Empty":
                result = value == null || string.IsNullOrEmpty(value.ToString()) || (value is int val && val == 0) || (value is double dbl && dbl == 0) || (value is DateTime tim && tim == DateTime.MinValue);
                break;
            case "NotEmpty":
                result = value != null && !string.IsNullOrEmpty(value.ToString()) && (value is not int i || i != 0) && (value is not double d || d != 0) && (value is not DateTime t || t != DateTime.MinValue);
                break;
            case "IsZeroPlus":
                result = (value is int j) && j > -1;
                break;
            case "NotIsZeroPlus":
                result = (value is int k) && k < 0;
                break;
        }

        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
