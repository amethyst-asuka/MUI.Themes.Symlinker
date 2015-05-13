using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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