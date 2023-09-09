using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKS_1.Models
{
    public class SenderPort : Port
    {
        public SenderPort(string serialPortName, Parity parity = Parity.None) : base(serialPortName, parity)
        {
        }

        public void Write(params byte[] bytes)
        {
            try
            {
                _serialPort.Write(bytes, 0, bytes.Length);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
