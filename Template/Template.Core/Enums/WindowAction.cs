﻿namespace Template.Core
{
    /// <summary>
    /// Action that can be performed on window.
    /// </summary>
    public enum WindowAction
    {
        /// <summary>
        /// Close the window.
        /// </summary>
        Close,

        /// <summary>
        /// Hide the window.
        /// </summary>
        Hide,

        /// <summary>
        /// Leave the window shown, but don't let it be interactable.
        /// </summary>
        LeaveShown,

        /// <summary>
        /// Leave the window shown and interactable.
        /// </summary>
        LeaveShownAndInteractable
    }
}
