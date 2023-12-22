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

namespace MusicPlayerGUI
{
    public record User(int userID, string firstName, string lastName, string email, string password, byte[]? picture);
    public record UserSignIn(string email, string password);
    class ApiRequests
    {
        private static HttpClientHandler handler = new HttpClientHandler();
        private static HttpClient client = new HttpClient(handler) { BaseAddress = Settings.GetServerUrl() };
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
            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            return content;
        }
        private static CookieContainer GetAuthCookiesContainer()
        {
            int? userID = Settings.GetUserID();
            string? password = Settings.GetPassword();
            CookieContainer container = new CookieContainer();
            container.Add(new Cookie("userID", Convert.ToString(userID)));
            container.Add(new Cookie("password", password));
            return container;
        }

        public static void UpdateClient()
        {
            handler = new HttpClientHandler() { CookieContainer = GetAuthCookiesContainer() };
            client = new HttpClient(handler) { BaseAddress = Settings.GetServerUrl() };
        }

        public async static Task<int> SignUp(User user)
        {

            var result = await client.PostAsync(GetSubUrlApi("signup"), GetContentAsJson(user));
            if (!result.IsSuccessStatusCode)
            {
                return -1;
            }
            return await result.Content.ReadFromJsonAsync<int>();
        }

        public async static Task<int> SignIn(UserSignIn user)
        {
            var result = await client.PostAsync(GetSubUrlApi("signin"), GetContentAsJson(user));
            if (!result.IsSuccessStatusCode)
            {
                return -1;
            }
            return await result.Content.ReadFromJsonAsync<int>();
        }

    }
}
