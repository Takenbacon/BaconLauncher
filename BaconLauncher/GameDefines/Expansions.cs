using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconLauncher.GameDefines
{
    public enum Expansions : int
    {
        Vanilla,
        TBC,
        WoTLK,
        Cata,
        MoP,
        WoD,
        Legion,
        BfA,
        Shadowlands
    }

    public class ExpansionIcons
    {
        public static readonly string[] LookupTable = new string[]
        {
            "Assets/ExpansionIcons/vanilla_icon.png",
            "Assets/ExpansionIcons/tbc_icon.png",
            "Assets/ExpansionIcons/wotlk_icon.png",
            "Assets/ExpansionIcons/cata_icon.png",
            "Assets/ExpansionIcons/mop_icon.png",
            "Assets/ExpansionIcons/wod_icon.png",
            "Assets/ExpansionIcons/legion_icon.png",
            "Assets/ExpansionIcons/bfa_icon.png",
            "Assets/ExpansionIcons/shadowlands_icon.png"
        };
    }
}
