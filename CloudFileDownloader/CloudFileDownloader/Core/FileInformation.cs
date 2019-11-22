namespace CloudFileDownloader.Core
{
    public class FileInformation
    {
        public string Name { get; internal set; }
     
        public string Id { get; internal set; }

        public override string ToString()
        {
            return $"(Id= { Id }) Name = { Name }";
        }
    }
}