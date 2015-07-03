using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MUI.Themes.Symlinker.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Admin_Rights(object sender, RoutedEventArgs e)
        {
            VistaSecurity.RestartElevated();
        }

        private void UAC_Initialized(object sender, EventArgs e)
        {
            if (VistaSecurity.IsAdmin()) { UAC.Visibility = System.Windows.Visibility.Collapsed; UACNote.Visibility = System.Windows.Visibility.Collapsed; }
        }
    }
}