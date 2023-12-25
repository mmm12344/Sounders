using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sounders.Views
{
    /// <summary>
    /// Interaction logic for AddPlayListPage.xaml
    /// </summary>
    public partial class AddPlayListPage : Page
    {
        List<Track> my = new List<Track>(); //Test
        public AddPlayListPage()
        {
            InitializeComponent();
            my.Add(new Track()
            {
                songName = "hedfdfjhsdgfuydtsyull",
                songPic = @"/Static/Images/Logo.jpeg",


            });
            likedTracksList.Items.Add(my);
            likedTracksList.Items.Add(my);
        }
    }
}
