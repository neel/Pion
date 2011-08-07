using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.Domain
{
    public interface IWebsiteRepository
    {
        string DownloadHtml(string url);
    }
}
