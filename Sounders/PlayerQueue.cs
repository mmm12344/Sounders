using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicPlayerGUI
{
    public record SongData(int songId, string songName, BitmapImage songImage);

    class PlayerQueue
    {
        private List<SongData> songData = new List<SongData>();
        private ListView queueList;

        public PlayerQueue(ListView queueList)
        {
            queueList.Items.Clear();
            this.queueList = queueList;
        }

        public void Enqueue(SongData item)
        {

            songData.Add(item);
            queueList.Items.Add(new { songID = Convert.ToString(item.songId), songName = item.songName, songPic = item.songImage });
        }

        public void EnqueueFromList(List<SongData> items)
        {
            foreach (SongData item in items)
            {
                Enqueue(item);
            }
        }

        public SongData Dequeue()
        {
            if (songData.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                SongData temp = songData[0];
                songData.RemoveAt(0);
                queueList.Items.RemoveAt(0);
                return temp;
            }

        }
        public SongData Peek()
        {
            if (songData.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return songData[0];
            }
        }
        public bool IsEmpty()
        {
            if (songData.Count == 0)
            {
                return true;
            }
            return false;
        }
        public int Count()
        {
            return songData.Count;
        }
        public void Clear()
        {
            songData.Clear();
        }
        public int FindLength()
        {
            return songData.Count;
        }
        public SongData Search(int songIdToBeSearched)
        {
            for (int i = 0; i < songData.Count; i++)
            {
                if (songData[i].songId == songIdToBeSearched)
                {
                    return songData[i];
                }

            }
            return null;
        }
        public void Randomize()
        {
            for (int i = 0; i < songData.Count; i++)
            {
                int randomIndex = new Random().Next(0, songData.Count - 1);
                ReplaceInList(i, randomIndex);
            }
        }
        private void ReplaceInList(int a, int b)
        {
            SongData temp = songData[a];
            songData[a] = songData[b];
            songData[b] = temp;

        }
        public List<SongData> GetQueueList()
        {

            return songData;

        }
    }
}