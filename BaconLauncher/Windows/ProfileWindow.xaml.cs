using BaconLauncher.Config;
using BaconLauncher.Windows;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;

namespace BaconLauncher
{
    public partial class ProfileWindow : MetroWindow
    {
        private Profile EditingProfile { get; set; }

        public ProfileWindow(Profile profile = null)
        {
            InitializeComponent();

            EditingProfile = profile;

            if (EditingProfile == null)
            {
                // Creating a profile
                Title = "Create Profile"; 

                // Set default expansion to WoTLK
                ExpansionsComboBox.SelectedIndex = (int)GameDefines.Expansions.WoTLK;
                IconImage.Source = new BitmapImage(new Uri("/BaconLauncher;component/Resources/Icons/questionmark.png", UriKind.Relative));
            }
            else
            {
                // Editing a profile
                Title = "Edit Profile";

                // Update fields to current profile
                ProfileNameTextBox.Text = profile.Name;
                BorderColor.SelectedColor = ColorHelper.ColorFromString("#" + profile.BorderColor, null);
                IconImage.Source = new BitmapImage(new Uri(profile.Icon, UriKind.Absolute));
                executableLocationTextBox.Text = profile.ExecutableLocation;
                CommandLineArgumentsTextBox.Text = profile.CommandLineArguments;

                if (profile.ApplicationSpecificSettings != null && profile.ApplicationSpecificSettings.GetType() == typeof(WorldOfWarcraftSettings))
                {
                    WorldOfWarcraftSettings wowSettings = (WorldOfWarcraftSettings)profile.ApplicationSpecificSettings;
                    RealmlistTextBox.Text = wowSettings.Realmlist;
                    ExpansionsComboBox.SelectedIndex = (int)wowSettings.Expansion;
                }
            }

            RefreshWoWSpecificSettingsVisibility();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                executableLocationTextBox.Text = openFileDialog.FileName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = EditingProfile;
            if (profile == null)
                profile = new Profile();

            profile.Name = ProfileNameTextBox.Text;
            profile.Icon = IconImage.Source.ToString();
            if (BorderColor.SelectedColor != null)
                profile.BorderColor = BorderColor.SelectedColor.Value.R.ToString("X2") + BorderColor.SelectedColor.Value.G.ToString("X2") + BorderColor.SelectedColor.Value.B.ToString("X2");
            else
                profile.BorderColor = String.Empty;
            profile.ExecutableLocation = executableLocationTextBox.Text;
            profile.CommandLineArguments = CommandLineArgumentsTextBox.Text;

            if (IsValidWorldOfWarcraftExecutable(executableLocationTextBox.Text))
            {
                ApplicationSpecificSettings appSpecificSettings = profile.ApplicationSpecificSettings;
                if (appSpecificSettings == null || appSpecificSettings.GetType() != typeof(WorldOfWarcraftSettings))
                    appSpecificSettings = new WorldOfWarcraftSettings();

                WorldOfWarcraftSettings wowSettings = (WorldOfWarcraftSettings)appSpecificSettings;
                wowSettings.Expansion = (GameDefines.Expansions)ExpansionsComboBox.SelectedIndex;
                wowSettings.Realmlist = RealmlistTextBox.Text;
                profile.ApplicationSpecificSettings = wowSettings;
            }
            else
                profile.ApplicationSpecificSettings = null;

            if (EditingProfile == null)
                ProfileManager.Instance.AddProfile(profile);
            else
            {
                // Update Tile, icon and border
                foreach (ProfileTile profileTile in ((MainWindow)System.Windows.Application.Current.MainWindow).profilesWrapPanel.Children)
                {
                    if (profileTile.Profile != profile)
                        continue;

                    profileTile.Title = profile.Name;
                    profileTile.Image = profile.Icon;
                    if (BorderColor.SelectedColor != null)
                    {
                        profileTile.BorderBrush = new SolidColorBrush(BorderColor.SelectedColor.Value);
                        profileTile.BorderThickness = new Thickness(1, 1, 1, 1);
                    }
                    else
                        profileTile.BorderThickness = new Thickness(0);
                    break;
                }

                ConfigManager.Instance.SaveConfig();
            }

            Close();
        }

        private bool IsValidWorldOfWarcraftExecutable(string executableLocation)
        {
            try
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(executableLocation);
                if (fileVersionInfo.ProductName != "World of Warcraft")
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void ErrorFromTextBox(TextBox erroredTextbox, string errorString)
        {
            erroredTextbox.BorderBrush = Brushes.Red;
            ErrorLabel.Content = errorString;
        }

        private void executableLocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshWoWSpecificSettingsVisibility();

            if (executableLocationTextBox.BorderBrush == Brushes.Red)
            {
                executableLocationTextBox.ClearValue(Border.BorderBrushProperty);
                ErrorLabel.Content = "";
            }
        }

        private void RefreshWoWSpecificSettingsVisibility()
        {
            if (IsValidWorldOfWarcraftExecutable(executableLocationTextBox.Text))
                WoWSpecificSettingsGroupBox.Visibility = Visibility.Visible;
            else
                WoWSpecificSettingsGroupBox.Visibility = Visibility.Hidden;
        }

        private void OnIconClicked(object sender, RoutedEventArgs e)
        {
            IconsWindow iw = new IconsWindow();
            iw.Owner = this;
            iw.Show();
        }
    }
}
