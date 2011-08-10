using System.Windows.Forms;

namespace Pion.UI
{
    /// <summary>
    /// Responsible for gathering user input with dialogs using the WPF/Winforms System.
    /// </summary>
    public sealed class DialogService : IDialogService
    {
        /// <summary>
        /// Initializes a new instance of the DialogService class.
        /// </summary>
        public DialogService()
        {
        }

        /// <summary>
        /// Asks the user the choose a folder of his liking.
        /// </summary>
        /// <returns>The chosen folder. An empty string is returned if the user aborts the selection.</returns>
        public string ChooseFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.ShowDialog();

            return folderBrowserDialog.SelectedPath;
        }
    }
}
