using System.Collections.Generic;

namespace CloudFileDownloader.Core
{
    public interface IDrive
    {
        List<FileInformation> GetAll();
        void Get(string itemId, string destPath);
        void Put(string localPath);
    }
}