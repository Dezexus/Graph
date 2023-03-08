using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pages
{

    public partial class GraphAsAlgebraicStructurePage : Page
    {
        private static readonly Regex regex0_9 = new Regex("[^0-9.-]+");//Регулярное выражение для ограничения ввода символами от 0 до 9
        private int CountEdges = 0;//Кол-во рёбер
        public List<List<short>> Matrix { get; private set; } = new List<List<short>>(); //Граф как АС, где M[0][0] - кол-во вершин

        public GraphAsAlgebraicStructurePage()
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
        /// Обновляет АС после изменения её пользоватем в поле ввода
        /// </summary>
        private void MatrixElement_ValueChanged(object sender, TextChangedEventArgs e) {

            var elements = new List<short>();
            foreach (TextBox item in Edges.Children) {

                if (string.IsNullOrEmpty(item.Text))
                    return;
                elements.Add(Convert.ToInt16(item.Text));
            }


            int k = 0;
            for (int i = 1; i <= CountEdges; i++)
                for (int j = 0; j < 2; j++)
                    Matrix[i][j] = elements[k++];
        }
        /// <summary>
        /// Пересоздаёт АС в случаи изменения её кол-ва вершин или рёбер
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (CountEdgesBox == null || CountVerticesBox == null)
                return;

            if (CountEdgesBox.Text != "0" && !string.IsNullOrEmpty(CountEdgesBox.Text) 
                && CountVerticesBox.Text != "0" && !string.IsNullOrEmpty(CountVerticesBox.Text))
                CreateAlgebraicStructure();
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
        /// Создаёт АС
        /// </summary>
        private void CreateAlgebraicStructure() {

            CountEdges = Convert.ToInt32(CountEdgesBox.Text);
            Edges.Width = 40 * 2;//Задание размерности Edges(WrapPanel) таким образом, чтобы на одной строке помещалось 2 элемента
            Edges.Children.Clear();//Очистка Edges(WrapPanel) от старой АС
            Matrix = new List<List<short>> {new List<short>()};
            Matrix[0].Add(Convert.ToInt16(CountVerticesBox.Text));//Сохраняет под индексом [0][0] кол-во вершин

            for (int i = 1; i <= CountEdges; i++) {//Создаёт АС
                Matrix.Add(new List<short>());
                for (int j = 0; j < 2; j++)
                    Matrix[i].Add(0);
            }

            //Заполнение Edges(WrapPanel) Текстовами полями, где каждая пара textbox ребро графа
            for (int i = 0; i < CountEdges; i++) {
                for (int j = 0; j < 2; j++) {

                    var textBox = new TextBox {
                        Width = 40,
                        Height = 30,
                        Text = "0",
                        TextAlignment = TextAlignment.Center,
                        Style = (Style)FindResource("TextBoxBase")
                };
                    textBox.PreviewTextInput += TextBox_PreviewTextInput;//Подписание Т.Поля на события
                    textBox.TextChanged += MatrixElement_ValueChanged;
                    Edges.Children.Add(textBox);
                }
            }
        }

        #endregion
    }
}
