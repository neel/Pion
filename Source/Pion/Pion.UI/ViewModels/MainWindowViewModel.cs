using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pion.ApplicationServices;
using System.ComponentModel;
using Pion.Domain;

namespace Pion.UI.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        int _currentProgressPercentage;
        string _currentVideoTitle;
        readonly IApplicationSettings _settings;
        readonly IYouTubeService _youTubeService;

        public MainWindowViewModel(IYouTubeService youTubeService, IApplicationSettings settings)
        {
            _settings = settings;
            _youTubeService = youTubeService;

            RegisterEventHandlers();
        }

        public string CurrentVideoTitle
        {
            get
            {
                return _currentVideoTitle;
            }

            private set
            {
                if (value == _currentVideoTitle)
                {
                    return;
                }

                _currentVideoTitle = value;

                RaisePropertyChanged("CurrentVideoTitle");
            }
        }

        public int CurrentProgressPercentage
        {
            get
            {
                return _currentProgressPercentage;
            }

            private set
            {
                if (value == _currentProgressPercentage)
                {
                    return;
                }

                _currentProgressPercentage = value;

                RaisePropertyChanged("CurrentProgressPercentage");
            }
        }

        public void Dispose()
        {
            RemoveEventHandlers();
        }

        public void Download(string url)
        {
            if (!CheckIfYouTubeLink(url))
            {
                return;
            }

            if (_youTubeService.IsDownloadInProgress)
            {
                return;
            }

            CurrentVideoTitle = _youTubeService.GetVideoTitle(url);

            _youTubeService.DownloadAsync(url, _settings.DownloadLocation);
        }

        bool CheckIfYouTubeLink(string url)
        {
            return !string.IsNullOrWhiteSpace(url) && url.Contains("youtube.com");
        }

        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        void RegisterEventHandlers()
        {
            _youTubeService.VideoDownloadCompleted += VideoDownloadCompletedEventHandler;
            _youTubeService.VideoDownloadProgressChanged += VideoDownloadProgressChangedCompleted;
        }

        void RemoveEventHandlers()
        {
            _youTubeService.VideoDownloadCompleted -= VideoDownloadCompletedEventHandler;
            _youTubeService.VideoDownloadProgressChanged -= VideoDownloadProgressChangedCompleted;
        }

        void VideoDownloadProgressChangedCompleted(object sender, VideoDownloadProgressChanged e)
        {
            CurrentProgressPercentage = e.ProgressPercentage;
        }

        void VideoDownloadCompletedEventHandler(object sender, EventArgs e)
        {
            CurrentProgressPercentage = 100;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
