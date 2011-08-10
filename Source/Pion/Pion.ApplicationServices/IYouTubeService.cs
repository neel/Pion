using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pion.Domain;

namespace Pion.ApplicationServices
{
    public interface IYouTubeService : IDisposable
    {
        bool IsDownloadInProgress { get; }

        void DownloadAsync(string videoUrl, string downloadDirectory);
        string GetVideoTitle(string videoUrl);
        void ShowDownloadLocation(string downloadDirectory);
        bool ValidateVideoLink(string videoLink);

        event EventHandler<VideoDownloadProgressChanged> VideoDownloadProgressChanged;
        event EventHandler VideoDownloadCompleted;
    }
}
