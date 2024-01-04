using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MusicPlayerGUI.settings;
using System.Text.Json;
using System.Windows;

namespace MusicPlayerGUI
{
    public record UserSignUp(string firstName, string lastName, string email, string password);
    public record UserSignIn(string email, string password);
    public record SongInfo(int songID, string name, byte[] picture);
    public record Song(int songID, string name, byte[] file, int likes, byte[] picture);
    public record PostSong(string name, byte[] file, byte[] picture);
    public record Playlist(int playlistID, string name, byte[] picture);
    public record PostPlaylist(string name, byte[] picture);
 
    class ApiRequests
    {
        private static HttpClientHandler handler = new HttpClientHandler();
        private static HttpClient client = new HttpClient(handler) { BaseAddress = new Uri(Settings.GetServerUrl()) };
        private static string GetSubUrlApi(string url)
        {
            return "/api/" + url;
        }
       
        private static string GetSubUrlAuth(string url)
        {
            return "/authentication/" + url;
        }
        private static StringContent GetContentAsJson(object obj)
        {
            var content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
            return content;
        }
        private static CookieContainer GetAuthCookiesContainer()
        {
            int? userID = Settings.GetUserID();
            string? password = Settings.GetPassword();
            CookieContainer container = new CookieContainer();
            container.Add(new Uri(Settings.GetServerUrl()), new Cookie("userID", Convert.ToString((userID == null)? "": userID)));
            container.Add(new Uri(Settings.GetServerUrl()), new Cookie("password",( password == null)? "":password));
            return container;
        }

        public static void UpdateClient()
        {
            handler = new HttpClientHandler() { CookieContainer = GetAuthCookiesContainer() };
            client = new HttpClient(handler) { BaseAddress = new Uri(Settings.GetServerUrl()) };
        }

        public async static Task<bool> IsLive()
        {
            try
            {
                var result = client.GetAsync("").Result;
                if (!result.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async static Task<int> SignUp(UserSignUp user)
        {
            if (!IsLive().Result)
                return -1;
            var result = client.PostAsync(GetSubUrlAuth("signup"), GetContentAsJson(user)).Result;
            if (!result.IsSuccessStatusCode)
            {
                return -1;
            }
            return await result.Content.ReadFromJsonAsync<int>();
        }

        public async static Task<int> SignIn(UserSignIn user)
        {
            if (!IsLive().Result)
                return -1;
            var result =  client.PostAsync(GetSubUrlAuth("signin"), GetContentAsJson(user)).Result;
            if (!result.IsSuccessStatusCode)
            {
                return -1;
            }
            return await result.Content.ReadFromJsonAsync<int>();
        }

        public async static Task<List<SongInfo>> GetLikedSongs()
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi("get_liked_songs")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<SongInfo>>();
        } 

        public async static Task<List<SongInfo>> GetOwnSongs()
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi("get_own_songs")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<SongInfo>>();
        }

        public async static Task<List<SongInfo?>> GetAllSongs()
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi("get_all_songs")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<SongInfo?>>();
        }

        public async static Task<List<SongInfo>> Search(string parameter)
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi($"search/{parameter}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<SongInfo>>();
        }

        public async static Task<List<Playlist>> GetAllPlaylists()
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi("get_all_playlists")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<Playlist>>();
        }

        public async static Task<List<Playlist>> GetOwnPlaylists()
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi("get_own_playlists")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<Playlist>>();
        }

        public async static Task<List<SongInfo>> GetPlaylistSongs(int playlistID)
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi($"get_playlist_songs/{playlistID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<List<SongInfo>>();
        }

        public async static Task<bool> DeleteSong(int songID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.GetAsync(GetSubUrlApi($"delete_song/{songID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> AddLikeToSong(int songID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.GetAsync(GetSubUrlApi($"add_like_to_song/{songID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<Song> GetSong(int songID)
        {
            if (!IsLive().Result)
                return null;
            var result = client.GetAsync(GetSubUrlApi($"get_song/{songID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await result.Content.ReadFromJsonAsync<Song>();
        }

        public async static Task<bool> AddSong(PostSong song)
        {
            if (!IsLive().Result)
                return false;
            var result = client.PostAsync(GetSubUrlApi("add_song"), GetContentAsJson(song)).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> AddPLaylist(PostPlaylist playlist)
        {
            if (!IsLive().Result)
                return false;
            var result = client.PostAsync(GetSubUrlApi("add_playlist"), GetContentAsJson(playlist)).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> IsLiked(int songID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.GetAsync(GetSubUrlApi($"is_liked/{songID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return await result.Content.ReadFromJsonAsync<bool>();
        }

        public async static Task<bool> AddSongToPlaylist(int playlistID, int songID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.PostAsync(GetSubUrlApi("add_song_to_playlist"),GetContentAsJson( new {playlistID = playlistID, songID = songID })).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> RemoveLike(int songID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.GetAsync(GetSubUrlApi($"remove_like/{songID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> ChangeUserInfo(UserSignUp user)
        {
            if (!IsLive().Result)
                return false;
            var result = client.PostAsync(GetSubUrlAuth("change_user_info"), GetContentAsJson(user)).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> DeletePlaylist(int playlistID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.GetAsync(GetSubUrlApi($"delete_playlist/{playlistID}")).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> RemoveSongFromPlaylist(int playlistID, int songID)
        {
            if (!IsLive().Result)
                return false;
            var result = client.PostAsync(GetSubUrlApi("remove_song_from_playlist"), GetContentAsJson(new { playlistID = playlistID, songID = songID })).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

    }
}
