using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    
    public partial class OrderSearchForm : Form
    {
        ManagerForm managerForm1;
        string EmployerId;

        //构造函数
        public OrderSearchForm(ManagerForm managerForm1,string EmployerId)
        {
            this.managerForm1 = managerForm1;
            this.EmployerId = EmployerId;
            InitializeComponent();
        }


        //界面初始化
        private void OrderSearchForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(270, 140);
            //this.Size = new Size(829, 550);
            this.Size = new Size(929, 550);


            //对控件的位置进行设置
            panel2.Visible = false;
            panel3.Visible = false;
            Point panelLocation = new Point(panel1.Location.X, panel1.Location.Y);
            panel2.Location = panelLocation;
            panel3.Location = panelLocation;
            Point buttonLocation = new Point(button1.Location.X, button1.Location.Y);
            button2.Location = buttonLocation;
            button3.Location = buttonLocation;

            button4.Visible = false;
            button4.Location = new Point(821, 34);

            dataGridView1.Dock = DockStyle.Fill;

            //将时间显示栏靠右对齐
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;   // 配置状态栏布局模式：水平排列，空间不足时自动折叠到溢出菜单
            this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;           // 将第一个状态栏标签靠右对齐，便于显示右侧信息
            toolStripStatusLabel1.Text = XmlClass.getInfo("EmployerInformation.xml", "ID", EmployerId, "Name");

        }


        //按菜名查询——详细信息
        private void button1_Click(object sender, EventArgs e)
        {
            OrderSearchClass orderSearch = new OrderSearchClass();
            orderSearch.searchByName(ComboboxSearchByName1.dishName);
            dataGridView1.DataSource = orderSearch.table_SearchByName;
        }


        //按日期查询——详情信息
        private void button2_Click(object sender, EventArgs e)
        {
            OrderSearchClass orderSearch = new OrderSearchClass();
            orderSearch.searchByTime(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = orderSearch.table_SearchByTime;

        }


        //按员工查询——详细信息
        private void button3_Click(object sender, EventArgs e)
        {
            OrderSearchClass orderSearch = new OrderSearchClass();
            orderSearch.searchBySingleEmployee(comboboxSearchByEmployee1.ID);
            dataGridView1.DataSource = orderSearch.table_SearchByEmployee;
        }


        //控件显示
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked && tabControl1.SelectedIndex == 0)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        //控件显示
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked && tabControl1.SelectedIndex == 0)
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        //控件显示
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked && tabControl1.SelectedIndex == 0)
            {
                panel3.Visible = true;
            }
            else
            {
                panel3.Visible = false;
            }
        }

        //控件显示
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                button4.Visible = true;
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
            }
            else
            {
                button4.Visible = false;
                if (radioButton1.Checked)
                {
                    panel1.Visible = true;
                }
                else if (radioButton2.Checked)
                {
                    panel2.Visible = true;
                }
                else if (radioButton3.Checked)
                {
                    panel3.Visible = true;
                }
            }
        }


        //汇总查询并画图表
        private void button4_Click(object sender, EventArgs e)
        {
            //按菜名汇总
            if (radioButton1.Checked)
            {
                OrderSearchClass orderSearch = new OrderSearchClass();
                orderSearch.searchByName_Summary();
                dataGridView2.DataSource = orderSearch.table_Name_Summary;

                //画图
                DGV_Exporter.drawChart_Pie(orderSearch.table_Name_Summary, chart1, "菜品销售额占比");
            }

            //按日期汇总
            else if (radioButton2.Checked)
            {
                OrderSearchClass orderSearch = new OrderSearchClass();
                orderSearch.searchByTime_Summary();
                dataGridView2.DataSource = orderSearch.table_Time_Summary;

                //画图
                DGV_Exporter.drawChart_Bar(orderSearch.table_Time_Summary, chart1, "每日销售额统计图");
            }

            //按点菜员汇总
            else if (radioButton3.Checked)
            {
                OrderSearchClass orderSearch = new OrderSearchClass();
                orderSearch.searchByEmployee_Summary();
                dataGridView2.DataSource = orderSearch.table_Employee_Summary;

                //画图
                DGV_Exporter.drawChart_Bar(orderSearch.table_Employee_Summary, chart1, "员工销售额统计图");
            }
            
        }


        //返回管理员界面
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            managerForm1.Show();
            this.Close();
        }


        //导出Excel
        private void label1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("无数据");
                    return;
                }
                DGV_Exporter.exportToExcel(dataGridView1, "消费明细");
            }
            else
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    MessageBox.Show("无数据");
                    return;
                }
                DGV_Exporter.exportToExcel(dataGridView2, "消费明细");
            }
        }


        //显示时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


        //鼠标移至标签、点击标签时改变颜色，增强交互效果
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.BackColor = Color.LightBlue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = SystemColors.ControlLight;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.BackColor = Color.DodgerBlue;
            label1.BorderStyle = BorderStyle.Fixed3D;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.BackColor = SystemColors.ControlLight;
            label1.BorderStyle = BorderStyle.None;
            timer2.Enabled = false;
        }
    }
}
