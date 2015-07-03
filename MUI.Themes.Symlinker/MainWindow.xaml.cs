using FirstFloor.ModernUI.Windows.Controls;
using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;
using System;

namespace MUI.Themes.Symlinker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ModernWindow_Initialized(object sender, EventArgs e)
        {
            if (VistaSecurity.IsAdmin())
            {
                this.Title += " (Elevated)";
            }
        }
    }
}