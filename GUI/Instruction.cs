using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Instruction : Form
    {
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.mymusic1);
        public Instruction()
        {
            InitializeComponent();            
            player.Play();
        }

        private void Instruction_Load(object sender, EventArgs e)
        {

        }

        private void Instruction_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
