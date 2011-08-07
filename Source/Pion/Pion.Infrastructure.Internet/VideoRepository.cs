using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pion.Domain;
using System.Net;
using System.ComponentModel;

namespace Pion.Infrastructure.Internet
{
    public sealed class VideoRepository : IVideoRepository
    {
        readonly WebClient _downloadAgent;

        public VideoRepository()
        {
            _downloadAgent = new WebClient();

            RegisterEventHandlers();
        }

        public void Dispose()
        {
            RemoveEventHandlers();
        }

        public void DownloadAsync(string videoUrl, string filepath)
        {
            _downloadAgent.DownloadFileAsync(new Uri(videoUrl), filepath);
        }

        void DownloadFileCompletedEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            VideoDownloadCompleted(this, EventArgs.Empty);
        }

        void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            VideoDownloadProgressChanged(this, new VideoDownloadProgressChanged(e.ProgressPercentage));
        }

        void RegisterEventHandlers()
        {
            _downloadAgent.DownloadFileCompleted += DownloadFileCompletedEventHandler;
            _downloadAgent.DownloadProgressChanged += DownloadProgressChangedEventHandler;
        }

        void RemoveEventHandlers()
        {
            _downloadAgent.DownloadFileCompleted -= DownloadFileCompletedEventHandler;
            _downloadAgent.DownloadProgressChanged -= DownloadProgressChangedEventHandler;
        }

        public event EventHandler VideoDownloadCompleted = delegate { };
        public event EventHandler<VideoDownloadProgressChanged> VideoDownloadProgressChanged = delegate { };
    }
}
