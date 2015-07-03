using System.Runtime.InteropServices;

namespace MUI.Themes.Symlinker
{
    /// <summary>
    /// Provides access to NTFS symbolic links in .Net.
    /// </summary>
    public partial class Symlink
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(
        string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        public enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }
    }
}