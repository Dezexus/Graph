using Classes;
using Pages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Windows
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
        private DispatcherTimer IsVisualizationBtnTimer; //Отвечает за проверку доступности VisualizationBtn

        public MainWindow()
        {
            InitializeComponent();
            foreach (string method in GraphInputMethodList) {
                FirstSelectGraphInputMethod.Items.Add(method);
                SecondSelectGraphInputMethod.Items.Add(method);
            }


            IsVisualizationBtnTimer = new DispatcherTimer();
            IsVisualizationBtnTimer.Tick += new EventHandler(IsVisualizationBtn);
            IsVisualizationBtnTimer.Interval = TimeSpan.FromMilliseconds(100);
            IsVisualizationBtnTimer.Start();
        }

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            Setting.LoadSetting();
        }

        private void Window_Closed(object sender, System.EventArgs e) {

            Setting.SaveSetting();
        }

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

        private void SettingBtn_Click(object sender, RoutedEventArgs e) {

            var settingWindow = new SettingWindow();
            settingWindow.Owner = this;
            settingWindow.ShowDialog();
        }

        #endregion

        #region Methods

        private List<Graph> GetAllGraph() {

            var firstGraph = new Graph();

            switch (FirstSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    firstGraph = ((AdjacencyMatrixPage)FirstGraphObj).GetGraph();
                    break;
                case 1:
                    firstGraph = ((IncidenceMatrixPage)FirstGraphObj).GetGraph();
                    break;
                case 2:
                    firstGraph = ((GraphAsAlgebraicStructurePage)FirstGraphObj).GetGraph();
                    break;
                default:
                    break;
            }

            var secondGraph = new Graph();

            switch (SecondSelectGraphInputMethod.SelectedIndex) {

                case 0:
                    secondGraph = ((AdjacencyMatrixPage)SecondGraphObj).GetGraph();
                    break;
                case 1:
                    secondGraph = ((IncidenceMatrixPage)SecondGraphObj).GetGraph();
                    break;
                case 2:
                    secondGraph = ((GraphAsAlgebraicStructurePage)SecondGraphObj).GetGraph();
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

        /// <summary>
        /// Определяет доступность VisualizationBtn
        /// </summary>
        private void IsVisualizationBtn(object sender, EventArgs e) {

            if (FirstGraphObj == null || SecondGraphObj == null) {

                VisualizationBtn.IsEnabled = false;
                return;
            }
                
            List<Graph> graphs = GetAllGraph();
            if (graphs[0] == null || graphs[1] == null) {

                VisualizationBtn.IsEnabled = false;
                return;
            }

            VisualizationBtn.IsEnabled = true;
        }

        #endregion


    }
}
