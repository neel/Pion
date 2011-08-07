using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.UI
{
    public sealed class ApplicationSettings : IApplicationSettings
    {
        public ApplicationSettings()
        {
        }

        public string DownloadLocation
        {
            get
            {
                return Properties.Settings.Default.DownloadLocation;
            }
        }

        public void ChangeDownloadLocation(string newDownloadLocation)
        {
            Properties.Settings.Default.DownloadLocation = newDownloadLocation;
            Save();
        }

        void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
