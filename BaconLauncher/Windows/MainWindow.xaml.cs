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
using BaconLauncher.Windows;
using BaconLauncher.Settings;

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

            SettingsManager.Instance.LoadSettings();
            ProfileManager.Instance.LoadProfiles();
        }

        private void OnProfileCreateClicked(object sender, RoutedEventArgs e)
        {
            ProfileWindow cpw = new ProfileWindow(null);
            cpw.Show();
        }

        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Show();
        }

        private void OnProfileRightClicked(object sender, MouseButtonEventArgs e)
        {
            ProfileTile profileTile = sender as ProfileTile;
            string filePath = System.IO.Path.GetDirectoryName(profileTile.Profile.ExecutableLocation);

            ContextMenu contextMenu = new ContextMenu();
            {
                MenuItem openFileLocationMenuItem = new MenuItem();
                openFileLocationMenuItem.Header = "Open File Location";
                openFileLocationMenuItem.Click += delegate { Process.Start("explorer.exe", filePath); };
                contextMenu.Items.Add(openFileLocationMenuItem);
            }
            {
                MenuItem clearCacheMenuItem = new MenuItem();
                clearCacheMenuItem.Header = "Clear Game Cache";
                if (!Directory.Exists(filePath + "\\Cache"))
                    clearCacheMenuItem.IsEnabled = false;
                clearCacheMenuItem.Click += delegate { ClearGameCache(filePath + "\\Cache"); };
                contextMenu.Items.Add(clearCacheMenuItem);
            }
            {
                MenuItem editMenuItem = new MenuItem();
                editMenuItem.Header = "Edit";
                editMenuItem.Click += delegate
                {
                    ProfileWindow cpw = new ProfileWindow(profileTile.Profile);
                    cpw.Show();
                };
                contextMenu.Items.Add(editMenuItem);
            }
            {
                MenuItem removeMenuItem = new MenuItem();
                removeMenuItem.Header = "Remove";
                removeMenuItem.Click += delegate { ProfileManager.Instance.RemoveProfileByTile(profileTile); };
                contextMenu.Items.Add(removeMenuItem);
            }
            contextMenu.IsOpen = true;
        }

        private void ClearGameCache(string cacheLocation)
        {
            Directory.Delete(cacheLocation, true);
        }

        private void OnProfileLeftClicked(object sender, RoutedEventArgs e)
        {
            ProfileTile profileTile = sender as ProfileTile;
            Profile profile = profileTile.Profile;

            try
            {
                string rootPath = System.IO.Path.GetDirectoryName(profile.ExecutableLocation);

                if (SettingsManager.Instance.Settings.AutoClearGameCache)
                    ClearGameCache(rootPath + "\\Cache");

                if (profile.Expansion >= GameDefines.Expansions.MoP)
                {
                    // Mop and higher use the /WTF/Config.wtf file for the realmlist
                    rootPath += "\\WTF";
                    string realmlistFile = rootPath + "\\Config.wtf";

                    if (Directory.Exists(rootPath) && File.Exists(realmlistFile))
                    {
                        // load the config file into memory
                        string[] lines = File.ReadAllLines(realmlistFile);

                        // write the config file back without the realm list setting
                        using (StreamWriter writer = new StreamWriter(realmlistFile, false))
                        {
                            for (int i = 0; i < lines.Length; ++i)
                            {
                                string line = lines[i];
                                if (line.IndexOf("SET realmlist", StringComparison.OrdinalIgnoreCase) == -1)
                                    writer.WriteLine(line);
                            }

                            // Write the new realm list setting
                            writer.WriteLine("SET realmlist \"" + profile.Realmlist + "\"");
                        }
                    }
                }
                else
                {
                    // Cata and lower use the /data/locale/realmlist.wtf file
                    rootPath += "\\Data";

                    foreach (string gameLocale in GameDefines.Locales.LookupTable)
                    {
                        string localePath = rootPath + "\\" + gameLocale;
                        string realmlistFile = localePath + "\\realmlist.wtf";

                        if (Directory.Exists(localePath) && File.Exists(realmlistFile))
                        {
                            using (StreamWriter writer = new StreamWriter(realmlistFile, false))
                                writer.Write("SET realmlist " + profile.Realmlist);
                        }
                    }
                }

                Process.Start(profile.ExecutableLocation, profile.CommandLineArguments);

                if (SettingsManager.Instance.Settings.CloseLauncherOnGameStart)
                    Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
