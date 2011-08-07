using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.Domain
{
    public interface IVideoRepository : IDisposable
    {
        void DownloadAsync(string videoUrl, string filepath);

        event EventHandler<VideoDownloadProgressChanged> VideoDownloadProgressChanged;
        event EventHandler VideoDownloadCompleted;
    }
}
