using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DTO;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private String id, name, dob, pos, macv, email, pass, steam;
        private static string user, userpass;
        private DTO.Member mb;
        private DTO.PhanCong pc;
        private DTO.MonHoc mh;
        private DTO.CTMH ctmh;
        private DTO.CongViec cv;
        private DTO.DoAn da;
        private DTO.Subteam su;
        private BUS.MemberBUS mbus;
        private BUS.PhanCongBUS pcbus;
        private BUS.MonHocBUS mhbus;
        private BUS.CTMHBUS ctmhbus;
        private BUS.CongViecBUS cvbus;
        private BUS.DoAnBUS dabus;
        private BUS.STeamBUS stbus;
        private List<DTO.Member> ListofMem = new List<DTO.Member>();
        private List<DTO.PhanCong> ListPC = new List<DTO.PhanCong>();
        private List<DTO.MonHoc> ListMH = new List<DTO.MonHoc>();
        private List<DTO.CTMH> ListCTMH = new List<DTO.CTMH>();
        private List<DTO.CongViec> ListCV = new List<DTO.CongViec>();
        private List<DTO.DoAn> ListDA = new List<DTO.DoAn>();
        private List<DTO.Subteam> ListST = new List<DTO.Subteam>();
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        private Color color;
        private int status;
        public Form1()
        {
            InitializeComponent();
        }

        private void lb2_Click(object sender, EventArgs e)
        {
           
        }
        public bool IsNumber(string pValue)     //Kiểm tra có phải là số ko
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public int CountChar(string pValue)     //Đếm số lượng ký tự trong chuỗi
        {
            int count = 0;
            foreach (Char c in pValue)
            {
                count++;
            }
            return count;
        }
        public void SetAcc(String email, String pass)
        {
            user = email;
            userpass = pass;
        }        
        private void bt4_Click(object sender, EventArgs e)
        {
            sfx.Play();
            mbus = new BUS.MemberBUS();
            ListofMem = mbus.Query();
            for (int i = 0; i < ListofMem.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListofMem.ElementAt(i).Id);
                li.SubItems.Add(ListofMem.ElementAt(i).Name);
                li.SubItems.Add(ListofMem.ElementAt(i).DoB);
                li.SubItems.Add(ListofMem.ElementAt(i).Pos);
                li.SubItems.Add(ListofMem.ElementAt(i).Email);
                tb.Items.Add(li);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void lb11_Click(object sender, EventArgs e)
        {

        }

        private void bt2_Click(object sender, EventArgs e)
        {
            sfx.Play();
            String updateid = tf23.Text;
            if (updateid==""||CountChar(tf1.Text) > 10 || tf1.Text == "" || tf2.Text == "" || tfemail.Text == "" || tfpass.Text == "" || cbx.Text == "Vị trí" || IsNumber(tf1.Text) == false || tf23.Text==""|| IsNumber(tf23.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MSSV để thao tác và kiểm tra lại các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                email = tfemail.Text;
                pass = tfpass.Text;              
                mb = new DTO.Member() { Id = id, Name = name, DoB = dob, Pos = pos, Email=email, Pass=pass };
                ShowonTable(mb);
                mbus = new BUS.MemberBUS();
                mbus.Update(mb, updateid);
            }
            
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (tf23.Text == "" || IsNumber(tf23.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MSSV để xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String deleteid = tf23.Text;
                mbus = new BUS.MemberBUS();
                mbus.Delete(deleteid);
            }      
        }

        private void bt14_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf12.Text) > 10 || tf12.Text == "" || tf13.Text == "")
            {
                MessageBox.Show("Làm ơn điền đầy đủ thông tin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            {
                steam = textBox7.Text;
                id = tf12.Text;
                macv = tf13.Text;
                DTO.PhanCong pc = new DTO.PhanCong() { Idsteam = steam, Mssv = id, Macv = macv };
                ShowonTable1(pc);
                BUS.PhanCongBUS pcbus = new BUS.PhanCongBUS();
                pcbus.Add(pc);
            }       
        }

        private void bt17_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (tf23.Text == "" || IsNumber(tf23.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MSSV để xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String deleteid = tf23.Text;
                pcbus = new BUS.PhanCongBUS();
                pcbus.Delete(deleteid);
            }          
        }

        private void bt15_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf12.Text) > 10 || tf23.Text == "" || IsNumber(tf23.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MSSV để thao tác và kiểm tra lại các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                id = tf12.Text;
                macv = tf13.Text;
                String updateid = tf23.Text;
                pc = new DTO.PhanCong() { Idsteam = steam, Mssv = id, Macv = macv };
                ShowonTable1(pc);
                pcbus = new BUS.PhanCongBUS();
                pcbus.Update(pc, updateid);
            }          
        }

        private void bt16_Click(object sender, EventArgs e)
        {
            sfx.Play();
            pcbus = new BUS.PhanCongBUS();
            ListPC = pcbus.Query();
            for (int i = 0; i < ListPC.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListPC.ElementAt(i).Idsteam);
                li.SubItems.Add(ListPC.ElementAt(i).Mssv);
                li.SubItems.Add(ListPC.ElementAt(i).Macv);
                tb2.Items.Add(li);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bt6_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf4.Text) > 10 || tf4.Text == "" || tf5.Text == "" || tf6.Text == ""|| IsNumber(tf4.Text) == false)
            {
                MessageBox.Show("Làm ơn điền đầy đủ thông tin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String mamh = tf4.Text;
                String tenmh = tf5.Text;
                int stc = Convert.ToInt32(tf6.Text);
                mh = new DTO.MonHoc() { Mamh = mamh, Tenmh = tenmh, Stc = stc, Tiendo = 0 };
                ShowonTable2(mh);
                mhbus = new BUS.MonHocBUS();
                mhbus.Add(mh);
            }         
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf4.Text) > 10 || tf4.Text==""|| tf5.Text==""|| tf6.Text==""||tf24.Text == "" || IsNumber(tf24.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MAMH để thao tác và kiểm tra lại các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String mamh = tf4.Text;
                String tenmh = tf5.Text;
                int Stc = Convert.ToInt32(tf6.Text);
                String updateid = tf24.Text;
                mh = new DTO.MonHoc() { Mamh = mamh, Tenmh = tenmh, Stc = Stc };
                ShowonTable2(mh);
                mhbus = new BUS.MonHocBUS();
                mhbus.Update(mh, updateid);
            }            
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            sfx.Play();
            mhbus = new BUS.MonHocBUS();
            ListMH = mhbus.Query();
            for (int i = 0; i < ListMH.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListMH.ElementAt(i).Mamh);
                li.SubItems.Add(ListMH.ElementAt(i).Tenmh);
                li.SubItems.Add(Convert.ToString(ListMH.ElementAt(i).Stc));
                tb3.Items.Add(li);
            }
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (tf24.Text == "" || IsNumber(tf24.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MAMH để xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String deleteid = tf24.Text;
                mhbus = new BUS.MonHocBUS();
                mhbus.Delete(deleteid);
            }           
        }

        private void bt10_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf10.Text) > 10 || tf10.Text == "" || tf11.Text == "")
            {
                MessageBox.Show("Làm ơn điền đầy đủ thông tin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            {
                String mssv = tf10.Text;
                String mamh = tf11.Text;
                ctmh = new DTO.CTMH() { Idsv = mssv, Mamh = mamh };
                ShowonTable3(ctmh);
                ctmhbus = new BUS.CTMHBUS();
                ctmhbus.Add(ctmh);
            }
        }

        private void ShowonTable(DTO.Member mb)
        {
            ListViewItem li = new ListViewItem(mb.Id);
            li.SubItems.Add(mb.Name);
            li.SubItems.Add(mb.DoB);
            li.SubItems.Add(mb.Pos);
            li.SubItems.Add(mb.Email);
            tb.Items.Add(li);
        }

        private void bt11_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf10.Text) > 10 || tf10.Text == "" || tf11.Text == "" || tf24.Text == "" || IsNumber(tf24.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MSSV để thao tác và kiểm tra lại các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String mssv = tf10.Text;
                String mamh = tf11.Text;
                String updateid = tf24.Text;
                ctmh = new DTO.CTMH() { Idsv = mssv, Mamh = mamh };
                ShowonTable3(ctmh);
                ctmhbus = new BUS.CTMHBUS();
                ctmhbus.Update(ctmh, updateid);
            }            
        }

        private void bt12_Click(object sender, EventArgs e)
        {
            sfx.Play();
            ctmhbus = new BUS.CTMHBUS();
            ListCTMH = ctmhbus.Query();
            for (int i = 0; i < ListCTMH.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListCTMH.ElementAt(i).Idsv);
                li.SubItems.Add(ListCTMH.ElementAt(i).Mamh);
                tb4.Items.Add(li);
            }
        }

        private void bt13_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (tf24.Text == "" || IsNumber(tf24.Text) == false)
            {
                MessageBox.Show("Bạn cần điền MAMH để xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String deleteid = tf24.Text;
                ctmhbus = new BUS.CTMHBUS();
                ctmhbus.Delete(deleteid);
            }            
        }

        private void bt18_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf14.Text) > 10 || tf14.Text == "" || tf15.Text == "" || tf16.Text == "" || tf17.Text == "")
            {
                MessageBox.Show("Làm ơn điền đầy đủ thông tin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String macv = tf14.Text;
                String tencv = tf15.Text;
                String tgbd = dtpk1.Value.Year.ToString() + "-" + dtpk1.Value.Month.ToString() + "-" + dtpk1.Value.Day.ToString();
                String tgkt = dtpk2.Value.Year.ToString() + "-" + dtpk2.Value.Month.ToString() + "-" + dtpk2.Value.Day.ToString();
                String tgth = tf16.Text;
                String mada = tf17.Text;
                if(rb1.Checked==true)
                {
                    status = 0;
                }
                if(rb2.Checked == true)
                {
                    status = 1;
                }
                cv = new DTO.CongViec() { Macv = macv, Tencv = tencv, Tgbd = tgbd, Tgkt = tgkt, Tgth = tgth, Mada = mada, Trangthai=status };
                ShowonTable4(cv);
                cvbus = new BUS.CongViecBUS();
                cvbus.Add(cv);
            }           
        }

        private void ShowonTable1(DTO.PhanCong pc)
        {
            ListViewItem li = new ListViewItem(pc.Idsteam);
            li.SubItems.Add(pc.Mssv);
            li.SubItems.Add(pc.Macv);
            tb2.Items.Add(li);
        }

        private void bt19_Click(object sender, EventArgs e)
        {
            sfx.Play();
            cvbus = new BUS.CongViecBUS();
            ListCV = cvbus.Query();
            for (int i = 0; i < ListCV.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListCV.ElementAt(i).Macv);
                li.SubItems.Add(ListCV.ElementAt(i).Tencv);
                li.SubItems.Add(ListCV.ElementAt(i).Tgbd);
                li.SubItems.Add(ListCV.ElementAt(i).Tgkt);
                li.SubItems.Add(ListCV.ElementAt(i).Tgth);
                li.SubItems.Add(ListCV.ElementAt(i).Mada);
                tb5.Items.Add(li);
            }
        }

        private void bt20_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if(CountChar(tf14.Text) > 10||tf14.Text==""||tf15.Text==""|| tf16.Text=="" || tf17.Text=="" ||tf25.Text=="")
            {
                MessageBox.Show("Bạn cần điền MACV và đầy đủ thông tin các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String macv = tf14.Text;
                String tencv = tf15.Text;
                String tgbd = dtpk1.Value.Year.ToString() + "-" + dtpk1.Value.Month.ToString() + "-" + dtpk1.Value.Day.ToString();
                String tgkt = dtpk2.Value.Year.ToString() + "-" + dtpk2.Value.Month.ToString() + "-" + dtpk2.Value.Day.ToString();
                String tgth = tf16.Text;
                String mada = tf17.Text;
                if (rb1.Checked == true)
                {
                    status = 0;
                }
                if (rb2.Checked == true)
                {
                    status = 1;
                }
                String updateid = tf25.Text;
                cv = new DTO.CongViec() { Macv = macv, Tencv = tencv, Tgbd = tgbd, Tgkt = tgkt, Tgth = tgth, Mada = mada, Trangthai = status };
                ShowonTable4(cv);
                cvbus = new BUS.CongViecBUS();
                cvbus.Update(cv, updateid);
            }
        }

        private void bt21_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (tf25.Text == "")
            {
                MessageBox.Show("Điền MACV cần xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String deleteid = tf25.Text;
                cvbus = new BUS.CongViecBUS();
                cvbus.Delete(deleteid);
            }              
        }

        private void bt23_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf18.Text) > 10 || tf18.Text == "" || tf19.Text == "" || tf20.Text == "" || tf21.Text == "")
            {
                MessageBox.Show("Làm ơn điền đầy đủ thông tin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String mada = tf18.Text;
                String tenda = tf19.Text;
                String tgbd = dtpk3.Value.Year.ToString() + "-" + dtpk3.Value.Month.ToString() + "-" + dtpk3.Value.Day.ToString();
                String tgkt = dtpk4.Value.Year.ToString() + "-" + dtpk4.Value.Month.ToString() + "-" + dtpk4.Value.Day.ToString();
                String tgth = tf20.Text;
                String mamh = tf21.Text;
                da = new DTO.DoAn() { MADA = mada, TenDA = tenda, Tgbd = tgbd, Tgkt = tgkt, Tgth = tgth, Mamh = mamh, Tiendo = 0 };
                ShowonTable5(da);
                dabus = new BUS.DoAnBUS();
                dabus.Add(da);
            }           
        }

        private void ShowonTable2(DTO.MonHoc mh)
        {
            ListViewItem li = new ListViewItem(mh.Mamh);
            li.SubItems.Add(mh.Tenmh);
            li.SubItems.Add(Convert.ToString(mh.Stc));
            tb3.Items.Add(li);
        }

        private void bt24_Click(object sender, EventArgs e)
        {
            sfx.Play();
            dabus = new BUS.DoAnBUS();
            ListDA = dabus.Query();
            for (int i = 0; i < ListDA.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListDA.ElementAt(i).MADA);
                li.SubItems.Add(ListDA.ElementAt(i).TenDA);
                li.SubItems.Add(ListDA.ElementAt(i).Tgbd);
                li.SubItems.Add(ListDA.ElementAt(i).Tgkt);
                li.SubItems.Add(ListDA.ElementAt(i).Tgth);
                li.SubItems.Add(ListDA.ElementAt(i).Mamh);
                tb6.Items.Add(li);
            }
        }

        private void bt25_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(tf18.Text) > 10 || tf18.Text == "" || tf19.Text == "" || tf20.Text == "" || tf21.Text == "" || tf26.Text == "")
            {
                MessageBox.Show("Bạn cần điền MADA và đầy đủ thông tin các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String mada = tf18.Text;
                String tenda = tf19.Text;
                String tgbd = dtpk1.Value.Year.ToString() + "-" + dtpk1.Value.Month.ToString() + "-" + dtpk1.Value.Day.ToString();
                String tgkt = dtpk2.Value.Year.ToString() + "-" + dtpk2.Value.Month.ToString() + "-" + dtpk2.Value.Day.ToString();
                String tgth = tf20.Text;
                String mamh = tf21.Text;
                String updateid = tf26.Text;
                da = new DTO.DoAn() { MADA = mada, TenDA = tenda, Tgbd = tgbd, Tgkt = tgkt, Tgth = tgth, Mamh = mamh };
                ShowonTable5(da);
                dabus = new BUS.DoAnBUS();
                dabus.Update(da, updateid);
            }           
        }

        private void bt26_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (tf26.Text == "")
            {
                MessageBox.Show("Điền MADA cần xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            else
            {
                String deleteid = tf25.Text;
                dabus = new BUS.DoAnBUS();
                dabus.Delete(deleteid);
            }
        }

        private void thôngTinNhàPhátTriểnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutSoftware asw = new AboutSoftware();
            asw.Visible = true;
        }

        private void cậpNhậtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Instruction ins = new Instruction();
            ins.Visible = true;
        }

        private void tb4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbx1.SelectedIndex==0)
            {
                mhbus = new BUS.MonHocBUS();
                ListMH = mhbus.SortASC();
                for (int i = 0; i < ListMH.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListMH.ElementAt(i).Mamh);
                    li.SubItems.Add(ListMH.ElementAt(i).Tenmh);
                    li.SubItems.Add(Convert.ToString(ListMH.ElementAt(i).Stc));
                    tb3.Items.Add(li);
                }
            }
            if (cbx1.SelectedIndex == 1)
            {
                mhbus = new BUS.MonHocBUS();
                ListMH = mhbus.SortDESC();
                for (int i = 0; i < ListMH.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListMH.ElementAt(i).Mamh);
                    li.SubItems.Add(ListMH.ElementAt(i).Tenmh);
                    li.SubItems.Add(Convert.ToString(ListMH.ElementAt(i).Stc));
                    tb3.Items.Add(li);
                }
            }
        }

        private void cbx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx2.SelectedIndex == 0)
            {
                cvbus = new BUS.CongViecBUS();
                ListCV = cvbus.ASCBD();
                for (int i = 0; i < ListMH.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListCV.ElementAt(i).Macv);
                    li.SubItems.Add(ListCV.ElementAt(i).Tencv);
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgbd));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgkt));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgth));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Mada));
                    tb3.Items.Add(li);
                }
            }
            if (cbx2.SelectedIndex == 1)
            {
                cvbus = new BUS.CongViecBUS();
                ListCV = cvbus.DESCBD();
                for (int i = 0; i < ListMH.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListMH.ElementAt(i).Mamh);
                    li.SubItems.Add(ListCV.ElementAt(i).Tencv);
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgbd));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgkt));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgth));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Mada));
                    tb3.Items.Add(li);
                }
            }
            if (cbx2.SelectedIndex == 2)
            {
                cvbus = new BUS.CongViecBUS();
                ListCV = cvbus.ASCKT();
                for (int i = 0; i < ListMH.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListMH.ElementAt(i).Mamh);
                    li.SubItems.Add(ListCV.ElementAt(i).Tencv);
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgbd));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgkt));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgth));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Mada));
                    tb3.Items.Add(li);
                }
            }
            if (cbx2.SelectedIndex == 3)
            {
                cvbus = new BUS.CongViecBUS();
                ListCV = cvbus.DESCKT();
                for (int i = 0; i < ListMH.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListMH.ElementAt(i).Mamh);
                    li.SubItems.Add(ListCV.ElementAt(i).Tencv);
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgbd));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgkt));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Tgth));
                    li.SubItems.Add(Convert.ToString(ListCV.ElementAt(i).Mada));
                    tb3.Items.Add(li);
                }
            }
        }

        private void cbx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbx3.SelectedIndex==0)
            {
                dabus = new BUS.DoAnBUS();
                ListDA = dabus.ASCBD();
                for (int i = 0; i < ListDA.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListDA.ElementAt(i).MADA);
                    li.SubItems.Add(ListDA.ElementAt(i).TenDA);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgbd);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgkt);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgth);
                    li.SubItems.Add(ListDA.ElementAt(i).Mamh);
                    tb6.Items.Add(li);
                }
            }
            if (cbx3.SelectedIndex == 1)
            {
                dabus = new BUS.DoAnBUS();
                ListDA = dabus.DESCBD();
                for (int i = 0; i < ListDA.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListDA.ElementAt(i).MADA);
                    li.SubItems.Add(ListDA.ElementAt(i).TenDA);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgbd);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgkt);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgth);
                    li.SubItems.Add(ListDA.ElementAt(i).Mamh);
                    tb6.Items.Add(li);
                }
            }
            if (cbx3.SelectedIndex == 2)
            {
                dabus = new BUS.DoAnBUS();
                ListDA = dabus.ASCKT();
                for (int i = 0; i < ListDA.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListDA.ElementAt(i).MADA);
                    li.SubItems.Add(ListDA.ElementAt(i).TenDA);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgbd);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgkt);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgth);
                    li.SubItems.Add(ListDA.ElementAt(i).Mamh);
                    tb6.Items.Add(li);
                }
            }
            if (cbx3.SelectedIndex == 3)
            {
                dabus = new BUS.DoAnBUS();
                ListDA = dabus.DESCKT();
                for (int i = 0; i < ListDA.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListDA.ElementAt(i).MADA);
                    li.SubItems.Add(ListDA.ElementAt(i).TenDA);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgbd);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgkt);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgth);
                    li.SubItems.Add(ListDA.ElementAt(i).Mamh);
                    tb6.Items.Add(li);
                }
            }
        }

        private void bt22_Click(object sender, EventArgs e)
        {
            sfx.Play();
            String searchid = textBox5.Text;
            if (searchid != "")
            {
                cvbus = new BUS.CongViecBUS();
                ListCV = cvbus.Search(searchid);
                for (int i = 0; i < ListCV.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListCV.ElementAt(i).Macv);
                    li.SubItems.Add(ListCV.ElementAt(i).Tencv);
                    li.SubItems.Add(ListCV.ElementAt(i).Tgbd);
                    li.SubItems.Add(ListCV.ElementAt(i).Tgkt);
                    li.SubItems.Add(ListCV.ElementAt(i).Tgth);
                    li.SubItems.Add(ListCV.ElementAt(i).Mada);
                    tb5.Items.Add(li);
                }
            }
            else
                MessageBox.Show("Thông tin của bạn chưa được tìm thấy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bt27_Click(object sender, EventArgs e)
        {
            sfx.Play();
            dabus = new BUS.DoAnBUS();
            String searchid = tf22.Text;
            if (searchid != "")
            {
                ListDA = dabus.Search(searchid);
                for (int i = 0; i < ListDA.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListDA.ElementAt(i).MADA);
                    li.SubItems.Add(ListDA.ElementAt(i).TenDA);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgbd);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgkt);
                    li.SubItems.Add(ListDA.ElementAt(i).Tgth);
                    li.SubItems.Add(ListDA.ElementAt(i).Mamh);
                    tb5.Items.Add(li);
                }
            }
            else                
                MessageBox.Show("Thông tin của bạn chưa được tìm thấy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void xóaBảngNhânViênToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NotificationGUI.DeleteNV nv = new NotificationGUI.DeleteNV();
            nv.Visible = true;
        }

        private void xóaBảngMônHọcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NotificationGUI.DeleteMH mh = new NotificationGUI.DeleteMH();
            mh.Visible = true;
        }

        private void xóaBảngCôngViệcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NotificationGUI.DeleteCV cv = new NotificationGUI.DeleteCV();
            cv.Visible = true;
        }

        private void xóaBảngĐồÁnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificationGUI.DeleteDoAn da = new NotificationGUI.DeleteDoAn();
            da.Visible = true;
        }

        private void xóaBảngPhânCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificationGUI.DeletePhanCong pc = new NotificationGUI.DeletePhanCong();
            pc.Visible = true;
        }

        private void xóaBảngChiTiếtMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificationGUI.DeleteCTMH ctmh = new NotificationGUI.DeleteCTMH();
            ctmh.Visible = true;
        }

        private void đenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            mns.BackColor = Color.Black;
            panel11.BackColor = Color.Black;
        }

        private void trắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gold;
            mns.BackColor = Color.Gold;
            panel11.BackColor = Color.Gold;
        }

        private void xanhNướcBiểnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue;
            mns.BackColor = Color.LightBlue;
            panel11.BackColor = Color.LightBlue;
        }

        private void xanhLáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
            mns.BackColor = Color.Green;
            panel11.BackColor = Color.Green;
        }

        private void xanhĐạiDươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkBlue;
            mns.BackColor = Color.DarkBlue;
            panel11.BackColor = Color.DarkBlue;
        }

        private void camToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Orange;
            mns.BackColor = Color.Orange;
            panel11.BackColor = Color.Orange;
        }

        private void đỏToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
            mns.BackColor = Color.Red;
            panel11.BackColor = Color.Red;
        }

        private void hồngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Pink;
            mns.BackColor = Color.Pink;
            panel11.BackColor = Color.Pink;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            tf1.Clear();
            tf2.Clear();
            cbx.ResetText();
            tfpass.Clear();
            tb.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sfx.Play();
            tf12.Clear();
            tf13.Clear();
            tb2.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sfx.Play();
            tf4.Clear();
            tf5.Clear();
            tf6.Clear();
            tb3.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sfx.Play();
            tf10.Clear();
            tf11.Clear();
            tb4.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sfx.Play();
            tf14.Clear();
            tf15.Clear();
            tf16.Clear();
            tf17.Clear();
            tf25.Clear();
            tb5.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)      //Hiện thị lên 2 bảng
        {
            sfx.Play();
            String searchid = searchbox1.Text;
            if (searchid != "")
            {
                mhbus = new BUS.MonHocBUS();
                ctmhbus = new BUS.CTMHBUS();                
                foreach (DTO.MonHoc mh in mhbus.Search(searchid))
                {
                    ListViewItem li = new ListViewItem(mh.Mamh);
                    li.SubItems.Add(mh.Tenmh);
                    li.SubItems.Add(Convert.ToString(mh.Stc));
                    tb3.Items.Add(li);
                }
                foreach (DTO.CTMH ctmh in ctmhbus.Search(searchid))
                {
                    ListViewItem li = new ListViewItem(ctmh.Idsv);
                    li.SubItems.Add(ctmh.Mamh);
                    tb4.Items.Add(li);
                }
            } 
            else
            {
                MessageBox.Show("Thông tin của bạn chưa được tìm thấy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sfx.Play();
            String searchid = searchbox.Text;
            if(searchid!="")
            {
                mbus = new BUS.MemberBUS();
                pcbus = new BUS.PhanCongBUS();                
                foreach (DTO.Member mb in mbus.Search(searchid))
                {
                    ListViewItem li = new ListViewItem(mb.Id);
                    li.SubItems.Add(mb.Name);
                    li.SubItems.Add(mb.DoB);
                    li.SubItems.Add(mb.Pos);
                    tb.Items.Add(li);
                }
                foreach (DTO.PhanCong ctmh in pcbus.Search(searchid))
                {
                    ListViewItem li = new ListViewItem(ctmh.Mssv);
                    li.SubItems.Add(ctmh.Macv);
                    tb2.Items.Add(li);
                }
            }
            else
                MessageBox.Show("Thông tin của bạn chưa được tìm thấy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void lb27_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
           
        }
        
        private void tabPage5_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();    //Tắt toàn bộ chương trình
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            this.BackColor = Properties.Settings.Default.Color;
            mns.BackColor = Properties.Settings.Default.Color;
            panel11.BackColor = Properties.Settings.Default.Color;
            StartTimer();
        }

        private void Form1_Resize(object sender, EventArgs e)   //Chỉnh kích thước form
        {
        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            sfx.Play();
        }

        private void mns_Click(object sender, EventArgs e)
        {
            sfx.Play();
        }

        private void fileToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void mns_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(textBox1.Text)>10 ||textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" )
            {
                MessageBox.Show("Làm ơn điền đầy đủ thông tin hoặc xem lại số lượng ký tự trong IDSTeam", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String masubteam = textBox1.Text;
                String tensubteam = textBox2.Text;               
                int soluong = int.Parse(textBox3.Text);
                su = new Subteam() { Idsteam = masubteam, nameofsteam = tensubteam, numofmem = soluong };
                ShowonTable6(su);
                stbus = new BUS.STeamBUS();
                stbus.Add(su);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sfx.Play();
            tf18.Clear();
            tf19.Clear();
            tf20.Clear();
            tf21.Clear();
            tb6.Items.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            sfx.Play();
            stbus = new BUS.STeamBUS();
            ListST = stbus.Query();
            for (int i = 0; i < ListDA.Count; i++)
            {
                ListViewItem li = new ListViewItem(ListST.ElementAt(i).Idsteam);
                li.SubItems.Add(ListST.ElementAt(i).nameofsteam);
                li.SubItems.Add(Convert.ToString(ListST.ElementAt(i).numofmem));          
                tb7.Items.Add(li);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (CountChar(textBox1.Text) > 10 || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text=="")
            {
                MessageBox.Show("Bạn cần điền IDSubTeam và đầy đủ thông tin các trường", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String updateid = textBox4.Text;
                String idsteam = textBox1.Text;
                String namesteam = textBox2.Text;
                int number = int.Parse(textBox3.Text);
                su = new DTO.Subteam { Idsteam=idsteam, nameofsteam=namesteam, numofmem=number};
                ShowonTable6(su);
                stbus = new BUS.STeamBUS();
                stbus.Update(su, updateid);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if (textBox4.Text == "")
            {
                MessageBox.Show("Hãy điền IDSubteam cần xóa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String deleteid = tf25.Text;
                stbus = new BUS.STeamBUS();
                stbus.Delete(deleteid);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sfx.Play();
            stbus = new BUS.STeamBUS();
            String searchid = textBox6.Text;
            if (searchid != "")
            {
                ListST = stbus.Search(searchid);
                for (int i = 0; i < ListDA.Count; i++)
                {
                    ListViewItem li = new ListViewItem(ListST.ElementAt(i).Idsteam);
                    li.SubItems.Add(ListST.ElementAt(i).nameofsteam);
                    li.SubItems.Add(Convert.ToString(ListST.ElementAt(i).numofmem));
                    tb7.Items.Add(li);
                }
            }
            else
                MessageBox.Show("Thông tin của bạn chưa được tìm thấy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            tb7.Items.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            sfx.Play();
            GUI.AIInstruction aii = new GUI.AIInstruction();
            tabPage7.Controls.Clear();
            tabPage7.Controls.Add(aii);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            sfx.Play();
            GUI.Traker tkgui = new GUI.Traker();
            tabPage9.Controls.Clear();
            tabPage9.Controls.Add(tkgui);
        }        

        private void button26_Click(object sender, EventArgs e)
        {
            sfx.Play();
            GUI.Typeping tpgui = new GUI.Typeping();
            tabPage10.Controls.Clear();
            tabPage10.Controls.Add(tpgui);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws= (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MSSV";
                ws.Cells[1, 2] = "Họ Tên";
                ws.Cells[1, 3] = "Ngày Sinh";
                ws.Cells[1, 4] = "Vị trí";
                ws.Cells[1, 5] = "Email";
                foreach(ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;
                    ws.Cells[i, 3] = item.SubItems[2].Text;
                    ws.Cells[i, 4] = item.SubItems[3].Text;
                    ws.Cells[i, 5] = item.SubItems[4].Text;
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !","Thông báo");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MAMH";
                ws.Cells[1, 2] = "Tên môn học";
                ws.Cells[1, 3] = "Số tín chỉ";               
                foreach (ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;
                    ws.Cells[i, 3] = item.SubItems[2].Text;                    
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !", "Thông báo");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MSSV";
                ws.Cells[1, 2] = "MACV";                
                foreach (ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;                    
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !", "Thông báo");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MSSV";
                ws.Cells[1, 2] = "MAMH";                
                foreach (ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;                    
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !", "Thông báo");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MACV";
                ws.Cells[1, 2] = "Tên công việc";
                ws.Cells[1, 3] = "Thời gian bắt đầu";
                ws.Cells[1, 4] = "Thời gian kết thúc";
                ws.Cells[1, 5] = "Thời gian thực hiện";
                ws.Cells[1, 6] = "Mã đồ án";
                foreach (ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;
                    ws.Cells[i, 3] = item.SubItems[2].Text;
                    ws.Cells[i, 4] = item.SubItems[3].Text;
                    ws.Cells[i, 5] = item.SubItems[4].Text;
                    ws.Cells[i, 6] = item.SubItems[5].Text;
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !", "Thông báo");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MACV";
                ws.Cells[1, 2] = "Tên công việc";
                ws.Cells[1, 3] = "Thời gian bắt đầu";
                ws.Cells[1, 4] = "Thời gian kết thúc";
                ws.Cells[1, 5] = "Thời gian thực hiện";
                ws.Cells[1, 6] = "Mã đồ án";
                foreach (ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;
                    ws.Cells[i, 3] = item.SubItems[2].Text;
                    ws.Cells[i, 4] = item.SubItems[3].Text;
                    ws.Cells[i, 5] = item.SubItems[4].Text;
                    ws.Cells[i, 6] = item.SubItems[5].Text;
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !", "Thông báo");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int i = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As XLS";
            sfd.Filter = "Excel Workbook|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)app.ActiveSheet;
                app.Visible = true;
                ws.Cells[1, 1] = "MASUBTEAM";
                ws.Cells[1, 2] = "Tên sub team";
                ws.Cells[1, 3] = "Số lượng";               
                foreach (ListViewItem item in tb.Items)
                {
                    ws.Cells[i, 1] = item.SubItems[0].Text;
                    ws.Cells[i, 2] = item.SubItems[1].Text;
                    ws.Cells[i, 3] = item.SubItems[2].Text;                    
                    i++;
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                app.Quit();
                MessageBox.Show("Bạn đã xuất file xls thành công !", "Thông báo");
            }
        }
        private void StartTimer()
        {            
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label20.Text = DateTime.Now.ToString();
        }

        private void đổiChủĐềToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK) //Nếu nhấp vào nút OK trên hộp thoại
            {
                color = new Color();
                color = dlg.Color; //Trả lại tên của màu đã lựa chọn
                this.BackColor = color;
                mns.BackColor = color;
                panel11.BackColor = color;
                Properties.Settings.Default.Color = color;
                Properties.Settings.Default.Save();
            }           
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            GUI.Progress pg = new GUI.Progress();
            tabPage6.Controls.Clear();
            tabPage6.Controls.Add(pg);
        }

        private void xóaDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Signin sg = new Signin();
            sg.Visible = true;
            this.Hide();
        }

        private void xóaBảngSubTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificationGUI.DeleteSubteam subteam = new NotificationGUI.DeleteSubteam();
            subteam.Visible = true;
        }

        private void đặtLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = new Color();
            color = Color.FromArgb(43, 86, 154);
            this.BackColor = color;
            mns.BackColor = color;
            panel11.BackColor = color;
            Properties.Settings.Default.Color = color;
            Properties.Settings.Default.State = false;
            Properties.Settings.Default.Save();
        }

        private void tb3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            sfx.Play();
            GUI.Message mess = new GUI.Message();
            mess.SetAcc(user,userpass);
            tabPage8.Controls.Clear();
            tabPage8.Controls.Add(mess);
        }

        private void ShowonTable3(DTO.CTMH ctmh)
        {
            ListViewItem li = new ListViewItem(ctmh.Idsv);
            li.SubItems.Add(ctmh.Mamh);
            tb4.Items.Add(li);
        }
        private void ShowonTable4(DTO.CongViec cv)
        {
            ListViewItem li = new ListViewItem(cv.Macv);
            li.SubItems.Add(cv.Tencv);
            li.SubItems.Add(cv.Tgbd);
            li.SubItems.Add(cv.Tgkt);
            li.SubItems.Add(cv.Tgth);
            li.SubItems.Add(cv.Mada);
            tb5.Items.Add(li);
        }
        private void ShowonTable5(DTO.DoAn da)
        {
            ListViewItem li = new ListViewItem(da.MADA);
            li.SubItems.Add(da.TenDA);
            li.SubItems.Add(da.Tgbd);
            li.SubItems.Add(da.Tgkt);
            li.SubItems.Add(da.Tgth);
            li.SubItems.Add(da.Mamh);
            tb6.Items.Add(li);
        }
        private void ShowonTable6(DTO.Subteam su)
        {
            ListViewItem li = new ListViewItem(su.Idsteam);
            li.SubItems.Add(su.nameofsteam);
            li.SubItems.Add(Convert.ToString(su.numofmem));       
            tb7.Items.Add(li);
        }
        private void bt1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if(CountChar(tf1.Text) >10||tf1.Text=="" || tf2.Text=="" || tfemail.Text=="" ||tfpass.Text=="" || cbx.Text=="Vị trí"|| IsNumber(tf1.Text) == false)
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
                email = tfemail.Text;
                pass = tfpass.Text;
                mb = new DTO.Member() { Id = id, Name = name, DoB = dob, Pos = pos, Email=email ,Pass = pass };
                ShowonTable(mb);
                mbus = new BUS.MemberBUS();
                mbus.Add(mb);
            }
        }
    }
}
