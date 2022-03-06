using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaconLauncher
{
    [XmlType("Profiles")]
    public class ProfileList
    {
        public ProfileList()
        {
            Profiles = new List<Profile>();
        }

        [XmlElement("Profile")]
        public List<Profile> Profiles { get; set; }
    }

    public class Profile
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Realmlist")]
        public string Realmlist { get; set; }
        [XmlElement("Expansion")]
        public GameDefines.Expansions Expansion { get; set; }
        [XmlElement("ExecutableLocation")]
        public string ExecutableLocation { get; set; }
        [XmlElement("CommandLineArguments")]
        public string CommandLineArguments { get; set; }
    }
}
