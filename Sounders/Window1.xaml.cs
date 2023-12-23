using MusicPlayerGUI;
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
using System.Windows.Shapes;

namespace Sounders
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
   
    public partial class Window1 : Window
    {
        List<Track> my = new List<Track>();
        public Window1()
        {
            InitializeComponent();

            my.Add(new Track()
            {
                songName = "hell",
                songPic = @"Static/Images/Logo.jpeg",


            });
            MyListView.Items.Add(my);
            MyListView.Items.Add(my);
        }
    }
}
