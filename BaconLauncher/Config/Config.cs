using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaconLauncher;

namespace BaconLauncher.Config
{
    public class Config
    {
        public Config()
        {
            // Default Configs
            CloseLauncherOnGameStart = false;

            Profiles = new List<Profile>();
        }

        public List<Profile> Profiles { get; set; }

        public bool CloseLauncherOnGameStart { get; set; }
    }
}
