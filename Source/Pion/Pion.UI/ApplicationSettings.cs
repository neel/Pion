using System;

namespace Pion.UI
{
    /// <summary>
    /// Represents the application-wide settings.
    /// </summary>
    public sealed class ApplicationSettings : IApplicationSettings
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationSettings class.
        /// </summary>
        public ApplicationSettings()
        {
        }

        /// <summary>
        /// The currently set folderpath wherein the videos are stored.
        /// </summary>
        public string DownloadLocation
        {
            get
            {
                string downloadLocation = Properties.Settings.Default.DownloadLocation;

                // Set the folder to the user's Desktop if it's his first time using this application.
                if (string.IsNullOrWhiteSpace(downloadLocation))
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                }

                return downloadLocation;
            }
        }

        /// <summary>
        /// Changes the download folder.
        /// </summary>
        /// <param name="newDownloadLocation">The new download folder to store the videos in.</param>
        public void ChangeDownloadLocation(string newDownloadLocation)
        {
            Properties.Settings.Default.DownloadLocation = newDownloadLocation;
            Save();
        }

        /// <summary>
        /// Persists the changed settings to disk.
        /// </summary>
        void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
