using Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using UserControls;

namespace Windows
{
    public partial class VisualizationGraphWindow : Window
    {
        private Graph _Graph; //Содержит граф как алгебраическую структуру, где m[0][0] - кол-во вершин
        private List<EllipseWithNumber> Vertexes = new List<EllipseWithNumber>(); //Вершины графа
        private List<Line> Lines = new List<Line>(); //Рёбра графа
        private Dictionary<short, Ellipse> Loops = new Dictionary<short, Ellipse>();//Петли графа (в случаи, если они существуют)

        private double Scale = 1;//Масштаб графа

        public VisualizationGraphWindow(Graph graph)
        {
            InitializeComponent();
            _Graph = graph;
        }

        #region Events
        /// <summary>
        /// Создаёт визуализацию графа при загрузке окна
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e) {

            short n = _Graph.GraphAsAlgebraicStructure[0][0];//Кол-во вершин в графе
            double radius = n * 1 / 4 * 60;
            double center = radius + 50;

            for (short i = 1; i <= n; i++) {//Создаёт массив вершин

                double ang = ((double)i / n * 2.0 * Math.PI) * (180 / Math.PI);
                double x = center + radius * Math.Cos(ang);
                double y = center + radius * Math.Sin(ang);
                var vertex = new EllipseWithNumber(50, 50, x, y, "#888888", i, this);
                Vertexes.Add(vertex);
            }

            for (short i = 1; i < _Graph.GraphAsAlgebraicStructure.Count; i++) {//Создаёт массив линий(рёбер) между вершинами

                if (Vertexes[_Graph.GraphAsAlgebraicStructure[i][0] - 1] == Vertexes[_Graph.GraphAsAlgebraicStructure[i][1] - 1]) {//Если 1-я и 2-я вершина совпадают, то с помощью эллипса создаёться петля

                    double x1 = (Vertexes[_Graph.GraphAsAlgebraicStructure[i][0] - 1].Margin.Left);
                    double y2 = (Vertexes[_Graph.GraphAsAlgebraicStructure[i][0] - 1].Margin.Top);
                    Loops.Add(_Graph.GraphAsAlgebraicStructure[i][0], CreateLoop(50, 50, x1, y2));
                }

                var brush = new SolidColorBrush(Colors.White);

                if ((int)Appearance.SelectedTheme != 1)
                    brush = new SolidColorBrush(Colors.Black); //Кастыль для смены цветов при разных темах, УБРАТЬ!!!

                var line = new Line { //Создание линии от центра 1-й першины, до центра 2-й
                    X1 = Vertexes[_Graph.GraphAsAlgebraicStructure[i][0] - 1].Margin.Left + 25,
                    Y1 = Vertexes[_Graph.GraphAsAlgebraicStructure[i][0] - 1].Margin.Top + 25,
                    X2 = Vertexes[_Graph.GraphAsAlgebraicStructure[i][1] - 1].Margin.Left + 25,
                    Y2 = Vertexes[_Graph.GraphAsAlgebraicStructure[i][1] - 1].Margin.Top + 25,
                    Stroke = brush,
                    StrokeThickness = 3
                };
                Lines.Add(line);
            }

            //Вывод рёбер, петель и вершин на экран
            foreach (var line in Lines)
                Canva.Children.Add(line);
            foreach (var loop in Loops)
                Canva.Children.Add(loop.Value);
            foreach (var ellipse in Vertexes)
                Canva.Children.Add(ellipse);

        }
        /// <summary>
        /// Изменение масштаба графа при прокрутке колеса мыши
        /// </summary>
        private void Window_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) {

            if (e.Delta > 0) {

                Scale -= -0.05;
                var scaleTransform = new ScaleTransform(Scale, Scale, 50, 50);
                Canva.RenderTransform = scaleTransform;
            } else if (e.Delta < 0) {

                Scale -= +0.05;
                var scaleTransform = new ScaleTransform(Scale, Scale, 50, 50);
                Canva.RenderTransform = scaleTransform;
            }
        }

        /// <summary>
        /// Выделение цветов рёбер связанных с вершинной на которую наведён курсор
        /// </summary>
        public void Vertex_MouseEnter(object sender, MouseEventArgs e) {

            var vertex = (EllipseWithNumber)sender;

            for (int i = 0; i < Lines.Count; i++)
                if (_Graph.GraphAsAlgebraicStructure[i + 1][0] == vertex.Number || _Graph.GraphAsAlgebraicStructure[i + 1][1] == vertex.Number)
                    Lines[i].Stroke = new SolidColorBrush(Colors.Red);
                else if (Loops.ContainsKey(vertex.Number))
                    Loops[vertex.Number].Stroke = new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// Снятие выделения цветов рёбер связанных с вершинной на которую наведён курсор
        /// </summary>
        public void Vertex_MouseLeave(object sender, MouseEventArgs e) {

            var vertex = (EllipseWithNumber)sender;

            for (int i = 0; i < Lines.Count; i++)
                if (_Graph.GraphAsAlgebraicStructure[i + 1][0] == vertex.Number || _Graph.GraphAsAlgebraicStructure[i + 1][1] == vertex.Number)
                    Lines[i].Stroke = new SolidColorBrush(Colors.Black);
                else if (Loops.ContainsKey(vertex.Number))
                    Loops[vertex.Number].Stroke = new SolidColorBrush(Colors.Black);
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">Ширина эллипса</param>
        /// <param name="height">Высота эллипса</param>
        /// <param name="desiredCenterX">Координата центра эллипса по оси x</param>
        /// <param name="desiredCenterY">Координата центра эллипса по оси y</param>
        /// <returns>Возвращает эллипс с прозрачной заливкой и закрашенной границей</returns>
        Ellipse CreateLoop(double width, double height, double desiredCenterX, double desiredCenterY) {

            Ellipse ellipse = new Ellipse { Width = width, Height = height };
            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height / 2);
            ellipse.Margin = new Thickness(left, top, 0, 0);
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.StrokeThickness = 3;

            return ellipse;
        }

        #endregion

        #region MoveGraph

        private Point? _movePoint; //Координаты мыши относительно _Grid
        /// <summary>
        /// Меняет вид курсора на Hand и сохраняет координаты мыши относительно _Grid
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {

            Cursor = Cursors.Hand;
            _movePoint = e.GetPosition(_Grid);
            _Grid.CaptureMouse();
        }
        /// <summary>
        /// Меняет вид курсора на Arrow и удаляет координаты мыши относительно _Grid
        /// </summary>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e) {

            Cursor = Cursors.Arrow;
            _movePoint = null;
            _Grid.ReleaseMouseCapture();
        }
        /// <summary>
        /// Изменяет коордиты _Grid в след за координатами мыши
        /// </summary>
        private void Window_MouseMove(object sender, MouseEventArgs e) {

            if (_movePoint == null)
                return;
            var p = e.GetPosition(this) - (Vector)_movePoint.Value;
            _Grid.Margin = new Thickness(p.X, p.Y, 0, 0);
        }



        #endregion


    }
}
