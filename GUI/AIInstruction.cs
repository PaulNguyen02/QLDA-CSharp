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
    public partial class AIInstruction : UserControl
    {
        private static int step = 0;
        private static List<Image> imglist = new List<Image>();
        private Image img;
        private System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(WindowsFormsApp1.Properties.Resources.SFXMouseClick);
        public AIInstruction()
        {           
            InitializeComponent();            
            LoadFirstPicture();            
        }
        private void LoadFirstPicture()
        {            
            for(int i=0;i<4;i++)
            {
                switch(i)
                {
                    case 0:
                    {
                        //img= Image.FromFile("E:\\C#\\QLSV\\Instruction1.jpg");
                        img = WindowsFormsApp1.Properties.Resources.Instruction1;
                        imglist.Add(img);
                        pb1.Image = img;
                        pb1.SizeMode = PictureBoxSizeMode.StretchImage;                       
                        //panel1.Controls.Add(pb1);
                        break;
                    }
                    case 1:
                    {
                        //img = Image.FromFile("E:\\C#\\QLSV\\Instruction2.jpg");
                        img = WindowsFormsApp1.Properties.Resources.Instruction2;
                        imglist.Add(img);
                        //pb1.Image = img;
                        //pb1.SizeMode = PictureBoxSizeMode.StretchImage;                      
                        break;
                    }
                    case 2:
                    {
                            //img = Image.FromFile("E:\\C#\\QLSV\\Instruction3.jpg");
                       img = WindowsFormsApp1.Properties.Resources.Instruction3;
                       imglist.Add(img);
                       //pb1.Image = img;
                       //pb1.SizeMode = PictureBoxSizeMode.StretchImage;                       
                       break;
                    }
                    case 3:
                    {
                            //img = Image.FromFile("E:\\C#\\QLSV\\Instruction4.jpg");
                       img = WindowsFormsApp1.Properties.Resources.Instruction4;
                       imglist.Add(img);
                       //pb1.Image = img;
                       //pb1.SizeMode = PictureBoxSizeMode.StretchImage;                       
                       break;
                    }
                }
            }                       
        }        
        private void bt1_Click(object sender, EventArgs e)
        {
            sfx.Play();
            step--;
            if (step < 0)
            {
                step = 3;
                pb1.Image = imglist.ElementAt(step);
            }
            else if(step >= 0 && step < 4)
            {
                pb1.Image = imglist.ElementAt(step);
            }
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            sfx.Play();
            step++;
            if(step>=0 && step<4)
            {
                pb1.Image = imglist.ElementAt(step);
            }
            else
            {
                step = 0;
                pb1.Image = imglist.ElementAt(step);
            }
        }

        private void bt3_Click(object sender, EventArgs e)
        {           
            sfx.Play();
            GUI.DecisionTree dt = new GUI.DecisionTree();
            panel1.Controls.Clear();
            panel1.Controls.Add(dt);
        }
    }
}
