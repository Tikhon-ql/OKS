namespace OKS_1.Windows
{
    public partial class LogsWindow : Form
    {
        public LogsWindow()
        {
            InitializeComponent();
            var logs = ReadLogsFromFile("logs/logs.log");
            logs = logs.Replace("|", " ");
            richTextBox1.Text += logs;
        }

        private string ReadLogsFromFile(string path)
        {
            var stream = File.Open(path,FileMode.Open,FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
