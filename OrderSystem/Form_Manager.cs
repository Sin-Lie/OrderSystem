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
    public partial class ManagerForm : Form
    {
        string EmployerId;
        Login loginForm1;

        //构造函数
        public ManagerForm(Login loginForm1,string EmployerId)
        {
            this.loginForm1 = loginForm1;
            this.EmployerId = EmployerId;
            InitializeComponent();
        }

        //界面初始化
        private void ManagerForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(320, 170);

            //
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Text = "\n\n欢迎登录管理员界面!!!";
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

            //将时间显示栏靠右对齐
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;   // 配置状态栏布局模式：水平排列，空间不足时自动折叠到溢出菜单
            this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;           // 将第一个状态栏标签靠右对齐，便于显示右侧信息
            toolStripStatusLabel1.Text = XmlClass.getInfo("EmployerInformation.xml", "ID", EmployerId, "Name");
        }


        //退出程序
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginForm1.Close();
        }


        //退出登录
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginForm1.Show();
            this.Close();
        }
        

        //修改密码，跳转至修改密码界面
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModifyPasswordForm modifyPasswordForm1 = new ModifyPasswordForm(this, EmployerId);
            modifyPasswordForm1.Show();
            this.Hide();
        }


        //时间显示
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //
        private void button1_Click(object sender, EventArgs e)
        {
            OrderSearchForm orderSearchForm1 = new OrderSearchForm(this, EmployerId);
            orderSearchForm1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeManagementForm employeeManagementForm1 = new EmployeeManagementForm(this);
            employeeManagementForm1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DishManagementForm dishManagementForm1 = new DishManagementForm(this);
            dishManagementForm1.Show();
            this.Hide();
        }
    }
}
