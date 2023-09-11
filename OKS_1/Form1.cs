using NLog;
using OKS_1.Models;
using OKS_1.Windows;
using System.IO.Ports;
using System.Text;

namespace OKS_1
{
    public partial class Form1 : Form
    {
        private ILogger loger = LogManager.GetCurrentClassLogger();
        private Port _sender;

        private string _receiverPortName;
        private string _senderPortName;

        private SettingsWindow _settingsWindow;
        private OutputWindow _outputWindow;
        private InfoWindow _infoWindow;

        private bool _isOutputWindowOpened = false;

        private Parity _parity = Parity.None;

        public Form1()
        {
            InitializeComponent();
            _settingsWindow = new SettingsWindow();
            //_outputWindow = new OutputWindow();
            _infoWindow = new InfoWindow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage(textBox1.Text);
            textBox1.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var portNames = GetFreePortNames();
            _sender = new SenderPort(portNames.Item1);
            _receiverPortName = portNames.Item2;
            _senderPortName = portNames.Item1;
            StartListen();
            loger.Info("App loaded");
        }

        private void SendMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var sendBytes = messageBytes.Append((byte)0).ToArray();
            ((SenderPort)_sender).Write(sendBytes);
            Statistics.SentByte += sendBytes.Length;
            loger.Info("Message sent");
            //TODO: Add logs
        }

        private Tuple<string, string> GetFreePortNames()
        {
            var portNames = SerialPort.GetPortNames();
            int senderPortIndex = 0;
            for (int i = 0; i < portNames.Length; i += 2)
            {
                try
                {
                    using var port = new SerialPort(portNames[i]);
                    port.Open();
                    senderPortIndex = i;
                    break;
                }
                catch
                {

                }
            }
            int receiverPortIndex = portNames.Length - 1;
            for (int i = portNames.Length - 1; i >= 0; i -= 2)
            {
                try
                {
                    using var port = new SerialPort(portNames[i]);
                    port.Open();
                    receiverPortIndex = i;
                    break;
                }
                catch
                {

                }
            }

            return new Tuple<string, string>(portNames[senderPortIndex], portNames[receiverPortIndex]);
        }

        private void StartListen()
        {
            _outputWindow = new OutputWindow(_receiverPortName, _parity);
            _outputWindow.Show();
            _isOutputWindowOpened = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_settingsWindow.ShowDialog() == DialogResult.OK)
            {
                var parity = _settingsWindow.GetParity();
                _settingsWindow.Clear();

                loger.Info($"Parity changed from {_parity} to {parity}");

                _parity = parity;
                _sender.SetParity(parity);               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _infoWindow.SenderPortName = _senderPortName;
            _infoWindow.ReceiverPortName = _outputWindow.ReceiverPort.GetPorname();
            _infoWindow.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_receiverPortName))
            {
                MessageBox.Show("Receiver port name undefined", "Info", MessageBoxButtons.OK);
            }
            if(_isOutputWindowOpened)
            {
                _outputWindow.Hide();
                _isOutputWindowOpened = false;
            }
            else
            {
                _outputWindow.Show();
                _isOutputWindowOpened = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage(textBox1.Text);
                textBox1.Clear();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            var logsWindow = new LogsWindow();
            logsWindow.Show();
        }
    }
}