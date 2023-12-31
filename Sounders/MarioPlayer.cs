using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sounders
{
    internal class MarioPlayer
    {
        public Uri currentSongUri { get; set; }
        public MediaPlayer playMedia = new MediaPlayer();


        public MarioPlayer() { }

        public void play() { 
           playMedia.Play();
        }



    }
}
