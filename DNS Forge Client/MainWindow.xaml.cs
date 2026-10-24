using System.IO;
using System.Security.Cryptography;
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
using DNS_Forge_Client.Entities;
using DNS_Forge_Client.Pages;
using MessagePack;

namespace DNS_Forge_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DataLoad();
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            this.Content = new MainPage();
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            DataLoad data = new DataLoad() { Adapter = MainPage.SelAdapter, DNS = MainPage.DNSAddress };
            byte[] bytes;

            bytes = MessagePackSerializer.Serialize(data);

            File.WriteAllBytes("DataLoad", bytes);
        }

        private void DataLoad()
        {
            byte[] bytes;

            try
            {
                bytes = File.ReadAllBytes("DataLoad");
                DataLoad data = MessagePackSerializer.Deserialize<DataLoad>(bytes);

                MainPage.SelAdapter = data.Adapter;
                MainPage.DNSAddress = data.DNS;
            }
            catch
            {
                return;
            }

        }


    }

}