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
    public partial class ModifyPasswordForm : Form
    {
        OrderForm orderForm1;
        ManagerForm managerForm1;
        string ID;
        string xmlPath;

        
        //员工修改密码界面构造函数
        public ModifyPasswordForm(OrderForm orderForm1,string EmployeeId)
        {
            this.orderForm1 = orderForm1;
            this.ID = EmployeeId;
            this.xmlPath = "EmployeeInformation.xml";

            InitializeComponent();
        }


        //管理员修改密码界面构造函数
        public ModifyPasswordForm(ManagerForm managerForm1, string EmployerId)
        {
            this.managerForm1 = managerForm1;
            this.ID = EmployerId;
            this.xmlPath = "EmployerInformation.xml";

            InitializeComponent();
        }



        //界面初始化
        private void ModifyPasswordForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = ID;
            this.Location = new Point(515, 175);
        }


        //确认修改密码
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != XmlClass.getInfo(xmlPath, "ID", ID, "Passward"))
            {
                MessageBox.Show("原密码输入错误");
                return;
            }
            if (textBox3.Text.Length < 8 || textBox3.Text.Length > 12)
            {
                MessageBox.Show("密码需为8-12位字符");
                return;
            }
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("两次输入的密码不一致，请重新输入密码");
                return;
            }
            XmlClass.modifyInfo(xmlPath, "ID", ID, "Passward", textBox3.Text);
            MessageBox.Show("修改成功");

            //修改成功后返回
            if (xmlPath == "EmployeeInformation.xml")
            {
                orderForm1.Show();
                this.Close();
                return;
            }
            managerForm1.Show();
            this.Close();
        }


        //返回
        private void button1_Click(object sender, EventArgs e)
        {
            //返回点菜界面
            if(xmlPath== "EmployeeInformation.xml")
            {
                orderForm1.Show();
                this.Close();
                return;
            }

            //返回员工管理界面
            managerForm1.Show();
            this.Close();
        }


        //密码显示
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = textBox3.PasswordChar = textBox4.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        
    }
}
