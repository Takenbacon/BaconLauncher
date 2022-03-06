using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaconLauncher.Settings
{
    [XmlType("Settings")]
    public class Settings
    {
        public Settings()
        {
            // Default settings
            CloseLauncherOnGameStart = false;
        }

        [XmlElement("CloseLauncherOnGameStart")]
        public bool CloseLauncherOnGameStart { get; set; }
    }
}
