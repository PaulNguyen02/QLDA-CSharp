using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1.GUI
{    
    public partial class Progress : UserControl
    {
        private BUS.CongViecBUS cvbus = new BUS.CongViecBUS();
        private Timer tm = new Timer();
        private ProgressBar pg, pg1;
        private Chart chart2;
        private Label name, name1, status, status1;
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        public Progress()
        {
            InitializeComponent();
        }
        private void ShowProgressTask(String search)
        {
            int top = 50;       //Xác định căn lề trên
            int left1 = 5;     // Xác định căn lề trái của name
            int left = 100;     // Xác định căn lề trái                                   
            int left2 = 310;     // Xác định căn lề trái của status
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
                pg.Width = 200;                
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
                panel4.Controls.Add(name);
                panel4.Controls.Add(pg);
                panel4.Controls.Add(status);
                top += pg.Height + 15;
            }
        }
        private void ShowDoughnutChart(String search)
        {
            Series sr = new Series();
            sr.Name = "Task";
            sr.ChartType = SeriesChartType.Doughnut;
            Title tc = new Title("Tiến độ công việc của môn"+" "+cvbus.SearchSubject(search));
            tc.Font= new Font("SegoeUI", 12, FontStyle.Regular);
            chart1.Titles.Add(tc); 
            chart1.Series.Add(sr);
            int num = (int)(0.5f + ((100f * 1) / cvbus.Count(search)));
            for (int i = 0; i < cvbus.Search1(search).Count; i++)
            {                
                chart1.Series["Task"].Points.AddXY(cvbus.Search1(search).ElementAt(i).Tencv, num);
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void ShowProgressProject(String search)
        {
            try
            {
                int progress = (int)(0.5f + ((100f * cvbus.CountFinish(search)) / cvbus.Count(search)));
                name1 = new Label();
                pg1 = new ProgressBar();
                status1 = new Label();
                int top = 50;       //Xác định căn lề trên
                int left1 = 5;     // Xác định căn lề trái của name1
                int left = 100;     // Xác định căn lề trái                                   
                int left2 = 360;     // Xác định căn lề trái của status1
                name1.Left = left1;
                name1.Top = top;
                name1.Height = 50;
                name1.Width = 100;
                name1.Text = "Tiến độ công việc của bạn";
                pg1.Left = left;
                pg1.Top = top;
                pg1.Height = 50;
                pg1.Width = 250;
                pg1.Value = progress;
                status1.Left = left2;
                status1.Top = top;
                status1.Text = Convert.ToString(progress + " " + "%");
                panel5.Controls.Add(name1);
                panel5.Controls.Add(pg1);
                panel5.Controls.Add(status1);
                top += pg1.Height + 15;
            }
            catch(Exception e)
            {
                MessageBox.Show("Môn học không tìm thấy","Lỗi");
            }
        }
        private void ShowPieChart(String search)
        {
            Series sr = new Series();
            sr.Name = "Progress";
            sr.ChartType = SeriesChartType.Pie;
            Title tc = new Title(cvbus.SearchSubject(search)+" đã hoàn thành");
            tc.Font = new Font("SegoeUI", 12, FontStyle.Regular);
            chart3.Titles.Add(tc);
            chart3.Series.Add(sr);
            int num = (int)(0.5f + ((100f * cvbus.CountFinish(search)) / cvbus.Count(search)));
            chart3.Series["Progress"].Points.AddXY("Đã xong" + " " + Convert.ToString(num) + "%", num);
            chart3.Series["Progress"].Points.AddXY("Chưa xong"+ " "+ Convert.ToString(100-num) + "%", 100-num);
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
                panel4.Controls.Clear();
                panel5.Controls.Clear();
                chart1.Series.Clear();
                chart3.Series.Clear();
                chart1.Titles.Clear();
                chart3.Titles.Clear();
                ShowProgressTask(search);
                ShowProgressProject(search);
                ShowDoughnutChart(search);
                ShowPieChart(search);
            }                     
        }        
        private void Progress_Load(object sender, EventArgs e)
        {
            
        }
    }
}
