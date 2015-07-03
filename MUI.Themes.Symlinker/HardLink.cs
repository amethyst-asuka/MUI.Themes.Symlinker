using System;
using System.Runtime.InteropServices;

namespace MUI.Themes.Symlinker
{
    /// <summary>
    /// Provides access to NTFS hard links in .Net.
    /// </summary>
    public partial class HardLink
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CreateHardLink(
        string lpFileName,
        string lpExistingFileName,
        IntPtr lpSecurityAttributes
        );
    }
}