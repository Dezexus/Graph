using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Edges.Pages
{
    public partial class IncidenceMatrixPage : Page
    {
        private static readonly Regex regex0_9 = new Regex("[^0-9.-]+");
        private static readonly Regex regex0_1 = new Regex("[^0-1.-]+");
        private int CountVertices = 0;
        private int CountEdges = 0;
        public List<List<short>> Matrix { get; private set; } = new List<List<short>>();

        public IncidenceMatrixPage()
        {
            InitializeComponent();
        }

        #region Events

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !TextBoxIsTextAllowed(e.Text);
        }
        private void MatrixElement_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !MatrixElementIsTextAllowed(e.Text);
        }
        private void CountEdgesBoxAndCountVerticesBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (CountEdgesBox == null || CountVerticesBox == null)
                return;

            if (CountVerticesBox.Text != "0" && !string.IsNullOrEmpty(CountVerticesBox.Text)
                && CountEdgesBox.Text != "0" && !string.IsNullOrEmpty(CountEdgesBox.Text))
                    CreateAdjacencyMatrix();
        }
        private void MatrixElement_VelueChanged(object sender, TextChangedEventArgs e) {

            var elements = new List<short>();
            foreach (TextBox item in Graph.Children)
                elements.Add(Convert.ToInt16(item.Text));

            int k = 0;
            for (int i = 0; i < CountVertices; i++)
                for (int j = 0; j < CountEdges; j++)
                    Matrix[i][j] = elements[k++];
        }

        #endregion

        #region Methods

        private static bool TextBoxIsTextAllowed(string text) {

            return !regex0_9.IsMatch(text);
        }
        private static bool MatrixElementIsTextAllowed(string text) {

            return !regex0_1.IsMatch(text);
        }
        private void CreateAdjacencyMatrix() {

            CountVertices = Convert.ToInt32(CountVerticesBox.Text);
            CountEdges = Convert.ToInt32(CountEdgesBox.Text);
            Graph.Width = 40 * CountVertices;
            Graph.Children.Clear();
            Matrix = new List<List<short>>();
            for (int i = 0; i < CountVertices; i++) {
                Matrix.Add(new List<short>());
                for (int j = 0; j < CountEdges; j++)
                    Matrix[i].Add(0);
            }

            for (int i = 0; i < CountVertices; i++) {
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
                    textBox.TextChanged += MatrixElement_VelueChanged;
                    Graph.Children.Add(textBox);
                }
            }

            ColumnNumbers.Children.Clear();
            for (int i = 1; i <= CountVertices; i++) {

                var textBlock = new TextBlock {
                    Width = 40,
                    Height = 20,
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Center,
                    Foreground = (Brush)FindResource("TextBlock.Static.Foreground")
                };
                ColumnNumbers.Children.Add(textBlock);


            }

            RowNumbers.Children.Clear();
            for (int i = 1; i <= CountEdges; i++) {

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
