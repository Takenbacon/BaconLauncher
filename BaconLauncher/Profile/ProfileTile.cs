using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace BaconLauncher
{
    public class ProfileTile : Tile
    {
        public readonly Guid ProfileGuid;

        public ProfileTile(Guid profileGuid)
        {
            ProfileGuid = profileGuid;
        }

        public static readonly DependencyProperty ImageProperty
            = DependencyProperty.Register(nameof(Image),
                                  typeof(string),
                                  typeof(ProfileTile),
                                  new PropertyMetadata(default(string)));

        public string Image
        {
            get => (string)this.GetValue(ImageProperty);
            set => this.SetValue(ImageProperty, value);
        }
    }
}
