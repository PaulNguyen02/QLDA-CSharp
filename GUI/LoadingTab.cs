using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1.GUI
{
    public partial class LoadingTab : UserControl
    {
        private int count = 0;
        public LoadingTab()
        {
            InitializeComponent();
            timer1.Start();
        }          
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformLayout();
        }        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadingTab_Load(object sender, EventArgs e)
        {
            
        }
    }
}
