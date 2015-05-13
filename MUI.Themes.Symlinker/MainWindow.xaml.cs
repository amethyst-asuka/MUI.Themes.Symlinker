using FirstFloor.ModernUI.Windows.Controls;
using hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton;
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
