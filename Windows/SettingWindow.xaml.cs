using Classes;
using System.Collections.Generic;
using System.Windows;

namespace Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private static readonly List<string> ThemeList = new List<string> {
            "Система",
            "Светлая",
            "Тёмная"
        };

        public SettingWindow()
        {
            InitializeComponent();
            ThemeList.ForEach(theme => ThemeListBox.Items.Add(theme));
        }

        private void ThemeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Appearance.SetTheme = Appearance.ThemeStrToTheme(ThemeListBox.SelectedValue.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            ThemeListBox.SelectedValue = Appearance.ThemeToThemeStr(Appearance.SelectedTheme);
        }
    }
}
