namespace Pion.UI
{
    public interface IApplicationSettings
    {
        string DownloadLocation { get; }
        void ChangeDownloadLocation(string newDownloadLocation);
    }
}