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
            "ExpansionIcons/vanilla_icon.png",
            "ExpansionIcons/tbc_icon.png",
            "ExpansionIcons/wotlk_icon.png",
            "ExpansionIcons/cata_icon.png",
            "ExpansionIcons/mop_icon.png",
            "ExpansionIcons/wod_icon.png",
            "ExpansionIcons/legion_icon.png",
            "ExpansionIcons/bfa_icon.png",
            "ExpansionIcons/shadowlands_icon.png"
        };
    }
}
