using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace MUI.Themes.Symlinker.Pages
{
    /// <summary>
    /// Interaction logic for FolderJunction.xaml
    /// </summary>
    public partial class FolderJunction : System.Windows.Controls.UserControl
    {
        public FolderJunction()
        {
            InitializeComponent();
        }

        private void Original_Folder(object sender, RoutedEventArgs e)
        {
            BrowsedFolder.Text = "You haven't selected a source yet.";
            JunctionPoint.Text = "You haven't selected a junction point yet.";
            FolderBrowserDialog Browse = new FolderBrowserDialog();
            Browse.Description = "Choose the original folder where the junction point will refer:";
            Browse.RootFolder = System.Environment.SpecialFolder.Desktop;
            Browse.ShowNewFolderButton = true;
            DialogResult result = Browse.ShowDialog();
            if (result == DialogResult.OK)
            {
                BrowsedFolder.Text = Browse.SelectedPath;
            }
        }

        private void Junction_Folder(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog Browse = new FolderBrowserDialog();
            Browse.Description = "Choose where the junction point will be:";
            Browse.RootFolder = System.Environment.SpecialFolder.Desktop;
            Browse.ShowNewFolderButton = true;
            DialogResult result = Browse.ShowDialog();
            if (result == DialogResult.OK)
            {
                JunctionPoint.Text = Browse.SelectedPath;
            }
        }

        private void Junction_Create(object sender, RoutedEventArgs e)
        {
            if (BrowsedFolder.Text == "You haven't selected a source yet.")
            {
                ModernDialog.ShowMessage("You didn't choose a source. Please do it.", "Oops!", MessageBoxButton.OK);
            }
            else if (JunctionPoint.Text == "You haven't selected a junction point yet.")
            {
                ModernDialog.ShowMessage("You didn't choose a junction point. Please do it.", "Oops!", MessageBoxButton.OK);
            }
            else
            {
                bool CatchException = false;
                try { Monitor.Core.Utilities.JunctionPoint.Create(JunctionPoint.Text, BrowsedFolder.Text, true); }
                catch (Exception) { CatchException = true; }
                if (CatchException == true)
                {
                    Color OldColor = AppearanceManager.Current.AccentColor;
                    AppearanceManager.Current.AccentColor = Color.FromRgb(0xe5, 0x14, 0x00);
                    if (!VistaSecurity.IsAdmin()) { ModernDialog.ShowMessage("There was an error creating the junction point! Probably because you are trying to create a junction point in an non-NTFS drive, you do not have the permissions to write here or you are creating junction points between drives. (You can restart the program as administrator at the home page)", "Oops!", MessageBoxButton.OK); }
                    else { ModernDialog.ShowMessage("There was an error creating the junction point! Probably because you are trying to create a junction point in an non-NTFS drive or you are creating junction points between drives.", "Oops!", MessageBoxButton.OK); }
                    AppearanceManager.Current.AccentColor = OldColor;
                }
                else
                {
                    ModernDialog.ShowMessage("The junction point was created with success.", "Success!", MessageBoxButton.OK);
                    BrowsedFolder.Text = "You haven't selected a source yet.";
                    JunctionPoint.Text = "You haven't selected a junction point yet.";
                }
            }
        }
    }
}