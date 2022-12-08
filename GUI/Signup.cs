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
    public partial class Signup : Form
    {
        private int count1 = 0;
        private DTO.Member mb = new DTO.Member();
        private BUS.MemberBUS mbus = new BUS.MemberBUS();
        private String id, name, dob, pos, email, pass;
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        private void button3_Click(object sender, EventArgs e)
        {
            sfx.Play();
            count1++;
            if (count1 % 2 == 1)      //Quy định số lần click là lẻ thì tắt nhạc
            {
                tf2.PasswordChar = '\0';        //Hiển thị password                
            }
            else
            {
                tf2.PasswordChar = '*';
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sfx.Play();
            this.Close();
        }

        public Signup()
        {
            InitializeComponent();
        }
        private int CountChar(string pValue)     //Đếm số lượng ký tự trong chuỗi
        {
            int count = 0;
            foreach (Char c in pValue)
            {
                count++;
            }
            return count;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf1.Text) > 10 || tf1.Text == "" || tf2.Text == "" || tf4.Text == "" || tf5.Text == "" || cbx.Text == "Vị trí")
            {
                MessageBox.Show("Làm ơn điền đầy đủ, mã số phải là số và nhỏ hơn 10 kí tự", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                id = tf1.Text;
                name = tf2.Text;
                dob = dtpk.Value.Year.ToString() + "-" + dtpk.Value.Month.ToString() + "-" + dtpk.Value.Day.ToString();
                if (cbx.Text == Convert.ToString(cbx.SelectedItem))
                {
                    pos = cbx.Text;
                }
                email = tf4.Text;
                pass = tf5.Text;
                mb = new DTO.Member() { Id = id, Name = name, DoB = dob, Pos = pos, Email = email, Pass = pass };                
                mbus = new BUS.MemberBUS();
                mbus.Add(mb);
            }
        }
    }
}
