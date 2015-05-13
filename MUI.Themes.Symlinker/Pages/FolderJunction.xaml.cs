using System.IO;
using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using FirstFloor.ModernUI.Windows.Controls;
using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;

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
                try {Monitor.Core.Utilities.JunctionPoint.Create(JunctionPoint.Text, BrowsedFolder.Text, true);}
                catch (Exception) { CatchException = true; }
                BrowsedFolder.Text = "You haven't selected a source yet.";
                JunctionPoint.Text = "You haven't selected a junction point yet.";
                if (CatchException == true)
                {
                    if (!VistaSecurity.IsAdmin()) { ModernDialog.ShowMessage("There was an error creating the junction point! Probably because you are trying to create a junction point in an non-NTFS drive, or you do not have the permissions to write here. (You can restart the program as administrator at the home page)", "Oops!", MessageBoxButton.OK);}
                    else { ModernDialog.ShowMessage("There was an error creating the junction point! Probably because you are trying to create a junction point in an non-NTFS drive.", "Oops!", MessageBoxButton.OK); }
                }
                else
                {
                    ModernDialog.ShowMessage("The junction point was created with success.", "Success!", MessageBoxButton.OK);
                }
            }
        }
    }
}
