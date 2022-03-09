using BaconLauncher.Settings;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BaconLauncher.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();

            CloseLauncherOnGameStartToggleSwitch.IsOn = SettingsManager.Instance.Settings.CloseLauncherOnGameStart;
            AutoClearGameCacheToggleSwitch.IsOn = SettingsManager.Instance.Settings.AutoClearGameCache;
        }

        private void CloseLauncherOnGameStartToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
                SettingsManager.Instance.Settings.CloseLauncherOnGameStart = toggleSwitch.IsOn;

            SettingsManager.Instance.SaveSettings();
        }

        private void AutoClearGameCacheToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
                SettingsManager.Instance.Settings.AutoClearGameCache = toggleSwitch.IsOn;

            SettingsManager.Instance.SaveSettings();
        }
    }
}
