using System;

namespace Template.Core
{
    /// <summary>
    /// Service used for working with window.
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Current page that is displayed.
        /// </summary>
        Page Page { get; set; }

        /// <summary>
        /// Window that is currently in focus.
        /// </summary>
        Window Window { get; set; }

        bool 

        /// <summary>
        /// Event that is called when the <see cref="Page"/> changes.
        /// </summary>
        event EventHandler<Page> PageChanged;

        /// <summary>
        /// Event that is called when the <see cref="Window"/> changes.
        /// </summary>
        event EventHandler<Window> WindowChanged;

        /// <summary>
        /// Allows window movement.
        /// </summary>
        void DragMove();

        /// <summary>
        /// Minimizes the current window.
        /// </summary>
        void Minimize();

        /// <summary>
        /// Maximizes or restores the current window's size.
        /// </summary>
        void MaximizeOrRestore();

        /// <summary>
        /// Closes the current window.
        /// </summary>
        void Close();

        /// <summary>
        /// Sets the width and height of the window.
        /// </summary>
        void SetWindowSize(double width, double height);
    }
}
