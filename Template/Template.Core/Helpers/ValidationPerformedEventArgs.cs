﻿using System;
using System.Collections.Generic;

namespace Template.Core
{
    /// <summary>
    /// Event arguments for the event that will be raised
    /// after a validation has been performed.
    /// </summary>
    public class ValidationPerformedEventArgs : EventArgs
    {
        /// <summary>
        /// List of validations for every property.
        /// </summary>
        public List<Validations> Validations { get; set; }
    }
}
