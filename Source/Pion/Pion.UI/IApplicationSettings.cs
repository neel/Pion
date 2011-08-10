namespace Pion.UI
{
    /// <summary>
    /// Represents the application-wide settings.
    /// </summary>
    public interface IApplicationSettings
    {
        /// <summary>
        /// The path of the folder, wherein the downloaded videos are stored.
        /// </summary>
        string DownloadLocation { get; }

        /// <summary>
        /// Changes the default download location.
        /// </summary>
        /// <param name="newDownloadLocation">The new path of the download folder.</param>
        void ChangeDownloadLocation(string newDownloadLocation);
    }
}