using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pion.UI
{
    public sealed class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public string ShowFolderBrowserDialog()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.ShowDialog();

            return folderBrowserDialog.SelectedPath;
        }
    }
}
