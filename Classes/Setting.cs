using XMLFileSettings;

namespace Classes
{
    class Setting
    {

        public static void LoadSetting() {

            Props props = new Props();

            props.ReadXml();
            Appearance.SetTheme = (Appearance.Themes)props.Fields.selectModeTheme;
            new Appearance();
        }

        public static void SaveSetting() {

            Props props = new Props();

            props.Fields.selectModeTheme = (int)Appearance.SelectedTheme;
            props.WriteXml();
        }

    }
}