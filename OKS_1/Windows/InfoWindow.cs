namespace OKS_1.Windows
{
    public partial class InfoWindow : Form
    {

        public string SenderPortName { get; set; }
        public string ReceiverPortName { get; set; }

        public InfoWindow()
        {
            InitializeComponent();
        }

        private void InfoWindow_Shown(object sender, EventArgs e)
        {
            string info = $"Sender port name: {SenderPortName}\n" +
                          $"Receiver port name: {ReceiverPortName}\n" +
                          $"Sent bytes: {Statistics.SentByte}";
            richTextBox1.Text = info;
        }
    }
}
