using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BaconLauncher.Config;
using MahApps.Metro.Controls;

namespace BaconLauncher
{
    public class ProfileManager
    {
        public static ProfileManager Instance { get; protected set; } = new ProfileManager();

        public void AddProfile(Profile profile)
        {
            ConfigManager.Instance.Config.Profiles.Add(profile);

            ProfileTile profileTile = CreateProfileTile(profile);
            ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children.Add(profileTile);

            ConfigManager.Instance.SaveConfig();
        }

        public void RemoveProfileByTile(ProfileTile profileTile)
        {
            ConfigManager.Instance.Config.Profiles.Remove(profileTile.Profile);
            ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children.Remove(profileTile);

            ConfigManager.Instance.SaveConfig();
        }

        public void LoadProfiles()
        {
            foreach (Profile profile in ConfigManager.Instance.Config.Profiles)
            {
                ProfileTile profileTile = CreateProfileTile(profile);
                ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children.Add(profileTile);
            }
        }

        private ProfileTile CreateProfileTile(Profile profile)
        {
            ProfileTile profileTile = new ProfileTile(profile);
            profileTile.Title = profile.Name;
            profileTile.Image = profile.Icon;

            if (profile.BorderColor != String.Empty && profile.BorderColor != null)
            {
                Color? borderColor = ColorHelper.ColorFromString(profile.BorderColor, null);
                profileTile.BorderBrush = new SolidColorBrush(borderColor.Value);
                profileTile.BorderThickness = new Thickness(1, 1, 1, 1);
            }
            return profileTile;
        }
    }
}
