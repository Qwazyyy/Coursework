using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Coursework.Methods
{
    public class BackgroundColor
    {
        public static Brush Colors()
        {
            Brush brush = new SolidColorBrush(Color.FromRgb(131, 112, 216));
            return brush;
        }
    }
}
