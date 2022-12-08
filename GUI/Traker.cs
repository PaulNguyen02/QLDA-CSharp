using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.GUI
{
    public partial class Traker : UserControl
    {
        private BUS.TrackerBUS tkbus;
        public Traker()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Traker_Load(object sender, EventArgs e)
        {
            tkbus = new BUS.TrackerBUS();
            for (int i = 0; i < tkbus.Query().Count; i++)
            {
                ListViewItem li = new ListViewItem(tkbus.Query().ElementAt(i).Id);
                li.SubItems.Add(tkbus.Query().ElementAt(i).Name);
                li.SubItems.Add(tkbus.Query().ElementAt(i).LastModify);
                li.SubItems.Add(tkbus.Query().ElementAt(i).User);                
                tb.Items.Add(li);
            }
            label2.Text = Convert.ToString(tkbus.Count());
        }
    }
}
