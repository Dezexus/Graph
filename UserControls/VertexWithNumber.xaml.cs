using Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Windows;

namespace UserControls
{
    public partial class VertexWithNumber : UserControl
    {
        private VisualizationGraphWindow _Owner;
        public short Number { get; private set; }


        public VertexWithNumber(double _width, double _height, double desiredCenterX, double desiredCenterY, short _number, VisualizationGraphWindow _owner)
        {
            InitializeComponent();
            _Owner = _owner;
            Number = _number;
            VertexNumber.Text = _number.ToString();
            Vertex.Width = _width;
            Vertex.Height = _height;
            double left = desiredCenterX - (_width / 2);
            double top = desiredCenterY - (_height / 2);
            UControl.Margin = new Thickness(left, top, 0, 0);
            Vertex.SetResourceReference(Ellipse.StrokeProperty, "Edge.Static.Border");
            Vertex.StrokeThickness = 3;

        }

        private void UControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {

            _Owner.Vertex_MouseEnter(sender, e);
        }

        private void UControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {

            _Owner.Vertex_MouseLeave(sender, e);
        }
    }
}
