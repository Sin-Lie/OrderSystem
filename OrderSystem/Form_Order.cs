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
    public partial class OrderForm : Form
    {

        string EmployeeId;
        Login loginForm1;

        //构造函数
        public OrderForm(Login loginForm1, string EmployeeId)
        {
            //传参
            this.loginForm1 = loginForm1;
            this.EmployeeId = EmployeeId;

            InitializeComponent();
        }


        //界面初始化
        private void OrderForm_Load(object sender, EventArgs e)
        {
            //界面样式
            this.Size = new Size(768, 450);
            this.Location = new Point(320, 170);

            //将时间显示栏靠右对齐
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;   // 配置状态栏布局模式：水平排列，空间不足时自动折叠到溢出菜单
            this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;           // 将第一个状态栏标签靠右对齐，便于显示右侧信息
            toolStripStatusLabel1.Text = XmlClass.getInfo("EmployeeInformation.xml", "ID", EmployeeId, "Name");

        }


        //结账
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (orderFormUserControl1.listView2.Items.Count == 0)
            {
                MessageBox.Show("请先点菜");
                return;
            }

            CheckoutForm checkoutForm1 = new CheckoutForm(this, orderFormUserControl1.listView2, EmployeeId);
            this.Hide();
            checkoutForm1.Show();
        }


        //点菜
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            orderFormUserControl1.order();
        }


        //删除一份
        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            orderFormUserControl1.removeSingleOrder();
        }


        //全部删除
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            orderFormUserControl1.listView2.Items.Clear();
        }


        //清空
        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            orderFormUserControl1.listView1.Items.Clear();
            orderFormUserControl1.listView2.Items.Clear();
        }


        //退出程序
        private void label1_Click(object sender, EventArgs e)
        {
            loginForm1.Close();
        }


        //退出登录
        private void label2_Click(object sender, EventArgs e)
        {
            loginForm1.Show();
            this.Close();
        }


        //修改密码，跳转至修改密码界面
        private void label3_Click(object sender, EventArgs e)
        {
            ModifyPasswordForm modifyPasswordForm1 = new ModifyPasswordForm(this, EmployeeId);
            modifyPasswordForm1.Show();
            this.Hide();
        }


        //时间显示
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
