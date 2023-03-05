using Edges.Pages;
using Edges.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Edges
{

    public partial class MainWindow : Window
    {
        
        
        private static readonly List<string> GraphInputMethodList = new List<string> {
            "Матрица смежности",
            "Матрица инцидентности",
            "Алгебраическая структура"
        };
        private object LeftGraphObj;
        private object RightGraphObj;

        public MainWindow()
        {
            InitializeComponent();
            foreach (string method in GraphInputMethodList) {
                LeftSelectGraphInputMethod.Items.Add(method);
                RightSelectGraphInputMethod.Items.Add(method);
            }
        }

        #region Events

        private void LeftSelectGraphInputMethod_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            switch (LeftSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    LeftFrame.Content = LeftGraphObj = new AdjacencyMatrixPage();
                    break;
                case 1:
                    LeftFrame.Content = LeftGraphObj = new IncidenceMatrixPage();
                    break;
                case 2:
                    LeftFrame.Content = LeftGraphObj = new GraphAsAlgebraicStructurePage();
                    break;

                default:
                    break;
            }
        }

        private void RightSelectGraphInputMethod_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            switch (RightSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    RightFrame.Content = RightGraphObj = new AdjacencyMatrixPage();
                    break;
                case 1:
                    RightFrame.Content = RightGraphObj = new IncidenceMatrixPage();
                    break;
                case 2:
                    RightFrame.Content = RightGraphObj = new GraphAsAlgebraicStructurePage();
                    break;
                default:
                    break;
            }
        }

        private void VisualizationBtn_Click(object sender, RoutedEventArgs e) {
            //List<object> graphs = GetAllGraph();
            var window = new VisualizationGraphWindow(((GraphAsAlgebraicStructurePage)LeftGraphObj).Matrix);
            window.Show();
        }

        #endregion

        #region Methods

        private List<object> GetAllGraph() {

            var firstGraph = new List<List<short>>();

            switch (LeftSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    firstGraph = ((AdjacencyMatrixPage)LeftGraphObj).Matrix;
                    break;
                case 1:
                    firstGraph = ((IncidenceMatrixPage)LeftGraphObj).Matrix;
                    break;
                case 2:
                    firstGraph = ((GraphAsAlgebraicStructurePage)LeftGraphObj).Matrix;
                    break;
                default:
                    break;
            }

            var secondGraph = new List<List<short>>();

            switch (RightSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    secondGraph = ((AdjacencyMatrixPage)RightGraphObj).Matrix;
                    break;
                case 1:
                    secondGraph = ((IncidenceMatrixPage)RightGraphObj).Matrix;
                    break;
                case 2:
                    secondGraph = ((GraphAsAlgebraicStructurePage)RightGraphObj).Matrix;
                    break;
                default:
                    break;
            }

            var graphs = new List<object> {
                firstGraph,
                secondGraph
            };

            return graphs;
        }


        #endregion


    }
}
