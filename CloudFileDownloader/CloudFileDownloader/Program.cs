using CloudFileDownloader.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CloudFileDownloader
{
    class Program
    {
        //    // If modifying these scopes, delete your previously saved credentials
        //    // at ~/.credentials/drive-dotnet-quickstart.json
        //    static string[] Scopes = { DriveService.Scope.DriveReadonly };
        //    static string ApplicationName = "CloudFilesDownloader";
        //    static string credPath = "token.json";
        static void Main(string[] args)
        {

            //    UserCredential credential = GetCredetials();

            //    // Create Drive API service.
            //    DriveService service = GetService(credential);

            //    //Execute Query
            //    IList<Google.Apis.Drive.v3.Data.File> files = GetAllFiles(service);

            //    Console.WriteLine("Files:");
            //    Display(files);

            IDriveExplorer explorer = new GoogleDriveExplorer();
            explorer.Autheticate();

            var files = explorer.Drive.GetAll();
            foreach (var file in files)
            {
                Console.WriteLine($"Name = {file.Name}, Id={file.Id}");
            }


            explorer.Drive.Put(@"C:\Users\Florin\Downloads\logoASCOR.JPG");
            var files2 = explorer.Drive.GetAll();
            foreach(var file in files2)
            {
                Console.WriteLine($"Name = {file.Name}, Id={file.Id}");
            }



        }

        //    private static void Display(IList<Google.Apis.Drive.v3.Data.File> files)
        //    {
        //        if (files != null && files.Count > 0)
        //        {
        //            foreach (var file in files)
        //            {
        //                Console.WriteLine("{0} ({1})", file.Name, file.Id);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("No files found.");
        //        }
        //    }

        //    private static IList<Google.Apis.Drive.v3.Data.File> GetAllFiles(DriveService service)
        //    {
        //        // Define parameters of request.
        //        FilesResource.ListRequest listRequest = service.Files.List();
        //        listRequest.PageSize = 10;
        //        listRequest.Fields = "nextPageToken, files(id, name)";

        //        // List files.
        //        IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
        //            .Files;
        //        return files;
        //    }

        //    private static DriveService GetService(UserCredential credential)
        //    {
        //        return new DriveService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = ApplicationName,
        //        });
        //    }

        //    private static UserCredential GetCredetials(string credentialsFileName = "client_secret_1035074375844-88ilrlps48sd1h2c9dl4muhat0j8kiq4.apps.googleusercontent.com.json")
        //    {
        //        UserCredential credential=null;
        //        using (var stream =new FileStream(credentialsFileName , FileMode.Open, FileAccess.Read))
        //        {
        //            // The file token.json stores the user's access and refresh tokens, and is created
        //            // automatically when the authorization flow completes for the first time.
        //            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //                GoogleClientSecrets.Load(stream).Secrets,
        //                Scopes,
        //                "user",
        //                CancellationToken.None,
        //                new FileDataStore(credPath, true)).Result;
        //            Console.WriteLine("Credential file saved to: " + credPath);
        //        }

        //        return credential;
        //    }
        //}
    }
}
