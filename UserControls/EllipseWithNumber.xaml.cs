using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Graph.UserControls
{
    public partial class EllipseWithNumber : UserControl
    {

        public EllipseWithNumber(double _width, double _height, double desiredCenterX, double desiredCenterY, string hexColor, short _number)
        {
            InitializeComponent();
            VertexNumber.Text = _number.ToString();
            Vertex.Width = _width;
            Vertex.Height = _height;
            double left = desiredCenterX - (_width / 2);
            double top = desiredCenterY - (_height / 2);
            UControl.Margin = new Thickness(left, top, 0, 0);
            Vertex.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            Vertex.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            Vertex.StrokeThickness = 3;

        }
    }
}
