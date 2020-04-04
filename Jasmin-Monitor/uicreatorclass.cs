using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace Jasmin_Monitor
{
    class uicreatorclass
    {
        //this class is a singleton
        private uicreatorclass() { }
        //we create all UI elemnts here
        #region methods
        //creates a canvas on where we put albel and ellipse and all the stuff
        static public Canvas createCanvas(string text, double width, double height, MainWindow main)
        {

            Canvas canvas = new Canvas();
            canvas.Width = width;
            canvas.Height = height / 2;
            Ellipse ellipse1 = CreateEllipse(canvas.Width - 30, canvas.Height, true);
            Canvas.SetTop(ellipse1, canvas.Height / 10);
            Canvas.SetLeft(ellipse1, 0);
            canvas.Children.Add(ellipse1);
            Ellipse ellipse2 = CreateEllipse(canvas.Height / 2, canvas.Height / 2, false);
            Canvas.SetTop(ellipse2, canvas.Height / 3);
            Canvas.SetLeft(ellipse2, canvas.Height / 10);
            canvas.Children.Add(ellipse2);
            main.addtoelipselist(ellipse2);
            Label label = createlabel(text, canvas.Height / 2);
            Canvas.SetTop(label, canvas.Height / 10);
            Canvas.SetLeft(label, canvas.Height / 10 + 2 * canvas.Height / 3);
            canvas.Children.Add(label);
            main.addtolabellist(label);
            return canvas;
        }
        //creates the label where we display values
        static public Label createlabel(string text, double height)
        {
            Label label = new Label();
            label.FontSize = height - 2;
            label.FontStyle = FontStyles.Italic;
            label.FontWeight = FontWeights.SemiBold;
            label.Content = text;
            return label;
        }
        //ellipse will actually be circles 
        static public Ellipse CreateEllipse(double width, double height, bool grey)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = width;
            ellipse.Height = height;
            if (grey)
                ellipse.Fill = new SolidColorBrush(System.Windows.Media.Colors.LightGray);
            else
                ellipse.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
            return ellipse;

        }
        //special label the title gets called separately because it has other structure and sizze
        static public Label createTitle(string text, double width, double height)
        {
            Label label = new Label();
            label.Content = text;
            label.Width = width;
            label.Height = height;
            label.FontSize = height;
            label.FontStyle = FontStyles.Italic;
            label.FontWeight = FontWeights.Bold;

            return label;
        }
        //profile picture is unique and creates a label with background bush set to image
        static public Ellipse createProfilePicture(double radius, Image image)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = radius;
            ellipse.Height = radius;
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = image.Source;
            ellipse.Fill = brush;
            return ellipse;
        }
        //creates the buttons as UI elements
        static public Button createbutton(double width, double height, short choice)
        {
            Button button = new Button();
            button.Width = width;
            button.Height = height;
            switch (choice)
            {
                case 0:
                    button.Background = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    button.Content = "Traffic" + '\n' + "Bun";
                    button.FontSize = width / 3;
                    button.FontWeight = FontWeights.SemiBold;

                    break;
                case 1:
                    button.Background = new SolidColorBrush(System.Windows.Media.Colors.Red);
                    button.Content = "J";
                    button.FontSize = width / 2;
                    button.FontWeight = FontWeights.UltraBold;
                    break;
                case 2:
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("settings.png", UriKind.Relative));
                    var brush = new ImageBrush(img.Source);
                    button.Background = brush;
                    break;
                default:
                    button.Background = new SolidColorBrush(System.Windows.Media.Colors.LightGray);
                    button.Content = "H";
                    button.FontSize = width / 2;
                    button.FontWeight = FontWeights.SemiBold;
                    //button.Click += myButton_Click;
                    break;
            }
            return button;

        }
        //rectangles are used to fil ion the gapsa
        static public Rectangle createRectangle(double width, double height, bool black)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Margin = new Thickness(10, 0, 30, 0);
            if (black)
                rectangle.Stroke = new SolidColorBrush(System.Windows.Media.Colors.Black);
            return rectangle;
        }
        #endregion
    }
}
