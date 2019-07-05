using System;

namespace Template.Core
{
    /// <summary>
    /// Event args for <see cref="IWindowService.PageChanged"/> event.
    /// </summary>
    public class PageChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Page that changed.
        /// </summary>
        public Page Page { get; set; }

        /// <summary>
        /// Window on which the page changed.
        /// </summary>
        public Window Window { get; set; }
    }

    /// <summary>
    /// Service used for working with window.
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Event that triggers once a page is changed.
        /// </summary>
        event EventHandler<PageChangedEventArgs> PageChanged;

        /// <summary>
        /// Opens a window if it isn't already open.
        /// </summary>
        /// <param name="window">Window to open.</param>
        /// <param name="previousWindowAction">What to do with previous window.</param>
        bool Open(Window window, WindowAction? previousWindowAction);

        /// <summary>
        /// Changes the page to the specified one on the specified window.
        /// </summary>
        /// <param name="page">Page to change to.</param>
        /// <param name="window">Window on which the page should be changed.
        /// <para>If left null, current window will be used.</para>
        /// </param>
        bool ChangePage(Page page, Window? window = null);

        /// <summary>
        /// Closes the specified window if it is currently open.
        /// </summary>
        /// <param name="window">Window to close.
        /// <para>If left null, current window will be used.</para>
        /// </param>
        bool Close(Window? window = null);

        /// <summary>
        /// Tries to move the specified window on drag.
        /// </summary>
        /// <param name="window">Window to move.
        /// <para>If left null, current window will be used.</para>
        /// </param>
        /// <returns></returns>
        bool DragMove(Window? window = null);

        /// <summary>
        /// Minimizes the specified window.
        /// </summary>
        /// <param name="window">Window to minimize.
        /// <para>If left null, current window will be used.</para>
        /// </param>
        /// <returns></returns>
        bool Minimize(Window? window = null);

        /// <summary>
        /// Maximizes or restores the specified window's size.
        /// </summary>
        /// <param name="window">Window to maximize or restore.
        /// <para>If left null, current window will be used.</para>
        /// </param>
        /// <returns></returns>
        bool MaximizeOrRestore(Window? window = null);

        /// <summary>
        /// Changes the size of the specified window.
        /// </summary>
        /// <param name="width">Desired width of the window.</param>
        /// <param name="height">Desired height of the window.</param>
        /// <param name="window">Window whose size needs to be changed.
        /// <para>If left null, current window will be used.</para>
        /// </param>
        /// <returns></returns>
        bool ChangeSize(double width, double height, Window? window = null);
    }
}
