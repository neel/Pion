using System;

namespace Pion.Domain
{
    public sealed class VideoDownloadProgressChanged : EventArgs
    {
        readonly int _progressPercentage;

        public VideoDownloadProgressChanged(int progressPercentage)
        {
            _progressPercentage = progressPercentage;
        }

        public int ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }
        }
    }
}
