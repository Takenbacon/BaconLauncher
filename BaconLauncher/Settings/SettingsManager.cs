using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BaconLauncher.Settings
{
    public class SettingsManager
    {
        public static SettingsManager Instance { get; protected set; } = new SettingsManager();
        public Settings Settings = new Settings();

        public void LoadSettings()
        {
            using (XmlTextReader xmlTextReader = new XmlTextReader("settings.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Settings));

                try
                {
                    Settings = (Settings)deserializer.Deserialize(xmlTextReader);
                }
                catch (SystemException)
                {

                }
            }
        }

        public void SaveSettings()
        {
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter("settings.xml", Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                serializer.Serialize(xmlTextWriter, Settings);
            }
        }
    }
}
