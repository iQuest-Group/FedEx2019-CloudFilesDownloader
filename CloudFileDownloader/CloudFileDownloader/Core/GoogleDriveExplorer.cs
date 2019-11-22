using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFileDownloader.Core
{
    public class GoogleDriveExplorer : IDriveExplorer
    {
        private UserCredential credential;
        private string credentialsFileName;
        private static string[] Scopes = { DriveService.Scope.DriveReadonly };
        private IDrive _drive;
        private const string credPath = "token1.json";


        public GoogleDriveExplorer(string credentialsFileName= "credentials.json")
        {
            this.credentialsFileName= credentialsFileName;
        }

        public IDrive Drive
        {
            get {
                if (_drive == null)
                {
                    _drive = new GoogleDrive(credential);
                }
                return _drive;
            }
        }

        public void Autheticate()
        {
            using (var stream = new FileStream(credentialsFileName, FileMode.Open, FileAccess.Read))
            {
                this.credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }
    }
}
