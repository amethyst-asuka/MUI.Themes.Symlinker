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
    /// Interaction logic for FileHard.xaml
    /// </summary>
    public partial class FileHard : UserControl
    {
        public FileHard()
        {
            InitializeComponent();
        }

        private void Original_File(object sender, RoutedEventArgs e)
        {
            BrowsedFile.Text = "You haven't selected a source yet.";
            HardLink.Text = "You haven't selected a hard link yet.";
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

        private void Hard_Link(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Save = new SaveFileDialog();
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Nullable<bool> FileSelected = Save.ShowDialog();
            if (FileSelected == true)
            {
                HardLink.Text = Save.FileName;
            }
        }

        private void HardLink_Create(object sender, RoutedEventArgs e)
        {
            if (BrowsedFile.Text == "You haven't selected a source yet.")
            {
                ModernDialog.ShowMessage("You didn't choose a source. Please do it.", "Oops!", MessageBoxButton.OK);
            }
            else if (HardLink.Text == "You haven't selected a hard link yet.")
            {
                ModernDialog.ShowMessage("You didn't choose a hard link. Please do it.", "Oops!", MessageBoxButton.OK);
            }
            else
            {
                if (File.Exists(HardLink.Text)) { File.Delete(HardLink.Text); }
                bool Sucess = MUI.Themes.Symlinker.HardLink.CreateHardLink(HardLink.Text, BrowsedFile.Text, IntPtr.Zero);
                if (Sucess == false)
                {
                    Color OldColor = AppearanceManager.Current.AccentColor;
                    AppearanceManager.Current.AccentColor = Color.FromRgb(0xe5, 0x14, 0x00);
                    if (!VistaSecurity.IsAdmin()) { ModernDialog.ShowMessage("There was an error creating the hard link! Probably because you are trying to create a hard link in an non-NTFS drive, you do not have the permissions to write here or you are creating links between drives. (You can restart the program as administrator at the home page)", "Oops!", MessageBoxButton.OK); }
                    else { ModernDialog.ShowMessage("There was an error creating the hard link! Probably because you are trying to create a hard link in an non-NTFS drive or you are creating links between drives.", "Oops!", MessageBoxButton.OK); }
                    AppearanceManager.Current.AccentColor = OldColor;
                }
                else
                {
                    ModernDialog.ShowMessage("The hard link was created with success.", "Success!", MessageBoxButton.OK);
                    BrowsedFile.Text = "You haven't selected a source yet.";
                    HardLink.Text = "You haven't selected a hard link yet.";
                }
            }
        }
    }
}