namespace Template.Core
{
    /// <summary>
    /// Service used for working with dialogs that allow
    /// the user to choose a file or folder location.
    /// </summary>
    public interface IChooserDialogsService
    {
        /// <summary>
        /// Allows the user to choose a file.
        /// </summary>
        /// <param name="title">Title displayed on file chooser dialog.</param>
        /// <param name="selectedFile">File that is selected in advance.</param>
        /// <param name="allowedExtensions">Extensions for files that can be picked.</param>
        /// <returns></returns>
        string ChooseFile(string title, string selectedFile = null, params string[] allowedExtensions);

        /// <summary>
        /// Allows the user to choose multiple files.
        /// </summary>
        /// <param name="title">Title displayed on file chooser dialog.</param>
        /// <param name="selectedFile">File that is selected in advance.</param>
        /// <param name="allowedExtensions">Extensions for files that can be picked.</param>
        /// <returns></returns>
        string[] ChooseFiles(string title, string selectedFile = null, params string[] allowedExtensions);

        /// <summary>
        /// Allows the user to choose a directory.
        /// </summary>
        /// <param name="description">Description to display on the directory chooser dialog.</param>
        /// <param name="selectedDirectory">Directory that is selected in advance.</param>
        /// <returns></returns>
        string ChooseDirectory(string description, string selectedDirectory = null);
    }
}