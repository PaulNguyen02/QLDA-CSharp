using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace WindowsFormsApp1.GUI
{
    public struct Node3
    {
        public string name;
        public string value;
        public List<Node3> arrNodes;
        public bool nodeRoot;
    };
    public partial class DecisionTree : UserControl
    {
        public static List<List<string>> dtSource;
        List<List<string>> dataSource;              //Mảng các thuộc tính và giá trị thực
        List<string> arrClause;                     //Chứa các thuộc tính(Attributes)
        List<string> arrValue;                      //Chứa các thuộc tính mục tiêu
        Dictionary<string, double> valueAttribute;  // Information Gain của từng giá trị thuộc tính(Attribute)
        Dictionary<string, bool> boolValueAttribute; // trả về true nếu giá trị của thộc tính(Attribute) chỉ cho ra một giá trị thuộc tính kết quả và ngược lại.
        private Graphics grs;
        Node3 decisionTree;
        public DecisionTree()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewClauses.Columns.Count < 1)
            {
                MessageBox.Show("Không có dữ liệu mẫu để chạy thuật toán", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
            dtSource = new List<List<string>>();
            for (int i = 0; i < dataGridViewClauses.Columns.Count; i++)
            {
                dtSource.Add(new List<string>());
                dtSource[i].Add(dataGridViewClauses.Columns[i].Name);
                for (int j = 0; j < dataGridViewClauses.Rows.Count - 1; j++)
                {
                    dtSource[i].Add(dataGridViewClauses.Rows[j].Cells[i].Value.ToString());
                }
            }
            ShowRes();
            PaintDecisionTree();
        }
        private void ShowRes()
        {
            try
            {
                decisionTree = new Node3();
                grs = pnlDecisionTree.CreateGraphics();
                arrClause = new List<string>();
                dataSource = new List<List<string>>();
                dataSource = dtSource;
                arrValue = new List<string>();
                int numRow = dataSource.Count();
                valueAttribute = new Dictionary<string, double>();
                boolValueAttribute = new Dictionary<string, bool>();
                foreach (var item in dataSource)
                {
                    arrClause.Add(item[0]);
                }
                for (int i = 1; i < dataSource[numRow - 1].Count(); i++)
                {
                    if (!arrValue.Contains(dataSource[numRow - 1][i]))
                    {
                        arrValue.Add(dataSource[numRow - 1][i]);
                    }
                }
                DecisionTrees("Start", ref decisionTree, dataSource, ref boolValueAttribute);               
                ShowRules(decisionTree, true, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tính với định dạng nguồn", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Trả về I (độ lợi thông tin của nút chủ (nút từ đó chọn nút kế tiếp))
        private double Result_I_S(List<List<string>> data)
        {
            int s1 = 0, s2 = 0;
            double infoGain = 0f;
            int numRow = data.Count();

            for (int i = 1; i < data[numRow - 1].Count(); i++)
            {
                if (arrValue[0] == data[numRow - 1][i])
                    s1++;
                else
                    s2++;
            }

            infoGain = -(1.0 * s1 / (s1 + s2)) * Math.Log(1.0 * s1 / (s1 + s2), 2f) - (1.0 * s2 / (s1 + s2)) * Math.Log(1.0 * s2 / (s1 + s2), 2f);
            return infoGain;
        }

        //Trả về I (độ lợi thông tin của từng giá trị trong mỗi thuộc tính(Attribute))
        private double Result_I(int s1, int s2)
        {
            double infoGain = 0f;
            infoGain = -(1.0 * s1 / (s1 + s2)) * Math.Log(1.0 * s1 / (s1 + s2), 2f) - (1.0 * s2 / (s1 + s2)) * Math.Log(1.0 * s2 / (s1 + s2), 2f);
            return infoGain;
        }

        //Trả về Entropy của giá trị (name) trong thuộc tính(Attribute) vị trí column
        private double Result_E(List<List<string>> data, ref Dictionary<string, bool> boolValueAttribute, int column, string name)
        {
            double entropy = 0f;
            int s1 = 0, s2 = 0;
            int numRow = data.Count();
            int numColumn = data[numRow - 1].Count();
            for (int i = 1; i < numColumn; i++)
            {
                if (data[column][i] == name)
                {
                    if (data[numRow - 1][i] == arrValue[0])
                        s1++;
                    else
                        s2++;
                }
            }

            if (s1 == 0 || s2 == 0)
            {
                if (boolValueAttribute.ContainsKey(data[column][0] + name))
                {
                    boolValueAttribute[data[column][0] + name] = true;
                }
                else
                    boolValueAttribute.Add(data[column][0] + name, true);
                return 0f;
            }
            else
            {
                if (boolValueAttribute.ContainsKey(data[column][0] + name))
                {
                    boolValueAttribute[data[column][0] + name] = false;
                }
                else
                    boolValueAttribute.Add(data[column][0] + name, false);
            }

            entropy = (1f * (s1 + s2) / (numColumn - 1)) * Result_I(s1, s2);
            return entropy;
        }

        //Trả về Gain của thuộc tính(Attribute) vị trí column
        private double Result_Gain(string node, List<List<string>> data, ref Dictionary<string, bool> boolValueAttribute, int column)
        {
            double gain = Result_I_S(data);
            List<string> temp = new List<string>();
            for (int i = 1; i < data[column].Count(); i++)
            {
                if (!temp.Contains(data[column][i]))
                {
                    gain -= Result_E(data, ref boolValueAttribute, column, data[column][i]);
                    temp.Add(data[column][i]);
                }
            }
            return gain;
        }

        //Trả về vị trí Best Attribute
        private int Result_Best_Attribute(string node, List<List<string>> data, ref Dictionary<string, bool> boolValueAttribute)
        {
            double bestGain = -99f;
            int indexBestAttribute = -1;
            for (int i = 0; i < data.Count() - 1; i++)
            {
                double temp = Result_Gain(node, data, ref boolValueAttribute, i);
                if (temp > bestGain)
                {
                    indexBestAttribute = i;
                    bestGain = temp;
                }
            }
            return indexBestAttribute;
        }

        private void DecisionTrees(string node, ref Node3 tree, List<List<string>> data, ref Dictionary<string, bool> boolValueAttribute)
        {
            List<List<string>> tempData = new List<List<string>>();
            tempData = data;
            int indexBestAttribute = Result_Best_Attribute(node, tempData, ref boolValueAttribute);
            tree.name = tempData[indexBestAttribute][0];
            tree.nodeRoot = false;
            tree.value = null;
            tree.arrNodes = new List<Node3>();
            List<string> tmpName = new List<string>();
            bool stopFunction = true;
            int index = 0;  //numNode = 0;
            for (int i = 1; i < tempData[indexBestAttribute].Count(); i++)
            {
                if (!tmpName.Contains(tempData[indexBestAttribute][i]))
                {
                    tmpName.Add(tempData[indexBestAttribute][i]);
                    if (Check_Leaf_Node(boolValueAttribute, tempData[indexBestAttribute][0] + tempData[indexBestAttribute][i]))
                    {                                               
                        //Nếu giá trị đang xét của Attribute chứa node lá 
                        Node3 tmp = new Node3();
                        tmp.name = tempData[indexBestAttribute][i];
                        tmp.value = tempData[tempData.Count() - 1][i];
                        tree.arrNodes.Add(tmp);
                        index++;
                    }
                    else
                    {
                        //Nếu giá trị đang xét của Attribute chứa node cành
                        Node3 tmp = new Node3();
                        tmp.name = tempData[indexBestAttribute][i];
                        tmp.value = null;
                        tmp.arrNodes = new List<Node3>();
                        tmp.arrNodes.Add(new Node3());
                        tree.arrNodes.Add(tmp);
                        stopFunction = false;
                        List<List<string>> tempData_cop = new List<List<string>>();
                        tempData_cop = UpdateData(tempData, indexBestAttribute, tempData[indexBestAttribute][i]);
                        Node3 tempNode = tree.arrNodes[index];
                        DecisionTrees(tmp.name, ref tempNode, tempData_cop, ref boolValueAttribute);
                        tree.arrNodes[index].arrNodes[tree.arrNodes[index].arrNodes.Count() - 1] = tempNode;
                        index++;
                    }

                }
            }
            //Nếu tất cả giá trị của thuộc tính Attribute đều là node lá ==> dừng
            if (stopFunction)
                return;
        }

        //Kiểm tra xem node đó có phải node lá hay ko
        private bool Check_Leaf_Node(Dictionary<string, bool> boolValueAttribute, string name)
        {
            if (boolValueAttribute[name])
                return true;
            return false;
        }

        private List<List<string>> UpdateData(List<List<string>> data, int column, string name)
        {
            List<List<string>> result = new List<List<string>>();
            int ii = 0;
            for (int i = 0; i < data.Count(); i++)
            {
                if (i != column)
                {
                    result.Add(new List<string>());
                    for (int j = 0; j < data[i].Count(); j++)
                        result[ii].Add(data[i][j]);
                    ii++;
                }
            }

            for (int i = data[column].Count() - 1; i > 0; i--)
            {
                if (data[column][i] != name)
                {
                    for (int j = 0; j < result.Count(); j++)
                    {
                        result[j].RemoveAt(i);
                    }
                }
            }

            return result;
        }        
        private void PaintDecisionTree()
        {
            DrawDecisionTree(decisionTree, grs, new Point(400, 0), true, 200);
        }

        private void DrawDecisionTree(Node3 decisionTreeX, Graphics grs, PointF point, bool start, float kc)
        {
            try
            {
                float tempX = point.X - 20;
                float tempY = point.Y + 15;
                if (start)
                {
                    tempX = point.X / 2;
                    tempY = point.Y + 15;
                }
                grs.DrawString(decisionTreeX.name, new Font("Segoe UI", 12), new SolidBrush(Color.Red), tempX - decisionTreeX.name.Length * 2, point.Y);
                int sumNode = decisionTreeX.arrNodes.Count();
                float width = point.X / (sumNode + 1);
                int ss = sumNode / 2 + 1;
                float tmpX;
                kc /= sumNode;
                for (int i = 1; i <= sumNode; i++)
                {
                    if (i < ss)
                    {
                        tmpX = tempX - kc * (i / ss + 1);
                    }
                    else
                    {
                        if (i == ss)
                        {
                            if (i == sumNode) tmpX = tempX + kc * (i / ss + 1);
                            else
                                tmpX = tempX;
                        }
                        else
                            tmpX = tempX + kc * (i / ss + 1);
                    }
                    if (start)
                    {
                        grs.DrawLine(new Pen(Color.Black), tempX + 20, tempY + 5, width * i + 20, tempY + 50);
                        grs.DrawString(decisionTreeX.arrNodes[i - 1].name, new Font("Segoe UI", 12), new SolidBrush(Color.DarkGreen), width * i - 10, tempY + 55);
                        if (decisionTreeX.arrNodes[i - 1].arrNodes == null)
                        {
                            if (decisionTreeX.arrNodes[i - 1].value != null)
                            {
                                grs.DrawLine(new Pen(Color.Black), width * i + 20, tempY + 75, width * i + 20, tempY + 110);
                                grs.DrawString(decisionTreeX.arrNodes[i - 1].value, new Font("Segoe UI", 12), new SolidBrush(Color.DarkBlue), width * i + 10, tempY + 110);
                            }
                        }
                        else
                        {
                            ss = sumNode / 2 + 1;
                            if (i < ss)
                            {
                                tmpX = width * i - 50 * (i / ss);
                            }
                            else
                                tmpX = width * i + 50 * (i / ss);
                            grs.DrawLine(new Pen(Color.Black), width * i + 20, tempY + 80, tmpX, tempY + 110);
                            DrawDecisionTree(decisionTreeX.arrNodes[i - 1].arrNodes[0], grs, new PointF(tmpX, tempY + 110), false, kc);
                        }
                    }
                    else
                    {
                        grs.DrawLine(new Pen(Color.Black), tempX + 20, tempY + 5, tmpX, tempY + 50);
                        grs.DrawString(decisionTreeX.arrNodes[i - 1].name, new Font("Segoe UI", 12), new SolidBrush(Color.DarkGreen), tmpX - 20, tempY + 55);
                        if (decisionTreeX.arrNodes[i - 1].arrNodes == null)
                        {
                            if (decisionTreeX.arrNodes[i - 1].value != null)
                            {
                                grs.DrawLine(new Pen(Color.Black), tmpX, tempY + 80, tmpX, tempY + 110);
                                grs.DrawString(decisionTreeX.arrNodes[i - 1].value, new Font("Segoe UI", 12), new SolidBrush(Color.DarkBlue), tmpX - 10, tempY + 110);
                            }
                        }
                        else
                        {
                            ss = sumNode / 2 + 1;
                            if (i < ss)
                            {
                                tmpX = width * i - 50 * (i / ss);
                            }
                            else
                                tmpX = width * i + 50 * (i / ss);
                            grs.DrawLine(new Pen(Color.Black), width * i + 20, tempY + 80, tmpX, tempY + 110);
                            DrawDecisionTree(decisionTreeX.arrNodes[i - 1].arrNodes[0], grs, new PointF(tmpX, tempY + 110), false, kc);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowRules(Node3 decisionTree, bool start, string rule)
        {
            string temp = "";
            if (start)
            {
                rule += "IF (" + decisionTree.name + "=";
            }
            else
                rule += "^ (" + decisionTree.name + "=";
            temp = rule;
            for (int i = 0; i < decisionTree.arrNodes.Count(); i++)
            {
                rule += decisionTree.arrNodes[i].name + ") ";
                Node3 node3 = decisionTree.arrNodes[i];
                if (node3.value == null) //nếu giá trị của Attribute ko chứa node lá => chứa 1 node Attribute khác(node nhánh)
                {
                    ShowRules(node3.arrNodes[0], false, rule);
                    rule = temp;
                }
                else
                {
                    rule += "THEN " + arrClause[arrClause.Count - 1] + "=" + node3.value + "\r\n";
                    rule = temp;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Open data source";
                ofd.Filter = "File Excel|*.xlsx; *.xls|All file|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // chạy file Excel theo đường dẫn
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);

                    // Lấy Sheet 1
                    Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets.get_Item(1);
                    // Lấy phạm vi dữ liệu
                    Excel.Range xlRange = xlWorksheet.UsedRange;
                    // Tạo mảng lưu trữ dữ liệu
                    object[,] valueArray = (object[,])xlRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                    int column = valueArray.GetLength(1);
                    int row = valueArray.GetLength(0);
                    //Hiển thị lên datagridview
                    DataTable dt = new DataTable();
                    for (int i = 1; i <= row; i++)
                    {
                        if (i == 1)
                        {
                            for (int j = 1; j <= column; j++)
                            {
                                dt.Columns.Add(valueArray[1, j].ToString());
                            }
                        }
                        else
                        {
                            List<string> arrObject = new List<string>();
                            for (int j = 1; j <= column; j++)
                            {
                                arrObject.Add(valueArray[i, j].ToString());
                            }
                            dt.Rows.Add(arrObject.ToArray());

                        }
                    }
                    dataGridViewClauses.DataSource = dt;

                    // Đóng Workbook.
                    xlWorkbook.Close(false);
                    // Đóng application.
                    xlApp.Quit();
                    //Khử hết đối tượng
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
