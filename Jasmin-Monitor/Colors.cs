using System.Collections;
using System.Windows.Media;

namespace Jasmin_Monitor
{
    //this class is used only to implement the colors of the Traffic bun indicator
    class Colors
    {
        //this class cannot be implemented
        private Colors() { }

        //we list all the useable colors using RGB
        static private SolidColorBrush red = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
        static private SolidColorBrush orangeRed = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 140, 0));
        static private SolidColorBrush yellow = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 0));
        static private SolidColorBrush lightgreen = new SolidColorBrush(System.Windows.Media.Color.FromRgb(140, 140, 255));
        static private SolidColorBrush green = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 175));

        //the methods allow only get for each of them
        static public SolidColorBrush Red { get; }
        static public SolidColorBrush OrangeRed { get; }
        static public SolidColorBrush Yellow { get; }
        static public SolidColorBrush LightGreen { get; }
        static public SolidColorBrush Green { get; }



    }
}
