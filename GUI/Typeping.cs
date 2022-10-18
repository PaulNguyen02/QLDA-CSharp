using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using iTextSharp.text.pdf;
using System.IO;
namespace WindowsFormsApp1.GUI
{
    public partial class Typeping : UserControl
    {
        private SaveFileDialog sfd = new SaveFileDialog();
        private SaveFileDialog SaveFileDialog1=new SaveFileDialog();
        private MemoryStream userInput = new MemoryStream();
        private string font="Segoe UI";
        private int size=12;
        private static int count=0, count1=0, count2=0, count3=0;
        private FontStyle font_style = FontStyle.Regular;
        private Color color;
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        public Typeping()
        {
            InitializeComponent();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            sfx.Play();
            if(Properties.Settings.Default.URL=="")
            {
                MessageBox.Show("Bạn hãy thiết lập đường dẫn","Thông báo");
            }
            else
            {                
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save As DOCX";
                sfd.Filter = "Docx Files|*.docx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                    app.Visible = true;
                    app.WindowState = WdWindowState.wdWindowStateNormal;
                    Microsoft.Office.Interop.Word.Document doc = app.Documents.Add();
                    Microsoft.Office.Interop.Word.Paragraph paragraph = doc.Paragraphs.Add();                                     
                    Clipboard.SetText(richTextBox1.Rtf, TextDataFormat.Rtf);
                    paragraph.Range.Paste();
                    doc.SaveAs2(sfd.FileName);
                }
            }              
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            sfx.Play();
            sfd.Title = "Save As PDF";
            sfd.Filter = "(*.pdf)|*.pdf";
            sfd.InitialDirectory = Properties.Settings.Default.URL;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                doc.Open();
                doc.Add(new iTextSharp.text.Paragraph(richTextBox1.Text));                
                doc.Close();
            }
        }
        private void SetFont()
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0: { font = "Microsoft Sans Serif"; break; }
                case 1: { font = "Segoe UI"; break; }
                case 2: { font = "Time New Roman"; break; }
                case 3: { font = "Arial"; break; }
                case 4: { font = "Consolas"; break; }
            }
        }
        private void SetSize()
        {
            switch (toolStripComboBox2.SelectedIndex)
            {
                case 0: { size = 4; break; }
                case 1: { size = 6; break; }
                case 2: { size = 8; break; }
                case 3: { size = 10; break; }
                case 4: { size = 12; break; }
                case 5: { size = 14; break; }
                case 6: { size = 16; break; }
                case 7: { size = 18; break; }
                case 8: { size = 22; break; }
                case 9: { size = 24; break; }
                case 10: { size = 26; break; }
                case 11: { size = 28; break; }
                case 12: { size = 30; break; }
                case 13: { size = 34; break; }
                case 14: { size = 36; break; }
            }
        }
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            count++;
            if(count%2==0)
            {
                font_style = FontStyle.Bold;
            }
            else
            {
                font_style = FontStyle.Regular;
            }
            richTextBox1.SelectionFont = new System.Drawing.Font(font, size, font_style);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            sfx.Play();
            count1++;
            if (count1 % 2 == 0)
            {
                font_style = FontStyle.Underline;
            }
            else
            {
                font_style = FontStyle.Regular;
            }
            richTextBox1.SelectionFont = new System.Drawing.Font(font, size, font_style);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            sfx.Play();
            count2++;
            if (count2 % 2 == 0)
            {
                font_style = FontStyle.Italic;
            }
            else
            {
                font_style = FontStyle.Regular;
            }
            richTextBox1.SelectionFont = new System.Drawing.Font(font, size, font_style);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            sfx.Play();
            count3++;
            if (count3 % 2 == 0)
            {
                font_style = FontStyle.Strikeout;
            }
            else
            {
                font_style = FontStyle.Regular;
            }
            richTextBox1.SelectionFont = new System.Drawing.Font(font, size, font_style);
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            label2.Text = richTextBox1.SelectionLength.ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = richTextBox1.Text.Split(' ').Count().ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            sfx.Play();
            SetSize();
            SetFont();
            richTextBox1.SelectionFont = new System.Drawing.Font(font, size, font_style);
        }

        

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            sfx.Play();
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK) //Nếu nhấp vào nút OK trên hộp thoại
            {
                color = new Color();
                color = dlg.Color; //Trả lại tên của màu đã lựa chọn
            }
            richTextBox1.SelectionColor = color;                          
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            sfx.Play();
            ColorDialog dlg = new ColorDialog();            
            if (dlg.ShowDialog() == DialogResult.OK) //Nếu nhấp vào nút OK trên hộp thoại
            {
                color = new Color();
                color = dlg.Color; //Trả lại tên của màu đã lựa chọn
            }
            richTextBox1.SelectionBackColor = color;
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripComboBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_MouseEnter(object sender, EventArgs e)
        {
           
        }
    }
}
