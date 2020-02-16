using NewBPMSApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace NewBPMSApp.MyConverters
{

    public class LaborConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var en = (Labor)value;
            Type type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false); if (attrs != null && attrs.Length > 0) return ((System.ComponentModel.DataAnnotations.DisplayAttribute)attrs[0]).Name;
            }
            return en.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
