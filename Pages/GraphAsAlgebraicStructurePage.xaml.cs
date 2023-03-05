using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Edges.Pages
{

    public partial class GraphAsAlgebraicStructurePage : Page
    {
        private static readonly Regex regex0_9 = new Regex("[^0-9.-]+");
        private int CountEdges = 0;
        public List<List<short>> Matrix { get; private set; } = new List<List<short>>();

        public GraphAsAlgebraicStructurePage()
        {
            InitializeComponent();
        }

        #region Events
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {

            e.Handled = !TextBoxIsTextAllowed(e.Text);
        }
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if (CountEdgesBox == null || CountVerticesBox == null)
                return;

            if (CountEdgesBox.Text != "0" && !string.IsNullOrEmpty(CountEdgesBox.Text) 
                && CountVerticesBox.Text != "0" && !string.IsNullOrEmpty(CountVerticesBox.Text))
                CreateAdjacencyMatrix();
        }

        #endregion

        #region Methods
        private static bool TextBoxIsTextAllowed(string text) {

            return !regex0_9.IsMatch(text);
        }
        private void CreateAdjacencyMatrix() {

            CountEdges = Convert.ToInt32(CountEdgesBox.Text);
            Edges.Width = 40 * 2;
            Edges.Children.Clear();
            Matrix = new List<List<short>>();
            Matrix.Add(new List<short>());
                Matrix[0].Add(Convert.ToInt16(CountVerticesBox.Text));

            for (int i = 1; i <= CountEdges; i++) {
                Matrix.Add(new List<short>());
                for (int j = 0; j < 2; j++)
                    Matrix[i].Add(0);
            }

            for (int i = 0; i < CountEdges; i++) {
                for (int j = 0; j < 2; j++) {

                    var textBox = new TextBox {
                        Width = 40,
                        Height = 30,
                        Text = "0",
                        TextAlignment = TextAlignment.Center,
                        Style = (Style)FindResource("TextBoxBase")
                };
                    textBox.PreviewTextInput += TextBox_PreviewTextInput;
                    textBox.TextChanged += MatrixElement_ValueChanged;
                    Edges.Children.Add(textBox);
                }
            }
        }

        #endregion
    }
}
