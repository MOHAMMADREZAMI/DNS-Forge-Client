using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using DNS_Forge_Client.Entities;
using MessagePack;

namespace DNS_Forge_Client.Pages
{
    /// <summary>
    /// Interaction logic for ChooseDNS.xaml
    /// </summary>
    public partial class Benchmark : UserControl
    {

        List<string> addresses = new List<string>()
            {
                "https://www.google.com/",
                "https://www.wikipedia.org/",
                "https://github.com/",
                "https://www.microsoft.com/",
                "https://www.cloudflare.com/",
                "https://www.epicgames.com/",
                "https://store.steampowered.com/",
                "https://open.spotify.com/"
            };

        public Benchmark()
        {
            InitializeComponent();    
            BenchmarkDNS();

        }

        private async Task BenchmarkDNS()
        {

            byte[] bytes;
            ObservableCollection<DNS_Benchmark> benchmarksList = new();
            BenchmarkTable.ItemsSource = benchmarksList;
            DNS DNS_List = new DNS();

            

            try
            {
                bytes = System.IO.File.ReadAllBytes("DNS-Package");
                DNS_List = MessagePackSerializer.Deserialize<DNS>(bytes);
            }
            catch
            {
                new Warning("           We Cannot Connect to server . \n      " +
                        "             Please Try again later. ").ShowDialog();
                return;
            }
            

            foreach (DNS_Template Value in DNS_List.IR)
            {
                benchmarksList.Add(await Test($"('{Value.Secondary}','{Value.Secondary}')", Value.Name));
            }

            foreach (DNS_Template Value in DNS_List.Global)
            {
                benchmarksList.Add(await Test($"('{Value.Secondary}','{Value.Secondary}')", Value.Name));

            }

            
        }

        private async Task<DNS_Benchmark> Test(string DNSAddress , string Name)
        {
            float Availability = 0;
            double Latency = 0;
            double divide = 8;

            Process.Start(new ProcessStartInfo("cmd.exe", "/c ipconfig /flushdns") { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden }).WaitForExit();
            Process.Start(new ProcessStartInfo("powershell.exe", $"-Command \"Set-DnsClientServerAddress -InterfaceAlias '{MainPage.SelAdapter}' -ServerAddresses {DNSAddress}\"") { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden }).WaitForExit();
            HttpClient httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5),
            };

            foreach (string Value in addresses)
            {
                try
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    await httpClient.GetAsync(Value);
                    stopwatch.Stop();
                    Availability += 12.5f;
                    Latency += stopwatch.Elapsed.TotalMilliseconds;
                }
                catch (Exception ex)
                {
                    Availability += 0;
                    divide -= 1;
                }
                
            }

            Process.Start(new ProcessStartInfo("powershell.exe", $"-Command \"Set-DnsClientServerAddress -InterfaceAlias '{MainPage.SelAdapter}' -ResetServerAddresses\"") { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden }).WaitForExit();
            return new DNS_Benchmark()
            {
                Name = Name,
                Availability = Availability,
                Latency = (divide == 0) ? -1 : (Latency / divide) / 1000
            };

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

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new MainPage();
        }
        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
