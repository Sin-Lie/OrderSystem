﻿﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OrderSystem
{
    /// <summary>
    /// 登录界面
    /// </summary>
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //界面初始化
        private void Login_Load(object sender, EventArgs e)
        {
            
            //判断系统是否激活
            if (!LoginClass.isInilized())
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                tabPage2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                this.Hide();
                MessageBox.Show("您正在首次使用我们的软件，初始化请点击“激活”按钮");
                this.Show();
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                tabPage2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                button3.Visible = false;
            }

            this.Location = new Point(415, 225);

            //界面加载后聚焦在登录按钮
            this.BeginInvoke((MethodInvoker)delegate
            {
                button1.Focus();
            });

        }

        //初次使用激活
        private void button3_Click(object sender, EventArgs e)
        {
            LoginClass.initilze();
            MessageBox.Show("激活成功");
            Login_Load(this, EventArgs.Empty);
        }

        //点菜员登录
        private void button1_Click(object sender, EventArgs e)
        {
            string ID;
            MessageBox.Show(LoginClass.strCheck("EmployeeInformation.xml", textBox1.Text, textBox2.Text,out ID));
            if (LoginClass.IsLoginAllowed)
            {
                OrderForm orderForm1 = new OrderForm(this,ID);
                orderForm1.Show();
                this.Hide();
            }
        }

        //管理员登录
        private void button2_Click_1(object sender, EventArgs e)
        {
            string ID="";
            MessageBox.Show(LoginClass.strCheck("EmployerInformation.xml", textBox3.Text, textBox4.Text,out ID));
            if (LoginClass.IsLoginAllowed)
            {
                ManagerForm managerForm1 = new ManagerForm(this,ID);
                managerForm1.Show();
                this.Hide();
            }
        }

        //密码显示
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
        //密码显示
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = checkBox2.Checked ? '\0' : '*';
        }


    }
}
