using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFileDownloader
{
    public class SignInBrowser
    {
        WebBrowser browser = new WebBrowser();
        internal void NavogateTo(Uri signInUrl)
        {
            browser.Show();
            browser.Navigate(signInUrl);
        }
    }
}
