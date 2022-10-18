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
    public partial class Progress : UserControl
    {
        private BUS.CongViecBUS cvbus = new BUS.CongViecBUS();
        private Timer tm = new Timer();
        private ProgressBar pg, pg1;
        private Label name, name1, status, status1;
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        public Progress()
        {
            InitializeComponent();
        }
        private void ShowProgressTask(String search)
        {
            int top = 120;       //Xác định căn lề trên
            int left1 = 5;     // Xác định căn lề trái của name
            int left = 100;     // Xác định căn lề trái                                   
            int left2 = 420;     // Xác định căn lề trái của status
            for (int i=0; i<cvbus.Search1(search).Count; i++)
            {
                name = new Label();
                pg = new ProgressBar();
                status = new Label();
                name.Left = left1;
                name.Top = top;
                name.Height = 50;
                name.Width = 50;
                pg.Left = left;
                pg.Top = top;
                pg.Height = 50;
                pg.Width = 300;                
                status.Left = left2;
                status.Top = top;
                status.Height = 50;
                status.Width = 50;
                name.Text = cvbus.Search1(search).ElementAt(i).Tencv;
                if (cvbus.Search1(search).ElementAt(i).Trangthai == 1)
                {
                    pg.Value = 100;
                    status.Text = "Đã Xong";
                }
                else
                {
                    pg.Value = 0;
                    status.Text = "Chưa Xong";
                }                
                panel1.Controls.Add(name);
                panel1.Controls.Add(pg);
                panel1.Controls.Add(status);
                top += pg.Height + 2;
            }
        }
        private void ShowProgressProject(String search)
        {
            try
            {
                int progress = (int)(0.5f + ((100f * cvbus.CountFinish(search)) / cvbus.Count(search)));
                name1 = new Label();
                pg1 = new ProgressBar();
                status1 = new Label();
                int top = 500;       //Xác định căn lề trên
                int left1 = 5;     // Xác định căn lề trái của name1
                int left = 100;     // Xác định căn lề trái                                   
                int left2 = 680;     // Xác định căn lề trái của status1
                name1.Left = left1;
                name1.Top = top;
                name1.Height = 50;
                name1.Width = 100;
                name1.Text = "Tiến độ công việc của bạn";
                pg1.Left = left;
                pg1.Top = top;
                pg1.Height = 50;
                pg1.Width = 550;
                pg1.Value = progress;
                status1.Left = left2;
                status1.Top = top;
                status1.Text = Convert.ToString(progress + " " + "%");
                panel1.Controls.Add(name1);
                panel1.Controls.Add(pg1);
                panel1.Controls.Add(status1);
                top += pg1.Height + 2;
            }
            catch(Exception e)
            {
                MessageBox.Show("Môn học không tìm thấy","Lỗi");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            String search = textBox1.Text;
            if(search=="")
            {
                MessageBox.Show("Hãy điền Mã môn học vào","Lỗi");
            }
            else
            {
                ShowProgressTask(search);
                ShowProgressProject(search);
            }                     
        }

        

        private void Progress_Load(object sender, EventArgs e)
        {
            
        }
    }
}
