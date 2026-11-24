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
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using MessagePack;
using DNS_Forge_Client.Entities;

namespace DNS_Forge_Client.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        
        //variabale
        public static string SelAdapter;
        public static string DNSAddress;
        
        public MainPage()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();

            StopButton.Visibility = Visibility.Hidden;
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window.GetWindow(this).DragMove();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window.GetWindow(this).DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void AdapterSetting(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new AdapterSetting();
        }

        private void ChooseDNS(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new ChooseDNS();
        }

        private void BenchMarkButton(object sender, RoutedEventArgs e)
        {
            if (SelAdapter is null)
            {
                new Warning("       Please Select Your Adapter first").ShowDialog();
                return;
            }

            Window.GetWindow(this).Content = new Benchmark();
        }


        private async void SET(object sender, RoutedEventArgs e)
        {
            if(SelAdapter is null || DNSAddress is  null)
            {
                new Warning().ShowDialog();
                return;
            }    

            SetButton.Visibility = Visibility.Hidden;
            StopButton.Visibility = Visibility.Visible;

            AdapterSettingButton.IsEnabled = false;
            ChooseButton.IsEnabled = false;
            BenchmarkButton.IsEnabled = false;

            await Process.Start(new ProcessStartInfo("powershell.exe", $"-Command \"Set-DnsClientServerAddress -InterfaceAlias '{SelAdapter}' -ServerAddresses {DNSAddress}\"") { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden }).WaitForExitAsync();
            
            
        }

        private async void STOP(object sender, RoutedEventArgs e)
        {
            SetButton.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Hidden;

            AdapterSettingButton.IsEnabled = true;
            ChooseButton.IsEnabled = true;
            BenchmarkButton.IsEnabled = true;

            await Process.Start(new ProcessStartInfo("powershell.exe", $"-Command \"Set-DnsClientServerAddress -InterfaceAlias '{SelAdapter}' -ResetServerAddresses\"") { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden }).WaitForExitAsync();
        }

        
    }
}
