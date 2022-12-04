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
    public partial class EditAccount : Form
    {
        private string user, password, newid, newname, newdob, newpos, newemail, newpass ;
        private DTO.Member mb;
        private BUS.EditAcountBUS mbus;
        public EditAccount()
        {
            InitializeComponent();
        }
        public void SetAcc(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==" "|| textBox2.Text==" "||textBox3.Text==" "||textBox4.Text==" ")
            {
                MessageBox.Show("Điền thông tin để sửa","");
            }
            else
            {
                newid = textBox1.Text;
                newname = textBox2.Text;
                newdob= dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
                if (comboBox1.Text == Convert.ToString(comboBox1.SelectedItem))
                {
                    newpos = comboBox1.Text;
                }
                newemail = textBox3.Text;
                newpass = textBox4.Text;
                mb = new DTO.Member() { Id = newid, Name = newname, DoB = newdob, Pos = newpos, Email = newemail, Pass = newpass };
                mbus = new BUS.EditAcountBUS();
                mbus.Update(mb,mbus.getID(user));
            }
        }
    }
}
