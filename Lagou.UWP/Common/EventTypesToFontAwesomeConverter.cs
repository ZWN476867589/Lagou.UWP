using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Lagou.UWP.Common {
    public class EventTypesToFontAwesomeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var t = (API.Entities.Company2._EventTypes)value;
            switch (t) {
                case API.Entities.Company2._EventTypes.Data:
                    return (char)0xf080;
                case API.Entities.Company2._EventTypes.Capital:
                    return (char)0xf155;
                case API.Entities.Company2._EventTypes.Member:
                    return (char)0xf007;
                case API.Entities.Company2._EventTypes.Product:
                    return (char)0xf135;
                default:
                    return (char)0xf11d;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
