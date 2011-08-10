
namespace Pion.UI
{
    /// <summary>
    /// Responsible for gathering input from the user using dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Asks the user to choose a folder.
        /// </summary>
        /// <returns>The chosen folder. An empty string is returned if the user aborted the selection.</returns>
        string ChooseFolder();
    }
}
