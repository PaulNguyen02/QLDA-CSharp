using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
namespace WindowsFormsApp1
{
    public partial class Signin : Form
    {
        private int count = 0, count1 = 0;
        private BUS.CheckSigninBUS csb;
        private GUI.LoadingTab ld;
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.mymusic1);       //Nhạc nền
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        public Signin()
        {
            InitializeComponent();            
            player.PlayLooping();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public bool IsConnectedToInternet(string host)
        {
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(host, 3000);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {


            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsConnectedToInternet("smtp-mail.outlook.com") && IsConnectedToInternet("imap-mail.outlook.com"))
            {
                if (tf1.Text == "" || IsNumber(tf1.Text) == false || tf2.Text == "")
                {
                    MessageBox.Show("Hãy điền ID và mật khẩu !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    csb = new BUS.CheckSigninBUS();
                    String id = tf1.Text;
                    String pass = tf2.Text;
                    if (csb.Checked(id, pass) == true)
                    {
                        sfx.Play();
                        if (checkBox1.Checked == true)
                        {
                            Properties.Settings.Default.ID = id;
                            Properties.Settings.Default.Pass = pass;
                            Properties.Settings.Default.Save();
                        }
                        Form1 f = new Form1();
                        f.SetAcc(csb.getEmail(id), csb.getPass(id));
                        f.Visible = true;
                        player.Stop();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bạn điền sai mật khẩu", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                while (IsConnectedToInternet("smtp-mail.outlook.com") == false || IsConnectedToInternet("imap-mail.outlook.com") == false)
                {
                    ld = new GUI.LoadingTab();
                    panel1.Controls.Clear();
                    panel1.Controls.Add(ld);
                    MessageBox.Show("Tình trạng kết nối: Mất!", "Kết nối thất bại");
                }
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Đã có kết nối hãy tắt ứng dụng và mở lại !", "Kết nối thành công", buttons);
                if (result == DialogResult.OK)                
                    this.Close();                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Signin_Load(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.ID!=string.Empty)
            {
                bool flag = Properties.Settings.Default.State;               
                if(flag==true)
                {
                    checkBox1.Checked = true;   //Hiển thị tick trên checbox
                    tf1.Text = Properties.Settings.Default.ID;
                    tf2.Text = Properties.Settings.Default.Pass;
                }
                else
                {
                    checkBox1.Checked = false;                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {           
            count++;
            if(count%2==1)      //Quy định số lần click là lẻ thì tắt nhạc
            {
                player.Stop();               
            }
            else
            {
                player.Play();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==false)
            {
                Properties.Settings.Default.State = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.State = true;
                Properties.Settings.Default.Save();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sfx.Play();
            GUI.Signup sg = new GUI.Signup();
            sg.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
