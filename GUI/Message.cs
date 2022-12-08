using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
namespace WindowsFormsApp1.GUI
{
    public partial class Message : UserControl
    {
        private static String emails, password, sent;       //Sent là email cần gửi
        private string smtpText = "smtp-mail.outlook.com";
        private List<Button> list = new List<Button>();
        private DAO.MailRepository mr;
        private string[] str2 = new string[30];
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        private BUS.MessageBUS msbus;
        public Message()
        {
            InitializeComponent();            
        }
        public void SetAcc(String email, String pass)
        {
            emails = email;
            password = pass;
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            sfx.Play();
            string fileName = "";
            using (OpenFileDialog myDialog = new OpenFileDialog())
            {
                myDialog.Multiselect = true;
                myDialog.InitialDirectory = "";
                myDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.xml";
                myDialog.FilterIndex = 2;
                myDialog.RestoreDirectory = true;

                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in myDialog.FileNames)
                    {
                        fileName = Path.GetFileName(file);
                    }
                }
                str2 = myDialog.FileNames;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sfx.Play();
            string fileName = "";
            using (OpenFileDialog myDialog = new OpenFileDialog())
            {
                myDialog.Multiselect = true;
                myDialog.InitialDirectory = "";
                myDialog.Filter = "Video Files|*.mkv;*.mp4"; 
                myDialog.FilterIndex = 2;
                myDialog.RestoreDirectory = true;

                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in myDialog.FileNames)
                    {
                        fileName = Path.GetFileName(file);
                    }
                }
                str2 = myDialog.FileNames;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sfx.Play();            
            //Thread thread = new Thread(() =>        //Sử dụng đa luồng để 1 lúc gửi n tin nhắn
            //{
                sent = label4.Text;
                MailMessage mail = new MailMessage(emails, sent, textBox5.Text, textBox6.Text);
                foreach (string names in str2)      // Đính kèm nhiều file cùng loại
                {
                    if (names != null)
                    {
                        mail.Attachments.Add(new Attachment(names));
                        MessageBox.Show("Đã tải " + names + " thành công!", "Upload File");
                    }
                }
                mail.IsBodyHtml = true;
                //Cấu hình thông tin máy chủ cần thiết cho máy khách
                SmtpClient client = new SmtpClient(smtpText);
                client.Host = "smtp-mail.outlook.com";
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(emails, password);
                client.EnableSsl = true;                // SSL là mã hóa thông tin gửi đi
                client.Send(mail);                      //Gửi mail
                textBox4.AppendText(mail.Subject);           // Vừa gửi xong vừa hiện thị lên khung chat
                textBox4.AppendText(Environment.NewLine);
                textBox4.AppendText(mail.Body);
                textBox4.AppendText(Environment.NewLine);
            //}
            //);
            //thread.Start();
        }
        public void CreateButton()
        {
            mr = new DAO.MailRepository("imap-mail.outlook.com", 993, true, emails, password);
            int top = 0;       //Xác định căn lề trên
            int left = 0;     // Xác định căn lề trái
            foreach (DTO.Messages m in mr.LoadMail())
            {
                Button button = new Button();
                button.Text = m.sender;
                button.Left = left;
                button.Top = top;
                button.Height = 50;
                button.Width = 300;
                button.ForeColor = Color.Black;
                panel5.Controls.Add(button);
                top += button.Height + 2;
                list.Add(button);
            }
            for (int i = 0; i < list.Count; i++)
            {
                Button b = list.ElementAt(i);
                b.Click += new EventHandler(button_Click);
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            label4.Text = b.Text;
            sfx.Play();
            textBox4.Clear();
            LoadMessageContent(b.Text);
        }
        private void SetNum()
        {
            mr = new DAO.MailRepository("imap-mail.outlook.com", 993, true, emails, password);
            String num = Convert.ToString(mr.getIbcount());
            label5.Text = num;
        }
        private void Message_Load(object sender, EventArgs e)
        {
            SetNum();
            CreateButton();
        }
        private void LoadMessageContent(String email)
        {
            mr = new DAO.MailRepository("imap-mail.outlook.com", 993, true, emails, password);
            foreach (DTO.Messages m in mr.LoadSpecifiedMail(email))
            {
                if (m.content == null)
                {
                    textBox4.AppendText(m.datetime);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.sender);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.subject);
                    textBox4.AppendText(Environment.NewLine);
                }
                else if (m.subject == null)
                {
                    textBox4.AppendText(m.datetime);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.sender);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.content);
                    textBox4.AppendText(Environment.NewLine);
                }
                else
                {
                    textBox4.AppendText(m.datetime);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.sender);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.subject);
                    textBox4.AppendText(Environment.NewLine);
                    textBox4.AppendText(m.content);
                    textBox4.AppendText(Environment.NewLine);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            string fileName = "";
            using (OpenFileDialog myDialog = new OpenFileDialog())
            {
                myDialog.Multiselect = true;
                myDialog.InitialDirectory = "";
                myDialog.Filter = "Music Files|*.wav;*.mp3";
                myDialog.FilterIndex = 2;
                myDialog.RestoreDirectory = true;

                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in myDialog.FileNames)
                    {
                        fileName = Path.GetFileName(file);
                    }
                }
                str2 = myDialog.FileNames;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sfx.Play();
            if (comboBox1.SelectedIndex == 0)
            {
                string fileName = ""; //khởi tạo ban đầu
                                      //open tệp tin với component OpenFileDialog
                using (OpenFileDialog myDialog = new OpenFileDialog())
                {
                    myDialog.Multiselect = true;        //Cho phép chọn n tập tin
                    myDialog.InitialDirectory = "";     //Khởi tạo đường dẫn                                                      
                    myDialog.Filter = "Pdf Files|*.pdf";
                    myDialog.FilterIndex = 2;
                    myDialog.RestoreDirectory = true;

                    if (myDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in myDialog.FileNames)
                        //cứ sau mỗi lần lặp ta lại có được một file, điều này cho phép bạn chọn nhiều file thay vì chỉ một cái
                        {
                            //Lấy đường dẫn của file cụ thể
                            //lấy tên của file cụ thể
                            fileName = Path.GetFileName(file);
                        }
                    }
                    str2 = myDialog.FileNames;
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                string fileName = ""; //khởi tạo ban đầu
                using (OpenFileDialog myDialog = new OpenFileDialog())
                {
                    myDialog.Multiselect = true;        //Cho phép chọn n tập tin
                    myDialog.InitialDirectory = "";     //Khởi tạo đường dẫn
                    myDialog.Filter = "text Files|*.txt";
                    myDialog.FilterIndex = 2;
                    myDialog.RestoreDirectory = true;

                    if (myDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in myDialog.FileNames)
                        {
                            fileName = Path.GetFileName(file);
                        }
                    }
                    str2 = myDialog.FileNames;
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                string fileName = ""; //khởi tạo ban đầu
                using (OpenFileDialog myDialog = new OpenFileDialog())
                {
                    myDialog.Multiselect = true;        //Cho phép chọn n tập tin
                    myDialog.InitialDirectory = "";     //Khởi tạo đường dẫn
                                                        //myDialog.Filter = "Text files (*.txt;*.pdf;*.docx)|*.txt;*.pdf;*.docx|All files (*.*)|*.*";
                    myDialog.Filter = " Docx Files|*.docx";
                    myDialog.FilterIndex = 2;
                    myDialog.RestoreDirectory = true;

                    if (myDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in myDialog.FileNames)
                        {
                            fileName = Path.GetFileName(file);
                        }
                    }
                    str2 = myDialog.FileNames;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (textBox3.Text == "")
            {
                MessageBox.Show("Hãy điền email cần gửi", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String email = textBox3.Text;
                msbus = new BUS.MessageBUS();
                if (msbus.Search(email) == true)
                {
                    label4.Text = email;
                    LoadMessageContent(email);
                }
                else
                    MessageBox.Show("Không tìm thấy email bạn cần tìm", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
