using Graph.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Edges.Windows
{
    public partial class VisualizationGraphWindow : Window
    {
        private List<List<short>> Matrix;
        private List<EllipseWithNumber> Vertexes = new List<EllipseWithNumber>();
        private List<Line> Lines = new List<Line>();
        private List<Ellipse> Loops = new List<Ellipse>();
        private double Scale = 1;

        public VisualizationGraphWindow(List<List<short>> matrix)
        {
            InitializeComponent();
            Matrix = matrix;
        }

        /*
         Рандомно выбирать координаты и при этом для каждого следующего запрещать координаты в радиосе 100, от предыдщих
         */

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e) {

                    short n = Matrix[0][0];
                    short h = 50;
                    short v = 50;
                    for (short i = 0; i < n; i++) {

                        if (i % 10 == 0 && i != 0) {

                            v += 100;
                            h = 50;
                        }
                        var vertex = new EllipseWithNumber(50, 50, h, v, "#888888", (short)(i+1));
                        Vertexes.Add(vertex);
                        h += 100;
                    }

                    for (short i = 1; i < Matrix.Count; i++) {

                        if (Vertexes[Matrix[i][0] - 1] == Vertexes[Matrix[i][1] - 1]) {

                            double x = (Vertexes[Matrix[i][0] - 1].Margin.Left);
                            double y = (Vertexes[Matrix[i][0] - 1].Margin.Top);

                            Loops.Add(CreateLoop(50, 50, x, y));
                        }

                        var line = new Line { 
                            X1 = Vertexes[Matrix[i][0] - 1].Margin.Left + 25,
                            Y1 = Vertexes[Matrix[i][0] - 1].Margin.Top + 25,
                            X2 = Vertexes[Matrix[i][1] - 1].Margin.Left + 25,
                            Y2 = Vertexes[Matrix[i][1] - 1].Margin.Top + 25,
                            Stroke = new SolidColorBrush(Colors.White),
                            StrokeThickness = 3
                        };
                        Lines.Add(line);
                    }

                    foreach (var line in Lines)
                        Canva.Children.Add(line);
                    foreach (var loop in Loops)
                        Canva.Children.Add(loop);
                    foreach (var ellipse in Vertexes)
                        Canva.Children.Add(ellipse);

                }

        private void Window_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) {

            if (e.Delta > 0) {

                Scale -= -0.02;
                var scaleTransform = new ScaleTransform(Scale, Scale, 50, 50);
                Canva.RenderTransform = scaleTransform;
            } else if (e.Delta < 0) {

                Scale -= +0.02;
                var scaleTransform = new ScaleTransform(Scale, Scale, 50, 50);
                Canva.RenderTransform = scaleTransform;
            }
        }

        #endregion

        #region Methods

        Ellipse CreateLoop(double width, double height, double desiredCenterX, double desiredCenterY) {

            Ellipse ellipse = new Ellipse { Width = width, Height = height };
            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height / 2);
            ellipse.Margin = new Thickness(left, top, 0, 0);
            ellipse.Stroke = new SolidColorBrush(Colors.White);
            ellipse.StrokeThickness = 3;

            return ellipse;
        }




        #endregion

        #region MoveGraph

        private Point? _movePoint;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {

            Cursor = Cursors.Hand;
            _movePoint = e.GetPosition(_Grid);
            _Grid.CaptureMouse();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e) {

            Cursor = Cursors.Arrow;
            _movePoint = null;
            _Grid.ReleaseMouseCapture();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e) {

            if (_movePoint == null)
                return;
            var p = e.GetPosition(this) - (Vector)_movePoint.Value;
            _Grid.Margin = new Thickness(p.X, p.Y, 0, 0);
        }

        #endregion



    }
}
