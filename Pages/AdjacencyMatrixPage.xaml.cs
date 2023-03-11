using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pages
{

    public partial class AdjacencyMatrixPage : Page
    {
        private static readonly Regex regex0_9 = new Regex("[^0-9.-]+");//Регулярное выражение для ограничения ввода символами от 0 до 9
        private static readonly Regex regex0_1 = new Regex("[^0-1.-]+");//Регулярное выражение для ограничения ввода символами от 0 до 1
        private int Dimension = 0;//Размерность матрицы 
        public List<List<short>> Matrix { get; private set; } = new List<List<short>>(); //Матрица смежности

        public AdjacencyMatrixPage()
        {
            InitializeComponent();
        }

        #region Events
        /// <summary>
        /// Проверка соотвествует ли введённый символ регулярному выражению regex0_9
        /// </summary>
        private void DimensionBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !DimensionIsTextAllowed(e.Text);
        }
        /// <summary>
        /// Проверка соотвествует ли введённый символ регулярному выражению regex0_1
        /// </summary>
        private void MatrixElement_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !MatrixElementIsTextAllowed(e.Text);
        }
        /// <summary>
        /// Обновляет матрицу смежности после изменения её пользоватем в поле ввода
        /// </summary>
        private void MatrixElement_ValueChanged(object sender, TextChangedEventArgs e) {

            var elements = new List<short>();
            foreach (TextBox item in Graph.Children) { //Сохраняет все элементы матрицы смежности в массив
                
                if (string.IsNullOrEmpty(item.Text))
                    return;
                elements.Add(Convert.ToInt16(item.Text));
            }

            int k = 0;//Номер элемента матрицы смежности
            for (int i = 0; i < Dimension; i++) //Заполнение матрицы смежности новыми данными
                for (int j = 0; j < Dimension; j++)
                    Matrix[i][j] = elements[k++];
        }
        /// <summary>
        /// Пересоздаёт матрицу смежности в случаи изменения её размерности
        /// </summary>
        private void DimensionBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (DimensionBox.Text != "0" && !string.IsNullOrEmpty(DimensionBox.Text))
                CreateIncidenceMatrix();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Проверяет соответствует ли текст регулярному выражению [^0-9.-]+
        /// </summary>
        /// <param name="text">Текст для проверки на соответствие regex</param>
        /// <returns></returns>
        private static bool DimensionIsTextAllowed(string text) => !regex0_9.IsMatch(text);
        /// <summary>
        /// Проверяет соответствует ли текст регулярному выражению [^0-1.-]+
        /// </summary>
        /// <param name="text">Текст для проверки на соответствие regex</param>
        /// <returns></returns>
        private static bool MatrixElementIsTextAllowed(string text) => !regex0_1.IsMatch(text);
        /// <summary>
        /// Создаёт матрицу смежности
        /// </summary>
        private void CreateIncidenceMatrix() {

            Dimension = Convert.ToInt32(DimensionBox.Text);//Получение размерности матрицы
            Graph.Width = 40 * Dimension; //Задание размерности Graph(WrapPanel) таким образом, чтобы на одной строке помещалось кол-во элементов равное размерности 
            Graph.Children.Clear(); //Очистка Graph(WrapPanel) от старой матрицы
            Matrix = new List<List<short>>(); //Создание матрицы смежности заполненой нулями
            for (int i = 0; i < Dimension; i++) {
                Matrix.Add(new List<short>());
                for (int j = 0; j < Dimension; j++)
                    Matrix[i].Add(0);
            }

            //Заполнение Graph(WrapPanel) Текстовами полями, где каждое поле элемент МС
            for (int i = 0; i < Dimension; i++) {
                for (int j = 0; j < Dimension; j++) {

                    var textBox = new TextBox {
                        Width = 40,
                        Height = 30,
                        Text = "0",
                        TextAlignment = TextAlignment.Center,
                        MaxLength = 1,
                    };
                    textBox.SetResourceReference(TextBox.StyleProperty, "TextBoxBase");
                    textBox.PreviewTextInput += MatrixElement_PreviewTextInput; //Подписание Т.Поля на события
                    textBox.TextChanged += MatrixElement_ValueChanged; 
                    Graph.Children.Add(textBox);
                }
            }

            //Создание нумерации строк и столбцов
            ColumnNumbers.Children.Clear();
            RowNumbers.Children.Clear();
            for (int i = 1; i <= Dimension; i++) {

                var textBlock = new TextBlock {
                    Width = 40,
                    Height = 20,
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Center,
                };
                textBlock.SetResourceReference(TextBlock.ForegroundProperty, "TextBlock.Static.Foreground");
                ColumnNumbers.Children.Add(textBlock);

                textBlock = new TextBlock
                {
                    Width = 30,
                    Height = 30,
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Right,
                    Margin = new Thickness(0, 0, 5, 0),
                };
                textBlock.SetResourceReference(TextBlock.ForegroundProperty, "TextBlock.Static.Foreground");
                RowNumbers.Children.Add(textBlock);
            }
        }

        #endregion


    }
}
