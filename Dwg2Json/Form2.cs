using Dwg2Json.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Teigha.Geometry;

namespace Dwg2Json
{
    public partial class Form2 : Form
    {
        private static FieldInfo[] p;      
        private JsonPolyline jsonPolyline ;
        private List<Control> lab = new List<Control>();
        private List<Control> txb = new List<Control>();
        private List<Control> pxb = new List<Control>();

        static Form2()
        {
            p= new JsonPolyline().GetType().GetFields();
        }
        public Form2(JsonPolyline jsonPolyline)
        {
            InitializeComponent();          
            this.jsonPolyline = jsonPolyline;
            Init();          
        }      
        private void button2_Click(object sender, EventArgs e)
          {
              
             if(txb[0].Text==""|| txb[0].Text == null)
            {
                MessageBox.Show("name不可为空");
                txb[0].Focus();
                return;
            }
            if (txb[1].Text == "" || txb[1].Text == null)
            {
                MessageBox.Show("area不可为空");
                txb[1].Focus();
                return;
            }
            for (int i = 0; i < lab.Count; i++)
              {
                  string name = lab[i].Text;
                  if (name != "coordinates")
                  {
                      FieldInfo p = jsonPolyline.GetType().GetField(name);
                      p.SetValue(jsonPolyline, txb[i].Text);
                  }
              }

            for (int i = 0; i < pxb.Count; i = i + 3)
            {
                if (pxb[i].Text == "")
                {
                    pxb[i].Text = "0";
                }
                if (pxb[i+1].Text == "")
                {
                    pxb[i+1].Text = "0";
                }
                if (pxb[i+2].Text == "")
                {
                    pxb[i+2].Text = "0";
                }
                jsonPolyline.coordinates[i / 3 ] = new Point3d(double.Parse(pxb[i].Text), double.Parse(pxb[i+1].Text), double.Parse(pxb[i+2].Text));
                 
            }
            Helper.LpaddLine(jsonPolyline);
              MessageBox.Show("保存成功");
              this.DialogResult = DialogResult.OK;
              this.Close();
              this.Dispose();
          }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }      
        private void button3_Click(object sender, EventArgs e)
        {
            if (FormMain.changehd)
            {
                Helper.LpaddLine(jsonPolyline);
            }            
            this.DialogResult = DialogResult.Cancel;          
            this.Close();
            this.Dispose();
        }  
        /// <summary>
        /// 初始化form
        /// </summary>
        void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                Label label = new Label();
                label.Location = new System.Drawing.Point(30, 50 * (i + 1) + 5);
                label.Size = new System.Drawing.Size(60, 15);
                label.Name = "label" + (i + 1);
                label.Text = p[i].Name;
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Location = new System.Drawing.Point(100, 50 * (i + 1));
                textBox.Size = new System.Drawing.Size(300, 15);
                textBox.Name = "textBox" + (i + 1);
                if (p[i].GetValue(jsonPolyline) != null)
                {
                    textBox.Text = p[i].GetValue(jsonPolyline).ToString();
                }

                this.Controls.Add(textBox);
            }
            foreach (Control cin in this.Controls)
            {
                if (cin is TextBox)
                {
                    txb.Add(cin);
                }
                if (cin is Label)
                {
                    lab.Add(cin);
                }
            }
            panel1.AutoScroll = true;
            int count = 1;
            if (jsonPolyline.coordinates != null)
            {
               count = jsonPolyline.coordinates.Length + 1;
            }
           
            for (int i = 0; i < count; i++)
            {                
                for(int j = 0; j < 4; j++)
                {
                    if (i == 0)
                    {
                        Label label = new Label();
                        label.Location = new System.Drawing.Point(90 * j + 10, 10);
                        label.Size = new System.Drawing.Size(60, 15);
                        label.Name = "label" + (i + 1);
                        switch (j)
                        {
                            case 0:label.Text = "序号";
                                break;
                            case 1: label.Text = "x轴坐标";
                                break;
                            case 2:
                                label.Text = "y轴坐标";
                                break;
                            default:
                                label.Text = "z轴坐标";
                                break;
                        }
                        this.panel1.Controls.Add(label);
                    }
                    else
                    {
                        TextBox textBox = new TextBox();
                        textBox.Location = new System.Drawing.Point(90 * j + 10, 45 *i + 10);
                        textBox.Size = new System.Drawing.Size(60, 15);                       
                        textBox.Name = "textBox" + (i + 1) + "a";
                        if (j == 0)
                        {
                            textBox.Text = i.ToString();
                        }
                        this.panel1.Controls.Add(textBox);
                        if (j != 0)
                        {
                            pxb.Add(textBox);
                        }
                    }                  
                }
            }        
            for(int i = 0; i < pxb.Count; i = i + 3)
            {
                pxb[i].Text = jsonPolyline.coordinates[i/3].X.ToString();
                pxb[i+1].Text = jsonPolyline.coordinates[i / 3 ].Y.ToString();
                pxb[i+2].Text = jsonPolyline.coordinates[i / 3 ].Z.ToString();             
            }
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
