using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Classes
{
    /// <summary>
    /// Управляет сменой тем оформления приложения
    /// </summary>
    class Appearance
    {

        #region Field

        private static readonly List<string> lightThemesDictionarys 
            = new List<string>() {

            "Style/Light/ButtonBase.xaml",
            "Style/Light/ComboBoxBase.xaml",
            "Style/Light/Color.xaml",
            "Style/Light/TextBoxBase.xaml",
            "Style/Light/ScrollViewer.xaml"
        };

        private static readonly List<string> darkThemesDictionarys 
            = new List<string>() {

            "Style/Dark/ButtonBase.xaml",
            "Style/Dark/ComboBoxBase.xaml",
            "Style/Dark/Color.xaml",
            "Style/Dark/TextBoxBase.xaml",
            "Style/Dark/ScrollViewer.xaml"
        };

        public enum Themes {
            System,
            Dark,
            Light
        }

        #endregion

        #region Propery

        public static Themes SelectedTheme { get; private set; }

        public static Themes SetTheme {
            set {
                SelectedTheme = value;
                SetCurrentTheme();
            }
        }

        #endregion

        #region Method

        public static void SetCurrentTheme() {

            switch (SelectedTheme) {

                case Themes.Dark:
                    SetDarkTheme();
                    break;
                case Themes.Light:
                    SetLightTheme();
                    break;
                case Themes.System:
                    SetSystemTheme();
                    break;
            }
        }

        public static void SetSystemTheme() {

            switch (GetSystemTheme()) {

                case Themes.Dark:
                    SetDarkTheme();
                    break;
                case Themes.Light:
                    SetLightTheme();
                    break;
                default:
                    SetLightTheme();
                    break;
            }
            SelectedTheme = Themes.System;
        }

        public static void SetDarkTheme() {

            DeleteNotStaticDictionarys();
            foreach (var dictionary in darkThemesDictionarys) {

                var obj = new ResourceDictionary();
                obj.Source = new Uri(dictionary, UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(obj);
            }
            SelectedTheme = Themes.Dark;
        }

        public static void SetLightTheme() {

            DeleteNotStaticDictionarys();
            foreach (var dictionary in lightThemesDictionarys) {

                var obj = new ResourceDictionary();
                obj.Source = new Uri(dictionary, UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(obj);
            }
            SelectedTheme = Themes.Light;
        }

        /// <summary>
        /// Удаляет все словари ресурсов, кроме общих тем
        /// </summary>
        private static void DeleteNotStaticDictionarys() {

            var mergedDictionaries = new List<ResourceDictionary>(Application.Current.Resources.MergedDictionaries);
            var joinList = new List<string>(darkThemesDictionarys);
            joinList.AddRange(lightThemesDictionarys);

            foreach (var dictionary in joinList) {

                var obj = new ResourceDictionary {
                    Source = new Uri(dictionary, UriKind.Relative)
                };

                foreach (var item in mergedDictionaries)
                    if (item.Source == obj.Source)
                        Application.Current.Resources.MergedDictionaries.Remove(item);
            }
        }

        private static Themes GetSystemTheme() {

            string RegistryKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes";
            string theme;
            theme = (string)Registry.GetValue(RegistryKey, "CurrentTheme", string.Empty);
            theme = theme.Split('\\').Last().Split('.').First().ToString();

            switch (theme) {

                case "dark":
                    return Themes.Dark;
                case "light":
                    return Themes.Light;
                default:
                    return Themes.Light;
            }
        }

        public static Themes ThemeStrToTheme(string theme) {

            switch (theme) {
                case "Тёмная":
                    return Themes.Dark;
                case "Светлая":
                    return Themes.Light;
                case "Система":
                    return Themes.System;
            }
            return Themes.System;
        }

        public static string ThemeToThemeStr(Themes theme) {

            switch (theme) {

                case Themes.System:
                    return "Система";
                case Themes.Dark:
                    return "Тёмная";
                case Themes.Light:
                    return "Светлая";
            }
            return "Система";
        }

        #endregion

    }
}
