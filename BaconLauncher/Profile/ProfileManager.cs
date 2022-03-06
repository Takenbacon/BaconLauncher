using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using MahApps.Metro.Controls;

namespace BaconLauncher
{
    public class ProfileManager
    {
        public static ProfileManager Instance { get; protected set; } = new ProfileManager();

        private ProfileList ProfileList = new ProfileList();

        public void AddProfile(Profile profile)
        {
            ProfileList.Profiles.Add(profile);

            ProfileTile profileTile = CreateProfileTile(profile);
            ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children.Add(profileTile);

            SaveAllProfiles();
        }

        public void RemoveProfileByTile(ProfileTile profileTile)
        {
            ProfileList.Profiles.Remove(profileTile.Profile);
            ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children.Remove(profileTile);

            SaveAllProfiles();
        }

        public void SaveAllProfiles()
        {
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter("profiles.xml", Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProfileList));

                serializer.Serialize(xmlTextWriter, ProfileList);
            }
        }

        public void LoadProfiles()
        {
            using (XmlTextReader xmlTextReader = new XmlTextReader("profiles.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(ProfileList));

                try
                {
                    ProfileList = (ProfileList)deserializer.Deserialize(xmlTextReader);
                } catch (SystemException)
                {

                }

                foreach (Profile profile in ProfileList.Profiles)
                {
                    ProfileTile profileTile = CreateProfileTile(profile);
                    ((MainWindow)Application.Current.MainWindow).profilesWrapPanel.Children.Add(profileTile);
                }
            }
        }

        private ProfileTile CreateProfileTile(Profile profile)
        {
            ProfileTile profileTile = new ProfileTile(profile);
            profileTile.Title = profile.Name;
            profileTile.Image = "..\\" + GameDefines.ExpansionIcons.LookupTable[(int)profile.Expansion];
            return profileTile;
        }
    }
}
