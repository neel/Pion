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
using System.Diagnostics;

namespace Prototype_3
{
    public partial class MainWindow : Window
    {
        readonly IsYouTubeVideoSpecification _youTubeUrlIdentifier;

        System.Windows.Forms.NotifyIcon _notifier;
        WindowState _storedWindowState;

        string _currentDownloadFilename;

        const string ICON_LOCATION = @"Icons\trayicon.ico";

        public MainWindow()
        {
            InitializeComponent();

            _youTubeUrlIdentifier = new IsYouTubeVideoSpecification();

            _storedWindowState = WindowState.Normal;

            _notifier = new System.Windows.Forms.NotifyIcon();


            InitializeTrayIcon();
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
            Status.Value = 60;

            ShowNewDownloadNotification(string.Format("{0} download completed. Click to open containing folder.", _currentDownloadFilename));
        }

        void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            Status.Value = e.ProgressPercentage;

            _notifier.Text = e.ProgressPercentage.ToString();
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

            string videoTitle = youTubeDownloader.GetTitle();

            _currentDownloadFilename = videoTitle;

            string filepath = System.IO.Path.ChangeExtension(System.IO.Path.Combine(Properties.Settings.Default.DownloadPath, videoTitle), "flv");

            youTubeDownloader.DownloadAsync(filepath);

            Filename.Content = videoTitle;

            ShowNewDownloadNotification(string.Format("Downloading {0}", videoTitle));
        }

        void InitializeTrayIcon()
        {
            _notifier.BalloonTipTitle = this.Title;
            _notifier.Text = this.Title;
            _notifier.Icon = new System.Drawing.Icon(ICON_LOCATION);
            _notifier.Click += new EventHandler(m_notifyIcon_Click);

 
            _notifier.BalloonTipClicked += new EventHandler(_notifier_BalloonTipClicked);

            ShowTrayIcon(true);
        }

        void _notifier_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.DownloadPath);
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

        void OnClose(object sender, CancelEventArgs args)
        {
            _notifier.Dispose();
            _notifier = null;
        }

        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _storedWindowState = WindowState;
            }
        }
        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Visibility = System.Windows.Visibility.Visible;
            WindowState = _storedWindowState;
        }


        void ShowTrayIcon(bool show)
        {
            if (_notifier != null)
            {
                _notifier.Visible = show;
            }
        }

        void ShowNewDownloadNotification(string message)
        {
            _notifier.BalloonTipText = message;
            _notifier.ShowBalloonTip(1500);
        }
    }
}
