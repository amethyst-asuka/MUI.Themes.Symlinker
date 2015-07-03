using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MUI.Themes.Symlinker.Pages
{
    /// <summary>
    /// Interaction logic for FileSymbolic.xaml
    /// </summary>
    public partial class FileSymbolic : UserControl
    {
        public FileSymbolic()
        {
            InitializeComponent();
        }

        private void Original_File(object sender, RoutedEventArgs e)
        {
            BrowsedFile.Text = "You haven't selected a source yet.";
            Symbolink.Text = "You haven't selected a symbolic link yet.";
            OpenFileDialog Browse = new OpenFileDialog();
            Browse.Title = "Browse...";
            Browse.InitialDirectory = "Desktop";
            Browse.CheckFileExists = true;
            Browse.CheckPathExists = true;
            Browse.ValidateNames = true;
            Browse.Multiselect = false;
            Nullable<bool> FileSelected = Browse.ShowDialog();
            if (FileSelected == true)
            {
                BrowsedFile.Text = Browse.FileName;
            }
        }

        private void Symbolink_Link(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Save = new SaveFileDialog();
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Nullable<bool> FileSelected = Save.ShowDialog();
            if (FileSelected == true)
            {
                Symbolink.Text = Save.FileName;
            }
        }

        private void Symlink_Create(object sender, RoutedEventArgs e)
        {
            if (BrowsedFile.Text == "You haven't selected a source yet.")
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
                    if (File.Exists(Symbolink.Text)) { File.Delete(Symbolink.Text); }
                    MUI.Themes.Symlinker.Symlink.CreateSymbolicLink(Symbolink.Text, BrowsedFile.Text, MUI.Themes.Symlinker.Symlink.SymbolicLink.File);
                }
                catch (Exception) { CatchException = true; }
                if (CatchException == true)
                {
                    Color OldColor = AppearanceManager.Current.AccentColor;
                    AppearanceManager.Current.AccentColor = Color.FromRgb(0xe5, 0x14, 0x00);
                    ModernDialog.ShowMessage("There was an error creating the symbolic link! Probably because you are trying to create a symbolic link in an non-NTFS drive.", "Oops!", MessageBoxButton.OK);
                    AppearanceManager.Current.AccentColor = OldColor;
                }
                else
                {
                    BrowsedFile.Text = "You haven't selected a source yet.";
                    Symbolink.Text = "You haven't selected a symbolic link yet.";
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