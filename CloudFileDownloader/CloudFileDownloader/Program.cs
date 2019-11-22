using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace CloudFileDownloader
{
    partial class Program
    {
        static string scope = "wl.basic";
        static string client_id = "0000000048323631";
        static Uri signInUrl = new Uri(String.Format(@"https://login.live.com/oauth20_authorize.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_type=code&scope={1}", client_id, scope));
        public static string AuthCode { get; set; }
        [STAThread]
        static void Main(string[] args)
        {
            Autheticate();


            //System.Threading.Thread t = new System.Threading.Thread(ThreadStart);
            //t.SetApartmentState(System.Threading.ApartmentState.STA);
            //t.Start();

            //Console.ReadLine();

            ////AuthCodeRetriever authCodeRtv = new AuthCodeRetriever();
            ////authCodeRtv.GetCode();
            ////TokenAquirer aquirer = new TokenAquirer();
            ////aquirer.GetAccessToken();
        }

        private static async Task Autheticate()
        {

            var msaAuthenticationProvider = new MsaAuthenticationProvider (client_id, "https://login.live.com/oauth20_desktop.srf", new[] { "onedrive.readonly", "wl.signin"});

                msaAuthenticationProvider.AuthenticateUserAsync();

            var oneDriveClient = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthenticationProvider);

            var rootItem = oneDriveClient.Drive.Items.Request().Filter("a");
        }

        private static void ThreadStart()
        {
            WebBrowser browser = new WebBrowser();
            browser.Dock = DockStyle.Fill;
            browser.Name = "webBrowser";
            browser.ScrollBarsEnabled = false;
            browser.TabIndex = 0;
            browser.Url = signInUrl;

            Form form = new Form();
            form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(browser);
            form.Name = "Browser";

            Application.Run(form);

            //Application.Run(form);

        }

        private static void browserLoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri.AbsoluteUri.Contains("code="))
            {
                AuthCode = Regex.Split(e.Uri.AbsoluteUri, "code=")[1];
            }
        }

    }
}
