using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sounders
{
    
        public class AudioPlayer
        {
            private MediaPlayer mediaPlayer = new MediaPlayer();

            public void Open(Uri audioUri)
            {
                mediaPlayer.Open(audioUri);
            }

            public void Play()
            {
                mediaPlayer.Play();
            }

            public void Pause()
            {
                mediaPlayer.Pause();
            }

            public void Seek(double position)
            {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                // Calculate the position based on the slider value
                double newPositionInSeconds = position * mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;

                // Ensure the calculated position is within the valid range
                newPositionInSeconds = Math.Max(0, Math.Min(newPositionInSeconds, mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds));

                
                mediaPlayer.Position = TimeSpan.FromSeconds(newPositionInSeconds);
            }
        }

        }
    }

