using NLog;
using OKS_1.Models;
using System.IO.Ports;
using System.Text;

namespace OKS_1.Windows
{
    public partial class OutputWindow : Form
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();

        public Port ReceiverPort { get; set; }
        public OutputWindow(string portName, Parity parity)
        {
            ReceiverPort = new ReceiverPort(ReceiveData, portName, parity);
            InitializeComponent();
        }

        private void ReceiveData(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                var messageBytes = ((ReceiverPort)ReceiverPort).Read();
                var message = Encoding.UTF8.GetString(messageBytes);
                richTextBox1.Text += message;
                logger.Info("Message received.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
