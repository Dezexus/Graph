using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pages
{
    public partial class IncidenceMatrixPage : Page
    {
        private static readonly Regex regex0_9 = new Regex("[^0-9.-]+");//Регулярное выражение для ограничения ввода символами от 0 до 9
        private static readonly Regex regex0_1 = new Regex("[^0-1.-]+");//Регулярное выражение для ограничения ввода символами от 0 до 1
        private int CountVertexes = 0;//Кол-во вершин
        private int CountEdges = 0;//Кол-во рёбер
        public List<List<short>> Matrix { get; private set; } = new List<List<short>>();//Матрица инцидентности

        public IncidenceMatrixPage()
        {
            InitializeComponent();
        }

        #region Events
        /// <summary>
        /// Проверка соотвествует ли введённый символ регулярному выражению regex0_9
        /// </summary>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !TextBoxIsTextAllowed(e.Text);
        }
        /// <summary>
        /// Проверка соотвествует ли введённый символ регулярному выражению regex0_1
        /// </summary>
        private void MatrixElement_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !MatrixElementIsTextAllowed(e.Text);
        }
        /// <summary>
        /// Пересоздаёт матрицу смежности после изменения её пользоватем в поле ввода
        /// </summary>
        private void CountEdgesBoxAndCountVerticesBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (CountEdgesBox == null || CountVerticesBox == null)
                return;

            if (CountVerticesBox.Text != "0" && !string.IsNullOrEmpty(CountVerticesBox.Text)
                && CountEdgesBox.Text != "0" && !string.IsNullOrEmpty(CountEdgesBox.Text))
                    CreateAdjacencyMatrix();
        }
        /// <summary>
        /// Обновляет матрицу смежности после изменения её пользоватем в поле ввода
        /// </summary>
        private void MatrixElement_ValueChanged(object sender, TextChangedEventArgs e) {

            var elements = new List<short>();
            foreach (TextBox item in Graph.Children)//Сохраняет все элементы матрицы инцидентности в массив
                elements.Add(Convert.ToInt16(item.Text));

            int k = 0;//Номер элемента матрицы смежности
            for (int i = 0; i < CountEdges; i++)//Заполнение матрицы смежности новыми данными
                for (int j = 0; j < CountVertexes; j++)
                    Matrix[i][j] = elements[k++];
        }

        #endregion

        #region Methods
        /// <summary>
        /// Проверяет соответствует ли текст регулярному выражению [^0-9.-]+
        /// </summary>
        /// <param name="text">Текст для проверки на соответствие regex</param>
        /// <returns></returns>
        private static bool TextBoxIsTextAllowed(string text) => !regex0_9.IsMatch(text);
        /// <summary>
        /// Проверяет соответствует ли текст регулярному выражению [^0-1.-]+
        /// </summary>
        /// <param name="text">Текст для проверки на соответствие regex</param>
        /// <returns></returns>
        private static bool MatrixElementIsTextAllowed(string text) => !regex0_1.IsMatch(text);
        /// <summary>
        /// Создаёт матрицу инцидетности
        /// </summary>
        private void CreateAdjacencyMatrix() {

            CountVertexes = Convert.ToInt32(CountVertexesBox.Text);
            CountEdges = Convert.ToInt32(CountEdgesBox.Text);
            Graph.Width = 40 * CountEdges;//Задание размерности Graph(WrapPanel) таким образом, чтобы на одной строке помещалось кол-во элементов равное вершинам 
            Graph.Children.Clear();//Очистка Graph(WrapPanel) от старой матрицы
            Matrix = new List<List<short>>();//Создание матрицы инцидентности заполненой нулями
            for (int i = 0; i < CountEdges; i++) {
                Matrix.Add(new List<short>());
                for (int j = 0; j < CountVertexes; j++)
                    Matrix[i].Add(0);
            }

            //Заполнение Graph(WrapPanel) Текстовами полями, где каждое поле элемент МИ
            for (int i = 0; i < CountVertexes; i++) {
                for (int j = 0; j < CountEdges; j++) {

                    var textBox = new TextBox {
                        Width = 40,
                        Height = 30,
                        Text = "0",
                        TextAlignment = TextAlignment.Center,
                        MaxLength = 1,
                        Style = (Style)FindResource("TextBoxBase")
                    };
                    textBox.PreviewTextInput += MatrixElement_PreviewTextInput;
                    textBox.TextChanged += MatrixElement_ValueChanged;
                    Graph.Children.Add(textBox);
                }
            }

            //Создание нумерации столбцов
            ColumnNumbers.Children.Clear();
            for (int i = 1; i <= CountEdges; i++) {

                var textBlock = new TextBlock {
                    Width = 40,
                    Height = 20,
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Center,
                    Foreground = (Brush)FindResource("TextBlock.Static.Foreground")
                };
                ColumnNumbers.Children.Add(textBlock);


            }

            //Создание нумерации строк
            RowNumbers.Children.Clear();
            for (int i = 1; i <= CountVertexes; i++) {

                var textBlock = new TextBlock {
                    Width = 40,
                    Height = 30,
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 5, 0),
                    Foreground = (Brush)FindResource("TextBlock.Static.Foreground")
                };
                RowNumbers.Children.Add(textBlock);
            }
        }

        #endregion

    }
}
