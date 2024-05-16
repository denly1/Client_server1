using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using static System.Net.Mime.MediaTypeNames;

namespace Client_server1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket;

        public MainWindow()
        {
            InitializeComponent();
            List<Socket> users = new List<Socket>();
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("26.89.178.79", 2000);
            ReceiveMassage();

        }

        /* private async void ListenToClients()
         {
             while (true)
             {
                 var client = await socket.AcceptAsync();
                 users.Add(client);
                 RecieveMassage(client);
             }
         }*/

        private async void ReceiveMassage(/*Socket c*/)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await socket.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                allMassage.Items.Add(message);

                /*foreach (var item in users)
                {
                    SendMessage(item, message);
                }*/
            }
        }
            
        private async void SendMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(bytes, SocketFlags.None);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(txt.Text);
        }
    }
}