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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Prototype_3
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            DownloadPath.Text = Properties.Settings.Default.DownloadPath;
        }

        void ChangeDownloadPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog downloadPathChooser = new FolderBrowserDialog();

            downloadPathChooser.ShowDialog();

            DownloadPath.Text = downloadPathChooser.SelectedPath;

            PersistDownloadPath(downloadPathChooser.SelectedPath);
        }

        void PersistDownloadPath(string downloadPath)
        {
            Properties.Settings.Default.DownloadPath = downloadPath;
            Properties.Settings.Default.Save();
        }
    }
}
