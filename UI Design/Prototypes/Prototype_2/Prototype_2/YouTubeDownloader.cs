using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;

namespace Prototype_2
{
    public sealed class YouTubeDownloader : IDisposable
    {
        string _youtubeSource;
        readonly WebClient _internalDownloader;
        readonly string _youTubeVideoUrl;

        public YouTubeDownloader(string youTubeVideoUrl)
        {
            _internalDownloader = new WebClient();
            _youTubeVideoUrl = youTubeVideoUrl;

            RegisterDownloadEventHandlers();
        }

        public void Dispose()
        {
            RemoveDownloadEventHandlers();
        }

        public void DownloadAsync(string destinationFilepath)
        {
            LazyLoadHtmlSource();

            var linkExtractor = new YouTubeSourceAnalyzer(_youtubeSource);

            string videoLink = linkExtractor.ExtractDirectDownloadLink();
            _internalDownloader.DownloadFileAsync(new Uri(videoLink), destinationFilepath);
        }

        public string GetTitle()
        {
            LazyLoadHtmlSource();

            var titleExtractor = new YouTubeSourceAnalyzer(_youtubeSource);

            return titleExtractor.ExtractTitle();
        }

        void DownloadFileCompletedEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            DownloadFileCompleted(this, e);
        }

        void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged(this, e);
        }

        void GetHtmlSource()
        {
            var sourceCodeFetcher = new YouTubeSourceCodeFetcher(_youTubeVideoUrl);
            _youtubeSource = sourceCodeFetcher.FetchHtmlSource();
        }

        void LazyLoadHtmlSource()
        {
            if (string.IsNullOrWhiteSpace(_youtubeSource))
            {
                GetHtmlSource();
            }
        }

        void RegisterDownloadEventHandlers()
        {
            _internalDownloader.DownloadFileCompleted += DownloadFileCompletedEventHandler;
            _internalDownloader.DownloadProgressChanged += DownloadProgressChangedEventHandler;
        }

        void RemoveDownloadEventHandlers()
        {
            _internalDownloader.DownloadFileCompleted -= DownloadFileCompletedEventHandler;
            _internalDownloader.DownloadProgressChanged -= DownloadProgressChangedEventHandler;
        }

        public event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted = delegate { };
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged = delegate { };
    }
}
