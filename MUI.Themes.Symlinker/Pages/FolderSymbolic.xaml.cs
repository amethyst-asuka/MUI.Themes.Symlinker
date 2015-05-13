using FirstFloor.ModernUI.Windows.Controls;
using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace MUI.Themes.Symlinker.Pages
{
    /// <summary>
    /// Interaction logic for FolderSymbolic.xaml
    /// </summary>
    public partial class FolderSymbolic : UserControl
    {
        public FolderSymbolic()
        {
            InitializeComponent();
        }
        private void Original_Folder(object sender, RoutedEventArgs e)
        {
            BrowsedFolder.Text = "You haven't selected a source yet.";
            Symbolink.Text = "You haven't selected a symbolic link yet.";
            System.Windows.Forms.FolderBrowserDialog Browse = new System.Windows.Forms.FolderBrowserDialog();
            Browse.Description = "Choose the original folder where the symbolic link will refer:";
            Browse.RootFolder = System.Environment.SpecialFolder.Desktop;
            Browse.ShowNewFolderButton = true;
            System.Windows.Forms.DialogResult result = Browse.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                BrowsedFolder.Text = Browse.SelectedPath;
            }
        }

        private void Symbolink_Link(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog Browse = new System.Windows.Forms.FolderBrowserDialog();
            Browse.Description = "Choose where the symbolic link will be:";
            Browse.RootFolder = System.Environment.SpecialFolder.Desktop;
            Browse.ShowNewFolderButton = true;
            System.Windows.Forms.DialogResult result = Browse.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Symbolink.Text = Browse.SelectedPath;
            }
        }

        private void Symlink_Create(object sender, RoutedEventArgs e)
        {
            if (BrowsedFolder.Text == "You haven't selected a source yet.")
            {
                ModernDialog.ShowMessage("You didn't choose a source. Please do it.", "Oops!", MessageBoxButton.OK);
            }
            else if (Symbolink.Text == "You haven't selected a symbolic link yet.")
            {
                ModernDialog.ShowMessage("You didn't choose a symbolic link. Please do it.", "Oops!", MessageBoxButton.OK);
            }
            else
            {
                bool CatchException = false;
                try
                {
                    if (Directory.Exists(Symbolink.Text)) { Directory.Delete(Symbolink.Text, true); }
                    MUI.Themes.Symlinker.Symlink.CreateSymbolicLink(Symbolink.Text, BrowsedFolder.Text, MUI.Themes.Symlinker.Symlink.SymbolicLink.Directory);
                }
                catch (Exception) { CatchException = true; }
                BrowsedFolder.Text = "You haven't selected a source yet.";
                Symbolink.Text = "You haven't selected a symbolic link yet.";
                if (CatchException == true)
                {
                    ModernDialog.ShowMessage("There was an error creating the symbolic link! Probably because you are trying to create a symbolic link in an non-NTFS drive.", "Oops!", MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("The symbolic link was created with success.", "Success!", MessageBoxButton.OK);
                }
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (!VistaSecurity.IsAdmin())
            {
                NoAdmin.Visibility = System.Windows.Visibility.Visible;
                Admin.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void Admin_Rights(object sender, RoutedEventArgs e)
        {
            VistaSecurity.RestartElevated();
        }
    }
}
