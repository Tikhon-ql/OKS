using System.IO.Ports;

namespace OKS_1.Models
{
    public class ReceiverPort : Port
    {
        public ReceiverPort(SerialDataReceivedEventHandler handler,string portName):base(portName)
        {
            _serialPort.DataReceived += handler;
        }

        public ReceiverPort(SerialDataReceivedEventHandler handler, string serialPortName, Parity parity = Parity.None) : base(serialPortName, parity)
        {
            _serialPort.DataReceived += handler;
        }

        public byte[] Read()
        {
            var buffer = new byte[1024];
            _serialPort.Read(buffer, 0, buffer.Length);
            var messageBuf = buffer.TakeWhile(b=>b != 0).ToArray();
            return messageBuf;
        }
    }
}
