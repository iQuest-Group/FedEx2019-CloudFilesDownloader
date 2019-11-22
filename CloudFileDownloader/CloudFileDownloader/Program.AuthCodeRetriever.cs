using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CloudFileDownloader
{
    partial class Program
    {
        public class AuthCodeRetriever
        {
            public string AuthCode { get; set; }
            WebBrowser browser = new WebBrowser();
            public void GetCode()
            {
                
                browser.LoadCompleted += browserLoadCompleted;
                browser.Navigate(signInUrl);
                
            }

            private void browserLoadCompleted(object sender, NavigationEventArgs e)
            {
                if (e.Uri.AbsoluteUri.Contains("code="))
                {
                    AuthCode = Regex.Split(e.Uri.AbsoluteUri, "code=")[1];
                }
            }
        }
    }
}
