using OKS_1.Models;
using OKS_1.Windows;
using System.IO.Ports;
using System.Text;

namespace OKS_1
{
    public partial class Form1 : Form
    {
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
        }

        private void SendMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var sendBytes = messageBytes.Append((byte)0).ToArray();
            ((SenderPort)_sender).Write(sendBytes);
            Statistics.SentByte += sendBytes.Length;
            //TODO: Add logs
        }

        private Tuple<string, string> GetFreePortNames()
        {
            var portNames = SerialPort.GetPortNames();
            int i = 0;
            for (; i < portNames.Length; i += 2)
            {
                try
                {
                    using var port = new SerialPort(portNames[i]);
                    port.Open();
                    break;
                }
                catch
                {

                }
            }
            int j = portNames.Length - 1;
            for (; j >= 0; j -= 2)
            {
                try
                {
                    using var port = new SerialPort(portNames[j]);
                    port.Open();
                    break;
                }
                catch
                {

                }
            }

            return new Tuple<string, string>(portNames[i], portNames[j]);
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
    }
}