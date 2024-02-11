using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading;

namespace ASCII_Assault_App
{
    public partial class MainForm : Form
    {
        private TcpClient tcpClient;
        private NetworkStream clientStream;

        private TextBox textBoxOutput;
        private TextBox textBoxMessage;
        private Button buttonSendMessage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            textBoxOutput = new TextBox();
            textBoxOutput.Multiline = true;
            textBoxOutput.Dock = DockStyle.Fill;

            textBoxMessage = new TextBox();
            textBoxMessage.Dock = DockStyle.Bottom;

            buttonSendMessage = new Button();
            buttonSendMessage.Text = "Send Message";
            buttonSendMessage.Dock = DockStyle.Bottom;
            buttonSendMessage.Click += buttonSendMessage_Click;

            Controls.Add(textBoxOutput);
            Controls.Add(textBoxMessage);
            Controls.Add(buttonSendMessage);

            // Other GUI initialization logic
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            string messageToSend = textBoxMessage.Text;
            SendMessage(messageToSend);
        }

        private void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            clientStream.Write(messageBytes, 0, messageBytes.Length);
            clientStream.Flush();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeClient();
        }

        private void InitializeClient()
        {
            // Implement your code to initialize the client connection
            // This is a placeholder for demonstration purposes
            displayMessage("Client initialized.");

            string serverIp = "127.0.0.1";
            int serverPort = 6969;

            try
            {
                tcpClient = new TcpClient(serverIp, serverPort);
                clientStream = tcpClient.GetStream();

                // Start a separate thread for receiving messages
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
                receiveThread.Start();

                displayMessage("Connected to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the server: " + ex.Message);
            }
        }

        private void ReceiveMessages()
        {

            while (true)
            {
                try
                {
                    byte[] messageBytes = new byte[4096];
                    int bytesRead = clientStream.Read(messageBytes, 0, messageBytes.Length);
                    if (bytesRead == 0)
                        break;

                    string message = Encoding.ASCII.GetString(messageBytes, 0, bytesRead);

                    displayMessage(message);

                    Console.WriteLine(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    break;
                }
            }
        }
        private void displayMessage(string message)
        {
            BeginInvoke((Action)(() =>
            {
                textBoxOutput.AppendText(message + Environment.NewLine);
            }));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Implement any cleanup logic here, such as closing the network connection
            // This is a placeholder for demonstration purposes
            displayMessage("Closing the application...");
            tcpClient?.Close();
        }
    }

    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}