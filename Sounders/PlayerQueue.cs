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
    public record SongData(int songID, string songName, BitmapImage songPic);
public class PlayerQueue
    {
        private List<SongData> songlist = new List<SongData>();
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
            foreach(SongData s in this.songlist)
            {
                if (s.songID == item.songID) return;
            }
            
            songlist.Add(item);
            queueList.Items.Add(item);
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
            if (songlist.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                SongData temp = songlist[0];
                songlist.RemoveAt(0);
                queueList.Items.RemoveAt(0);
                return temp;
            }

        }

        private void AddToQueueListFromsongList(List<SongData> songs)
        {
            ClearQueueList();
            foreach(SongData song in songs)
            {
                queueList.Items.Add(song);
            }
        }

        private void ClearQueueList()
        {
            queueList.Items.Clear();
        }

        public void ClearAll()
        {
            int itemsCount = songlist.Count();
            for(int i = 0; i < itemsCount; i++)
            {
                Dequeue();
            }
        }
        public SongData Peek()
        {
            if (songlist.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return songlist[0];
            }
        }
        public bool IsEmpty()
        {
            if (songlist.Count == 0)
            {
                return true;
            }
            return false;
        }
        public int Count()
        {
            return songlist.Count;
        }
        public void Clear()
        {
            songlist.Clear();
        }
        public int FindLength()
        {
            return songlist.Count;
        }
        public SongData Search(int songIdToBeSearched)
        {
            for (int i = 0; i < songlist.Count; i++)
            {
                if (songlist[i].songID == songIdToBeSearched)
                {
                    return songlist[i];
                }

            }
            return null;
        }
        public void Randomize()
        {
            for (int i = 1; i < songlist.Count; i++)
            {
                int randomIndex = new Random().Next(1, songlist.Count - 1);
                ReplaceInList(i, randomIndex);
            }
            AddToQueueListFromsongList(songlist);
        }
        private void ReplaceInList(int a, int b)
        {
            SongData songListtemp = songlist[a];
            songlist[a] = songlist[b];
            songlist[b] = songListtemp;
        }
        public List<SongData> GetQueueList()
        {

            return songlist;

        }

        public void AddCurrentSongToPlayer()
        {
            var result = ApiRequests.GetSong(Peek().songID).Result;
            if (result != null)
            {

                try
                {
                    songPicture.Source = HelperMethods.GetBitmapImgFromBytes(result.picture);
                    songName.Text = result.name;
                    likeButton.Tag = Convert.ToString(Peek().songID);
                    bool isLiked = ApiRequests.IsLiked(Peek().songID).Result;
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