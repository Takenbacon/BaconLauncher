using BaconLauncher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BaconLauncher
{
    public enum ProfileTypes : int
    {
        Generic,
        WorldOfWarcraft
    }

    [JsonDerivedType(typeof(WorldOfWarcraftSettings), (int)ProfileTypes.WorldOfWarcraft)]
    public class ApplicationSpecificSettings
    {
    }

    public class WorldOfWarcraftSettings : ApplicationSpecificSettings
    {
        public string Realmlist { get; set; } = string.Empty;
        public GameDefines.Expansions Expansion { get; set; }
    }

    public class Profile
    {
        public string Name { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string ExecutableLocation { get; set; } = string.Empty;
        public string CommandLineArguments { get; set; } = string.Empty;
        public ApplicationSpecificSettings ApplicationSpecificSettings { get; set; } = null;
    }
}
