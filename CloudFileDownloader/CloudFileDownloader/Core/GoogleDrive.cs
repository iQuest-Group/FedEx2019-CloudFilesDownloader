using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace CloudFileDownloader.Core
{
    public class GoogleDrive : IDrive
    {
        private readonly DriveService service;

        public GoogleDrive(UserCredential creds)
        {
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = creds,
                ApplicationName = "CloudFileDownloader",
            });
        }

        public void Get(string itemId, string destPath)
        {
            var request = service.Files.Get(itemId);
            var stream = new System.IO.MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream, destPath);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream);

        }


        private static void SaveStream(System.IO.MemoryStream stream, string saveTo)
        {
            using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }

        public List<FileInformation> GetAll()
        {
            List<FileInformation> allItems = new List<FileInformation>();
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    allItems.Add(new FileInformation { Name=file.Name, Id=file.Id });
                }
            }
            return allItems;
        }

        public void Put(string localPath, string destination)
        {
            throw new NotImplementedException();
        }
    }
}
