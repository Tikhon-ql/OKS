using OKS_1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OKS_1.Windows
{
    public partial class OutputWindow : Form
    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
