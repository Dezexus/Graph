﻿using Edges.Pages;
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
            //List<object> graphs = GetAllGraph();
            var window = new VisualizationGraphWindow(((GraphAsAlgebraicStructurePage)FirstGraphObj).Matrix);
            window.Show();
        }

        #endregion

        #region Methods

        private List<object> GetAllGraph() {

            var firstGraph = new List<List<short>>();

            switch (FirstSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    firstGraph = ((AdjacencyMatrixPage)FirstGraphObj).Matrix;
                    break;
                case 1:
                    firstGraph = ((IncidenceMatrixPage)FirstGraphObj).Matrix;
                    break;
                case 2:
                    firstGraph = ((GraphAsAlgebraicStructurePage)FirstGraphObj).Matrix;
                    break;
                default:
                    break;
            }

            var secondGraph = new List<List<short>>();

            switch (SecondSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    secondGraph = ((AdjacencyMatrixPage)SecondGraphObj).Matrix;
                    break;
                case 1:
                    secondGraph = ((IncidenceMatrixPage)SecondGraphObj).Matrix;
                    break;
                case 2:
                    secondGraph = ((GraphAsAlgebraicStructurePage)SecondGraphObj).Matrix;
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
