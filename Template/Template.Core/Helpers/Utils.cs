using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace Template.Core
{
    public static class Utils
    {
        private static readonly HashSet<string> uniques = new HashSet<string>();

        /// <summary>
        /// Random number generator.
        /// </summary>
        public static Random Rng { get; } = new Random();

        /// <summary>
        /// Takes in a <see cref="SecureString"/> and returns its data
        /// converted to <see cref="string"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Unsecure(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        /// Performs a unique operation only once. If an action
        /// with the same id is passed in, it won't be executed.
        /// </summary>
        /// <param name="uniqueId">Unique id to use to remember which
        /// ones were executed and which ones weren't.</param>
        /// <param name="action">Action to execute.</param>
        public static void PerformOnce(string uniqueId, Action action)
        {
            if (uniques.Add(uniqueId))
                action();
            else
                System.Diagnostics.Debugger.Break();
        }
    }
}
