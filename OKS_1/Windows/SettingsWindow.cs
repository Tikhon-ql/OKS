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
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
            var parities = new List<string> { "Even", "Mark", "None", "Odd", "Space" };
            parities.ForEach(item =>
            {
                comboBox1.Items.Add(item);
            });
            comboBox1.SelectedIndex = 0;
        }

        public Parity GetParity()
        {
            return (Parity)Enum.Parse(typeof(Parity), comboBox1.SelectedItem.ToString());
        }
        public void Clear()
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
