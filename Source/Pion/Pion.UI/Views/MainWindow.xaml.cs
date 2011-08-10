using System;
using System.Windows;
using System.Windows.Interop;
using Pion.Infrastructure.Common;
using Pion.UI.ViewModels;

namespace Pion.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        void HandleClipboardUpdateMessage()
        {
            if (!CheckIfClipboardContainsValidData())
            {
                return;
            }

            string copiedText = Clipboard.GetText();

            MainWindowViewModel mainWindowVm = (MainWindowViewModel)this.DataContext;

            mainWindowVm.Download(copiedText);
        }

        void RegisterClipboardListener()
        {
            NativeMethods.AddClipboardFormatListener(GetCurrentWindowHandle());
        }

        void RemoveClipboardListener()
        {
            NativeMethods.RemoveClipboardFormatListener(GetCurrentWindowHandle());
        }

        void ShowSettingsEventHandler(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();

            settings.DataContext = ServiceLocator.Resolve<SettingsViewModel>();

            settings.ShowDialog();
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
    }
}
