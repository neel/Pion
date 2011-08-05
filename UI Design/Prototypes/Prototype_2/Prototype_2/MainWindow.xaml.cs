using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.ComponentModel;
using System.Net;

namespace Prototype_2
{
    public partial class MainWindow : Window
    {
        readonly IsYouTubeVideoSpecification _youTubeUrlIdentifier;

        public MainWindow()
        {
            InitializeComponent();

            _youTubeUrlIdentifier = new IsYouTubeVideoSpecification();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            RemoveClipboardListener();

            base.OnClosing(e);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource source = GetCurrentHwndWindowSource();

            source.AddHook(WndProc);

            RegisterClipboardListener();
        }

        bool CheckIfClipboardContainsValidData()
        {
            return Clipboard.ContainsText();
        }

        HwndSource GetCurrentHwndWindowSource()
        {
            return PresentationSource.FromVisual(this) as HwndSource;
        }

        IntPtr GetCurrentWindowHandle()
        {
            HwndSource source = GetCurrentHwndWindowSource();

            return source.Handle;
        }

        void DownloadFileCompletedEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            Status.Text = string.Format("Done");
        }

        void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            Status.Text = e.ProgressPercentage.ToString();
        }

        void HandleClipboardUpdateMessage()
        {
            if (!CheckIfClipboardContainsValidData())
            {
                return;
            }

            string copiedText = Clipboard.GetText();
            if (!_youTubeUrlIdentifier.IsSatisfiedBy(copiedText))
            {
                return;
            }

            YouTubeDownloader youTubeDownloader = new YouTubeDownloader(copiedText);

            youTubeDownloader.DownloadFileCompleted += DownloadFileCompletedEventHandler;
            youTubeDownloader.DownloadProgressChanged += DownloadProgressChangedEventHandler;

            string filepath = System.IO.Path.ChangeExtension(System.IO.Path.Combine(Properties.Settings.Default.DownloadPath, youTubeDownloader.GetTitle()), "flv");

            youTubeDownloader.DownloadAsync(filepath);

            Filename.Text = youTubeDownloader.GetTitle();
        }

        void RegisterClipboardListener()
        {
            NativeMethods.AddClipboardFormatListener(GetCurrentWindowHandle());
        }

        void RemoveClipboardListener()
        {
            NativeMethods.RemoveClipboardFormatListener(GetCurrentWindowHandle());
        }

        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case (int)WindowsMessageType.WM_CLIPBOARDUPDATE:
                    HandleClipboardUpdateMessage();
                    break;
            }

            return IntPtr.Zero;
        }

        private void ShowSettings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow();

            settings.ShowDialog();
        }
    }
}
