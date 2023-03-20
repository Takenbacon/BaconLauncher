using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace BaconLauncher
{
    public class IconTile : Tile
    {
        public IconTile()
        {
        }

        public static readonly DependencyProperty IconImageProperty
            = DependencyProperty.Register(nameof(IconImage),
                                  typeof(string),
                                  typeof(IconTile),
                                  new PropertyMetadata(default(string)));

        public string IconImage
        {
            get => (string)this.GetValue(IconImageProperty);
            set => this.SetValue(IconImageProperty, value);
        }
    }
}
