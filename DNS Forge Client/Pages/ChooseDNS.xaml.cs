using System;
using System.Collections.Generic;
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
using System.Text.Json;
using DNS_Forge_Client.Entities;
using MessagePack;

namespace DNS_Forge_Client.Pages
{
    /// <summary>
    /// Interaction logic for ChooseDNS.xaml
    /// </summary>
    public partial class ChooseDNS : UserControl
    {
        DNS DNS_List;
        string DNS;

        public ChooseDNS()
        {
            InitializeComponent();
            CheckDNSOptions();   
        }

        private async Task CheckDNSOptions()
        {
            listBoxGlobal.Visibility = Visibility.Hidden;
            IRButton.Visibility = Visibility.Hidden;


            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(0.1)
            };

            HttpResponseMessage response = null;
            byte[] bytes;

            try
            {
                response = await client.PostAsync("https://raw.githubusercontent.com/MOHAMMADREZAMI/DNS-Forge-Client/refs/heads/master/DNS.json", null);
                string Json = await response.Content.ReadAsStringAsync();

                DNS_List = JsonSerializer.Deserialize<DNS>(Json);

                bytes = MessagePackSerializer.Serialize(DNS_List);
                System.IO.File.WriteAllBytes("DNS-Package", bytes);
            }
            catch(Exception ex)
            {
                try
                {
                    bytes = System.IO.File.ReadAllBytes("DNS-Package");
                    DNS_List = MessagePackSerializer.Deserialize<DNS>(bytes);
                }
                catch
                {
                    new Warning("           We Cannot Connect to server . \n      " +
                        "             Please Try again later. ").ShowDialog();
                    Window.GetWindow(this).Content = new MainPage();
                    return;
                }
                
            }
           
           


            foreach(DNS_Template Value in DNS_List.IR)
            {
                listBoxIR.Items.Add(Value.Name);
            }

            foreach(DNS_Template Value in DNS_List.Global)
            {
                listBoxGlobal.Items.Add(Value.Name);
            }


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

        private void listBoxGlobal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (DNS_Template Value in DNS_List.Global)
            {
                if (listBoxGlobal.SelectedItem == Value.Name)
                {
                    DNSimage.Source = new BitmapImage(new Uri(Value.Logo));

                    Description.Content = "Description : " + Value.Description + $" DNS is <<{Value.Primary} , {Value.Secondary}>>";

                    DNS = $"('{Value.Primary}','{Value.Secondary}')";

                    return;
                }
            }
        }

        private void listBoxIR_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (DNS_Template Value in DNS_List.IR)
            {
                if (listBoxIR.SelectedItem == Value.Name)
                {
                    DNSimage.Source = new BitmapImage(new Uri(Value.Logo));

                    Description.Content = "Description : " + Value.Description + $" DNS is <<{Value.Primary} , {Value.Secondary}>>";

                    DNS = $"('{Value.Primary}','{Value.Secondary}')";
                    return;
                }
            }

            

        }

        private void Switch_Global(object sender, RoutedEventArgs e)
        {
            GlobalButton.Visibility = Visibility.Hidden;
            IRButton.Visibility = Visibility.Visible;

            listBoxIR.Visibility = Visibility.Hidden;
            listBoxGlobal.Visibility = Visibility.Visible ;
        }

        private void Switch_IR(object sender, RoutedEventArgs e)
        {
            IRButton.Visibility = Visibility.Hidden;
            GlobalButton.Visibility = Visibility.Visible;

            listBoxGlobal.Visibility = Visibility.Hidden;
            listBoxIR.Visibility = Visibility.Visible ;
        }

        private void SetConfig(object sender, RoutedEventArgs e)
        {
            if (DNS is null)
            {
                new Warning("                     DNS Not Selected").ShowDialog();
                return;
            }
                

            MainPage.DNSAddress = DNS;
            new Set().ShowDialog();
            Window.GetWindow(this).Content = new MainPage();
        }

        private void listBoxGlobal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
