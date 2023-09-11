using System.IO.Ports;

namespace OKS_1.Models
{
    public class Port : IDisposable
    {

        protected SerialPort _serialPort;
        //public Port(SerialPort serialPort)
        //{
        //    _serialPort = serialPort;
        //}

        public Port(string portName)
        {
            _serialPort = new SerialPort();
            _serialPort.BaudRate = 19200;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Open();
        }

        public Port(string serialPortName, Parity parity = Parity.None)
        {
            _serialPort = new SerialPort(serialPortName,19200,parity, 8,StopBits.One);      
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.Open();
        }

        public void SetParity(Parity parity) => _serialPort.Parity = parity;
        public void SetPortname(string portName) => _serialPort.PortName = portName;

        public string GetPorname() => _serialPort.PortName;

        public void Dispose()
        {
            _serialPort.Dispose();
        }
    }
}
