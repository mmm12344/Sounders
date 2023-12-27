using Sounders;
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
        private Image songPicture;
        private TextBlock songName;
        private Button likeButton;

        public PlayerQueue(MainWindow mainWindow)
        {
            mainWindow.queueList.Items.Clear();
            this.queueList = mainWindow.queueList;
            this.songPicture = mainWindow.songPicture;
            this.songName = mainWindow.songName;
            this.likeButton = mainWindow.likeButton;
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

        public void AddCurrentSongToPlayer()
        {
            var result = ApiRequests.GetSong(Peek().songId).Result;
            if (result != null)
            {

                try
                {
                    songPicture.Source = HelperMethods.GetBitmapImgFromBytes(result.picture);
                    songName.Text = result.name;
                    likeButton.Tag = Convert.ToString(Peek().songId);
                    bool isLiked = ApiRequests.IsLiked(Peek().songId).Result;
                    Image image = new Image();
                    image.Height = 20;
                    image.Width = 20;
                    if (isLiked)
                    {
                        image.Source = new BitmapImage(new Uri("Static/Images/RedHeart.png", UriKind.Relative));
                        likeButton.Content = image;
                    }
                    else
                    {
                        image.Source = new BitmapImage(new Uri("Static/Images/WhiteHeart.png", UriKind.Relative));
                        likeButton.Content = image;
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                }
            }
        }
    }
}