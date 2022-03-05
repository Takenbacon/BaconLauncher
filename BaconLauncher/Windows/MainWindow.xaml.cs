using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace BaconLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ProfileManager.Instance.LoadProfiles();
        }

        private void OnProfileCreateClicked(object sender, RoutedEventArgs e)
        {
            ProfileWindow cpw = new ProfileWindow(null);
            cpw.Show();
        }

        private void OnProfileRightClicked(object sender, MouseButtonEventArgs e)
        {
            ProfileTile profileTile = sender as ProfileTile;

            ContextMenu contextMenu = new ContextMenu();
            MenuItem removeMenuItem = new MenuItem();
            removeMenuItem.Header = "Remove";
            removeMenuItem.Click += delegate { ProfileManager.Instance.RemoveProfileByTile(profileTile); };
            contextMenu.Items.Add(removeMenuItem);
            MenuItem editMenuItem = new MenuItem();
            editMenuItem.Header = "Edit";
            editMenuItem.Click += delegate
            {
                Profile profile = ProfileManager.Instance.GetProfileByGuid(profileTile.ProfileGuid);
                if (profile == null)
                    return;
                ProfileWindow cpw = new ProfileWindow(profile);
                cpw.Show();
            };
            contextMenu.Items.Add(editMenuItem);
            contextMenu.IsOpen = true;
        }

        private void OnProfileLeftClicked(object sender, RoutedEventArgs e)
        {
            ProfileTile profileTile = sender as ProfileTile;
            Profile profile = ProfileManager.Instance.GetProfileByGuid(profileTile.ProfileGuid);
            if (profile == null)
                return;

            try
            {
                string rootPath = System.IO.Path.GetDirectoryName(profile.ExecutableLocation);
                rootPath += "\\Data";

                foreach (string gameLocale in GameDefines.Locales.LookupTable)
                {
                    string localePath = rootPath + "\\" + gameLocale;
                    string realmlistFile = localePath + "\\realmlist.wtf";

                    if (Directory.Exists(localePath) && File.Exists(realmlistFile))
                    {
                        using (StreamWriter writer = new StreamWriter(localePath + realmlistFile, false))
                            writer.Write("set realmlist " + profile.Realmlist);
                    }
                }

                Process.Start(profile.ExecutableLocation, profile.CommandLineArguments);
            }
            catch (Exception)
            {

            }
        }
    }
}
