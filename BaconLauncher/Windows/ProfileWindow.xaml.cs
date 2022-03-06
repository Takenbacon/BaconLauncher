using MahApps.Metro.Controls;
using Microsoft.Win32;
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
            }
            else
            {
                // Editing a profile
                Title = "Edit Profile";

                // Update fields to current profile
                ProfileNameTextBox.Text = profile.Name;
                executableLocationTextBox.Text = profile.ExecutableLocation;
                ExpansionsComboBox.SelectedIndex = (int)profile.Expansion;
                CommandLineArgumentsTextBox.Text = profile.CommandLineArguments;
                RealmlistTextBox.Text = profile.Realmlist;
            }
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
            profile.ExecutableLocation = executableLocationTextBox.Text;
            profile.Expansion = (GameDefines.Expansions)ExpansionsComboBox.SelectedIndex;
            profile.CommandLineArguments = CommandLineArgumentsTextBox.Text;
            profile.Realmlist = RealmlistTextBox.Text;

            if (EditingProfile == null)
                ProfileManager.Instance.AddProfile(profile);
            else
            {
                // Update Tile
                foreach (ProfileTile profileTile in ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children)
                {
                    if (profileTile.Profile != profile)
                        continue;

                    profileTile.Title = profile.Name;
                    profileTile.Image = "..\\" + GameDefines.ExpansionIcons.LookupTable[(int)profile.Expansion];
                    break;
                }

                ProfileManager.Instance.SaveAllProfiles();
            }

            Close();
        }
    }
}
