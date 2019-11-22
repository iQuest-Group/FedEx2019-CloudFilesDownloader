using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace CloudFileDownloader
{
    public class TokenAquirer
    {

        static string client_id = "0000000048323631";
        static string client_secret = "dacoqzCRYQN983^(]bOS52|";
        static string accessTokenUrl = String.Format(@"https://login.live.com/oauth20_token.srf?client_id={0}&client_secret={1}&redirect_uri=https://login.live.com/oauth20_desktop.srf&grant_type=authorization_code&code=", client_id, client_secret);
        static string apiUrl = @"https://apis.live.net/v5.0/";
        public Dictionary<string, string> tokenData = new Dictionary<string, string>();
        public string AccessToken { get; set; }
        public string ClientInfo { get; set; }
        public string AuthCode { get; set; }
        public void GetAccessToken()
        {
            MakeRequest(accessTokenUrl+AuthCode);
        }

        private void MakeRequest(string uri)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(AccessToken_DownloadStringCompleted);
            wc.DownloadString(new Uri(uri));
        }

        private void AccessToken_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            tokenData = deserializeJson(e.Result);
            if (tokenData.ContainsKey("access_token"))
            {
                this.AccessToken = tokenData["access_token"];
                GetUserInfo(apiUrl + "me?access_token=" + AccessToken);
            }
        }

        private void GetUserInfo(string urlRequest)
        {
            if (!String.IsNullOrEmpty(AccessToken))
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ClientInfo_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri(urlRequest));
            }
        }

        private void ClientInfo_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            this.ClientInfo = e.Result;
        }

        private Dictionary<string, string> deserializeJson(string json)
        {
            var jss = new JavaScriptSerializer();
            var d = jss.Deserialize<Dictionary<string, string>>(json);
            return d;
        }
    }

    //private void accessToken_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    //{
    //    tokenData = deserializeJson(e.Result);
    //    if (tokenData.ContainsKey("access_token"))
    //    {
    //        App.Current.Properties.Add("access_token", tokenData["access_token"]);
    //        getUserInfo();
    //    }
    //}

}
