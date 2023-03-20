using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace BaconLauncher.Windows
{
    /// <summary>
    /// Interaction logic for IconsWindow.xaml
    /// </summary>
    public partial class IconsWindow : MetroWindow
    {
        public IconsWindow()
        {
            InitializeComponent();
            LoadIcons();
        }

        public void LoadIcons()
        {
            foreach (string file in Defines.Icons.LookupTable)
            {
                IconTile iconTile = new IconTile();
                iconTile.IconImage = "/BaconLauncher;component/" + file;

                iconsWrapPanel.Children.Add(iconTile);
            }
        }

        private void IconTile_Click(object sender, RoutedEventArgs e)
        {
            (Owner as ProfileWindow).IconImage.Source = new BitmapImage(new Uri((sender as IconTile).IconImage, UriKind.Relative));
            Close();
        }
    }
}
