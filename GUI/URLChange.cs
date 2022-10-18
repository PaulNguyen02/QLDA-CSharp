using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.GUI
{
    public partial class URLChange : Form
    {
        public URLChange()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Nhập đường dẫn");
            }
            else
            {
                Properties.Settings.Default.URL = textBox1.Text;
                Properties.Settings.Default.Save();
            }
        }
    }
}
