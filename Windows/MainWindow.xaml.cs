using Edges.Pages;
using Edges.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Classes;

namespace Edges
{

    public partial class MainWindow : Window
    {
        
        
        private static readonly List<string> GraphInputMethodList = new List<string> {
            "Матрица смежности",
            "Матрица инцидентности",
            "Алгебраическая структура"
        };
        private object FirstGraphObj;
        private object SecondGraphObj;

        public MainWindow()
        {
            InitializeComponent();
            foreach (string method in GraphInputMethodList) {
                FirstSelectGraphInputMethod.Items.Add(method);
                SecondSelectGraphInputMethod.Items.Add(method);
            }
        }

        #region Events

        private void FirstSelectGraphInputMethod_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            switch (FirstSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    FirstFrame.Content = FirstGraphObj = new AdjacencyMatrixPage();
                    break;
                case 1:
                    FirstFrame.Content = FirstGraphObj = new IncidenceMatrixPage();
                    break;
                case 2:
                    FirstFrame.Content = FirstGraphObj = new GraphAsAlgebraicStructurePage();
                    break;

                default:
                    break;
            }
        }

        private void SecondSelectGraphInputMethod_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            switch (SecondSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    SecondFrame.Content = SecondGraphObj = new AdjacencyMatrixPage();
                    break;
                case 1:
                    SecondFrame.Content = SecondGraphObj = new IncidenceMatrixPage();
                    break;
                case 2:
                    SecondFrame.Content = SecondGraphObj = new GraphAsAlgebraicStructurePage();
                    break;
                default:
                    break;
            }
        }

        private void VisualizationBtn_Click(object sender, RoutedEventArgs e) {
            List<Graph> graphs = GetAllGraph();
            var window = new VisualizationGraphWindow(graphs[0] + graphs[1]);
            window.Show();
        }

        #endregion

        #region Methods

        private List<Graph> GetAllGraph() {

            var firstGraph = new Graph();

            switch (FirstSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    firstGraph = Graph.AdjacencyMatrixToGraph(((AdjacencyMatrixPage)FirstGraphObj).Matrix);
                    break;
                case 1:
                    firstGraph = Graph.IncidenceMatrixToGraph(((IncidenceMatrixPage)FirstGraphObj).Matrix);
                    break;
                case 2:
                    firstGraph = new Graph(((GraphAsAlgebraicStructurePage)FirstGraphObj).Matrix);
                    break;
                default:
                    break;
            }

            var secondGraph = new Graph();

            switch (SecondSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    secondGraph = Graph.AdjacencyMatrixToGraph(((AdjacencyMatrixPage)SecondGraphObj).Matrix);
                    break;
                case 1:
                    secondGraph = Graph.IncidenceMatrixToGraph(((IncidenceMatrixPage)SecondGraphObj).Matrix);
                    break;
                case 2:
                    secondGraph = new Graph(((GraphAsAlgebraicStructurePage)SecondGraphObj).Matrix);
                    break;
                default:
                    break;
            }

            var graphs = new List<Graph> {
                firstGraph,
                secondGraph
            };

            return graphs;
        }


        #endregion


    }
}
