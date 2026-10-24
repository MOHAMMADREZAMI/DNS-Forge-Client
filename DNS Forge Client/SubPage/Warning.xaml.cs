using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DNS_Forge_Client.Pages;

namespace DNS_Forge_Client
{
    
    public partial class Warning : Window
    {
        

        public Warning(string Content = "")
        {
            InitializeComponent();
            ContentWarning.FontStretch = FontStretches.Medium;
            if(Content != "")
            {
                ContentWarning.Content = Content;
            } 
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            Close();
        }

        


    }

}