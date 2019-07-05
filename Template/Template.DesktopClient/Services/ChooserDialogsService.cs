using Template.Core;
using System.Linq;
using System.Windows.Forms;

namespace Template.DesktopClient
{
    /// <summary>
    /// Implementation of <see cref="IChooserDialogsService"/>.
    /// </summary>
    public class ChooserDialogsService : IChooserDialogsService
    {
        /// <summary>
        /// Implementation of <see cref="IChooserDialogsService.ChooseDirectory(string, string)"/>.
        /// </summary>
        public string ChooseDirectory(string description, string selectedDirectory)
        {
            // Create a folder browser dialog
            var dialog = new FolderBrowserDialog
            {
                Description = description,
                SelectedPath = selectedDirectory
            };

            // If the user chose something...
            return dialog.ShowDialog() == DialogResult.OK
                // Return the selected directory
                ? dialog.SelectedPath
                // Otherwise, just return null
                : null;
        }

        private string[] Choose(string title, string selectedFile, bool allowMultiple, params string[] allowedExtensions)
        {
            // Create an open file dialog
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = allowMultiple,
                FileName = selectedFile,
                Title = title
            };

            // If there are extensions provided...
            if (!allowedExtensions.IsNullOrEmpty())
                // Specify them as is required by the API
                dialog.Filter = string.Join("|", allowedExtensions.Select(ext => $"{ext} files (*.{ext})|*.{ext}"));

            // If the user chose something...
            return dialog.ShowDialog() == DialogResult.OK
                // Return the selected file(s)
                ? dialog.FileNames
                // Otherwise, just return null
                : null;
        }

        /// <summary>
        /// Implementation of <see cref="IChooserDialogsService.ChooseFiles(string, string, string[])"/>.
        /// </summary>
        public string[] ChooseFiles(string title, string selectedFile = null, params string[] allowedExtensions)
            => Choose(title, selectedFile, true, allowedExtensions);

        /// <summary>
        /// Implementation of <see cref="IChooserDialogsService.ChooseFile(string, string, string[])"/>.
        /// </summary>
        public string ChooseFile(string title, string selectedFile, params string[] allowedExtensions)
            => Choose(title, selectedFile, false, allowedExtensions)?.ElementAt(0);
    }
}