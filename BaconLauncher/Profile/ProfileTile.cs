﻿using System;
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
        public readonly Profile Profile;

        public ProfileTile()
        {
            Profile = null;
        }

        public ProfileTile(Profile profile)
        {
            Profile = profile;
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

        public static readonly DependencyProperty DropShadowColorProperty
            = DependencyProperty.Register(nameof(DropShadowColor),
                          typeof(string),
                          typeof(ProfileTile),
                          new PropertyMetadata(default(string)));

        public string DropShadowColor
        {
            get => (string)this.GetValue(DropShadowColorProperty);
            set => this.SetValue(DropShadowColorProperty, value);
        }
    }
}
