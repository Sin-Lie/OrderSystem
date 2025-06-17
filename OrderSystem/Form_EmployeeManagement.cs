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
    public partial class EmployeeManagementForm : Form
    {
        ManagerForm managerForm1;
        bool isFormWidthIncreased = false;

        //构造函数
        public EmployeeManagementForm(ManagerForm managerForm1)
        {
            this.managerForm1 = managerForm1;

            InitializeComponent();
        }


        //界面初始化
        private void EmployeeManagementForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(320, 170);
            this.Size = new Size(801, 489);

            Point groupBoxLocation = new Point(groupBox1.Location.X, groupBox1.Location.Y);
            groupBox2.Location = groupBoxLocation;
            groupBox3.Location = groupBoxLocation;
            groupBox4.Location = groupBoxLocation;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;

            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_EmployeeAll();
            dataGridView1.DataSource = dataTableLoadClass1.table_EmployeeAll;
            dataTableLoadClass1.tableLoad_EmployeeOff();
            dataGridView2.DataSource = dataTableLoadClass1.table_EmployeeOff;

            DataGridViewRow gridViewRow1 = dataGridView1.SelectedRows[0];
            textBox1.Text = XmlClass.readAvailableId("EmployeeID");
            textBox10.Text = gridViewRow1.Cells[1].Value.ToString();
            textBox7.Text = gridViewRow1.Cells[2].Value.ToString();
            textBox11.Text = gridViewRow1.Cells[0].Value.ToString();
            textBox8.Text = gridViewRow1.Cells[1].Value.ToString();
            textBox12.Text = gridViewRow1.Cells[2].Value.ToString();

            if (gridViewRow1.Cells[3].Value.ToString() == "是")
            {
                button11.Enabled = false;
            }
            else
            {
                button12.Enabled = false;
            }
        }


        //显示隐藏界面
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isFormWidthIncreased)
            {
                this.Width += 240;
                linkLabel1.Left += 240;
                isFormWidthIncreased = true;
            }
            groupBox1.Visible = true;

            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;

        }

        //显示隐藏界面
        private void button2_Click(object sender, EventArgs e)
        {
            if (!isFormWidthIncreased)
            {
                this.Width += 240;
                linkLabel1.Left += 240;
                isFormWidthIncreased = true;
            }

            groupBox2.Visible = true;

            groupBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;

        }

        //显示隐藏界面
        private void button3_Click(object sender, EventArgs e)
        {
            if (!isFormWidthIncreased)
            {
                this.Width += 240;
                linkLabel1.Left += 240;
                isFormWidthIncreased = true;
            }

            groupBox3.Visible = true;

            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox4.Visible = false;
        }

        //显示隐藏界面
        private void button4_Click(object sender, EventArgs e)
        {
            if (!isFormWidthIncreased)
            {
                this.Width += 240;
                linkLabel1.Left += 240;
                isFormWidthIncreased = true;
            }

            groupBox4.Visible = true;

            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }


        //隐藏 隐藏界面
        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            this.Width -= 240;
            linkLabel1.Left -= 240;
            isFormWidthIncreased = false;
        }

        //隐藏 隐藏界面
        private void button8_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            this.Width -= 240;
            linkLabel1.Left -= 240;
            isFormWidthIncreased = false;
        }

        //隐藏 隐藏界面
        private void button10_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            this.Width -= 240;
            linkLabel1.Left -= 240;
            isFormWidthIncreased = false;
        }

        //隐藏 隐藏界面
        private void button13_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            this.Width -= 240;
            linkLabel1.Left -= 240;
            isFormWidthIncreased = false;
        }


        //密码显示
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = textBox5.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        //密码显示
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.PasswordChar = checkBox2.Checked ? '\0' : '*';
        }


        //添加员工
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入姓名");
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (textBox4.Text == "")
            {
                MessageBox.Show("请输入密码");
                return;
            }
            if (textBox4.Text.Length < 8 || textBox4.Text.Length > 12)
            {
                MessageBox.Show("密码需为8-12位字符");
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("请再次输入密码");
                return;
            }

            if (textBox4.Text == textBox5.Text)
            {
                XmlClass.addInfo("EmployeeInformation.xml", textBox2.Text, textBox3.Text, textBox4.Text);
                if (XmlClass.IdExists)
                {
                    MessageBox.Show("用户名已存在");
                    return;
                }

                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("两次输入密码不一致");
            }

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_EmployeeAll();
            dataGridView1.DataSource = dataTableLoadClass1.table_EmployeeAll;
            dataTableLoadClass1.tableLoad_EmployeeOff();
            dataGridView2.DataSource = dataTableLoadClass1.table_EmployeeOff;
            
        }


        //当dataGridView选中行改变时，刷新textBox的信息
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow gridViewRow1 = dataGridView1.SelectedRows[0];
                textBox10.Text = gridViewRow1.Cells[1].Value.ToString();
                textBox7.Text = gridViewRow1.Cells[2].Value.ToString();
                textBox11.Text = gridViewRow1.Cells[0].Value.ToString();
                textBox8.Text = gridViewRow1.Cells[1].Value.ToString();
                textBox12.Text = gridViewRow1.Cells[2].Value.ToString();
                if (gridViewRow1.Cells[3].Value.ToString() == "是")
                {
                    button11.Enabled = false;
                    button12.Enabled = true;
                }
                else
                {
                    button12.Enabled = false;
                    button11.Enabled = true;
                }
            }
            
        }


        //当dataGridView选中行改变时，刷新textBox的信息
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow gridViewRow1 = dataGridView2.SelectedRows[0];
                textBox10.Text = gridViewRow1.Cells[1].Value.ToString();
                textBox7.Text = gridViewRow1.Cells[2].Value.ToString();
                textBox11.Text = gridViewRow1.Cells[0].Value.ToString();
                textBox8.Text = gridViewRow1.Cells[1].Value.ToString();
                textBox12.Text = gridViewRow1.Cells[2].Value.ToString();
                if (gridViewRow1.Cells[3].Value.ToString() == "是")
                {
                    button11.Enabled = false;
                    button12.Enabled = true;
                }
                else
                {
                    button12.Enabled = false;
                    button11.Enabled = true;
                }
            }
        }


        //修改员工姓名
        private void button7_Click(object sender, EventArgs e)
        {
            XmlClass.modifyInfo("EmployeeInformation.xml", "Name", textBox10.Text, "Name", textBox9.Text);
            MessageBox.Show("修改成功");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_EmployeeAll();
            dataGridView1.DataSource = dataTableLoadClass1.table_EmployeeAll;
            dataTableLoadClass1.tableLoad_EmployeeOff();
            dataGridView2.DataSource = dataTableLoadClass1.table_EmployeeOff;
        }


        //修改密码
        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Length < 8 || textBox6.Text.Length > 12)
            {
                MessageBox.Show("密码需为8-12位字符");
                return;
            }

            XmlClass.modifyInfo("EmployeeInformation.xml", "Username", textBox7.Text, "Passward", textBox6.Text);
            MessageBox.Show("修改成功");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_EmployeeAll();
            dataGridView1.DataSource = dataTableLoadClass1.table_EmployeeAll;
            dataTableLoadClass1.tableLoad_EmployeeOff();
            dataGridView2.DataSource = dataTableLoadClass1.table_EmployeeOff;
        }


        //启用账号
        private void button11_Click(object sender, EventArgs e)
        {
            XmlClass.modifyInfo("EmployeeInformation.xml", "ID", textBox11.Text, "OnOff", "是");
            MessageBox.Show("账号已启用");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_EmployeeAll();
            dataGridView1.DataSource = dataTableLoadClass1.table_EmployeeAll;
            dataTableLoadClass1.tableLoad_EmployeeOff();
            dataGridView2.DataSource = dataTableLoadClass1.table_EmployeeOff;
        }


        //停用账号
        private void button12_Click(object sender, EventArgs e)
        {
            XmlClass.modifyInfo("EmployeeInformation.xml", "ID", textBox11.Text, "OnOff", "否");
            MessageBox.Show("账号已停用");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_EmployeeAll();
            dataGridView1.DataSource = dataTableLoadClass1.table_EmployeeAll;
            dataTableLoadClass1.tableLoad_EmployeeOff();
            dataGridView2.DataSource = dataTableLoadClass1.table_EmployeeOff;
        }


        //返回管理员界面
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            managerForm1.Show();
            this.Close();
        }


    }
}
