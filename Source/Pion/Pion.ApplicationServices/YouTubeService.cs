using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pion.Domain;
using System.IO;

namespace Pion.ApplicationServices
{
    public sealed class YouTubeService : IYouTubeService
    {
        bool _isDownloadInProgress;
        readonly IVideoRepository _videoRepository;
        readonly IWebsiteRepository _websiteRepository;

        public YouTubeService(IWebsiteRepository websiteRepository, IVideoRepository videoRepository)
        {
            _websiteRepository = websiteRepository;
            _videoRepository = videoRepository;
        }

        public bool IsDownloadInProgress
        {
            get
            {
                return _isDownloadInProgress;
            }
        }

        public void Dispose()
        {
            RemoveEventHandlers();
        }

        public void DownloadAsync(string videoUrl, string downloadDirectory)
        {
            TransitionToDownloadingState();

            string htmlSource = GetHtmlSource(videoUrl);

            YouTubeParser youtubeParser = new YouTubeParser(htmlSource);
            string videolink = youtubeParser.ExtractDownloadLink();

            string videoFilepath = CreateDestinationFilepath(downloadDirectory, GetVideoTitle(videoUrl));
            _videoRepository.DownloadAsync(videolink, videoFilepath);

            TransitionToIdleState();
        }

        public string GetVideoTitle(string videoUrl)
        {
            string htmlSource = GetHtmlSource(videoUrl);

            YouTubeParser parser = new YouTubeParser(htmlSource);

            return parser.ExtractTitle();
        }

        string CreateDestinationFilepath(string downloadDirectory, string videoTitle)
        {
            return Path.Combine(downloadDirectory, Path.ChangeExtension(videoTitle, ".flv"));
        }

        string GetHtmlSource(string url)
        {
            return _websiteRepository.DownloadHtml(url);
        }

        void RegisterEventHandlers()
        {
            _videoRepository.VideoDownloadCompleted += VideoDownloadCompletedEventHandler;
            _videoRepository.VideoDownloadProgressChanged += VideoDownloadProgressChangedEventHandler;
        }

        void RemoveEventHandlers()
        {
            _videoRepository.VideoDownloadCompleted -= VideoDownloadCompletedEventHandler;
            _videoRepository.VideoDownloadProgressChanged -= VideoDownloadProgressChangedEventHandler;
        }

        void TransitionToDownloadingState()
        {
            _isDownloadInProgress = true;
        }

        void TransitionToIdleState()
        {
            _isDownloadInProgress = false;
        }

        void VideoDownloadCompletedEventHandler(object sender, EventArgs e)
        {
            VideoDownloadCompleted(this, EventArgs.Empty);
        }

        void VideoDownloadProgressChangedEventHandler(object sender, VideoDownloadProgressChanged e)
        {
            VideoDownloadProgressChanged(this, new VideoDownloadProgressChanged(e.ProgressPercentage));
        }

        public event EventHandler VideoDownloadCompleted = delegate { };
        public event EventHandler<VideoDownloadProgressChanged> VideoDownloadProgressChanged = delegate { };
    }
}
