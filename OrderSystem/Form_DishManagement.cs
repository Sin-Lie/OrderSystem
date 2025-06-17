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
    public partial class DishManagementForm : Form
    {
        ManagerForm managerForm1;
        string dishTypeToLoad = "Vegetables";

        //构造函数
        public DishManagementForm(ManagerForm managerForm1)
        {
            this.managerForm1 = managerForm1;

            InitializeComponent();
        }

        //界面初始化
        private void DishManagementForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(320, 170);
            this.Size = new Size(801, 489);

            Point groupBoxLocation = new Point(groupBox1.Location.X, groupBox1.Location.Y);
            groupBox2.Location = groupBoxLocation;
            groupBox4.Location = groupBoxLocation;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox4.Visible = false;

            comboBox1.Items.Add("荤菜类");
            comboBox1.Items.Add("蔬菜类");
            comboBox1.Items.Add("汤类");

            //加载DataTable
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
            

            
            DataGridViewRow gridViewRow1 = dataGridView1.SelectedRows[0];
            textBox10.Text = gridViewRow1.Cells[0].Value.ToString();
            textBox9.Text = gridViewRow1.Cells[1].Value.ToString();
            textBox11.Text = gridViewRow1.Cells[0].Value.ToString();
            textBox8.Text = gridViewRow1.Cells[1].Value.ToString();

            if (gridViewRow1.Cells[2].Value.ToString() == "是")
            {
                button11.Enabled = false;
            }
            else
            {
                button12.Enabled = false;
            }



        }

        bool isFormWidthIncreased = false;

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
        private void button13_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            this.Width -= 240;
            linkLabel1.Left -= 240;
            isFormWidthIncreased = false;
        }


        //添加菜品
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入菜名");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入单价");
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选择菜品种类");
                return;
            }

            XmlClass.addInfo(textBox1.Text,textBox2.Text,comboBox1.Text);
            if (XmlClass.IdExists)
            {
                MessageBox.Show("该菜品已存在");
                return;
            }
            MessageBox.Show("添加成功");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;

        }


        //当dataGridView选中行改变时，刷新textBox的信息
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow gridViewRow1 = dataGridView1.SelectedRows[0];
                textBox10.Text = gridViewRow1.Cells[0].Value.ToString();
                textBox9.Text = gridViewRow1.Cells[1].Value.ToString();
                textBox11.Text = gridViewRow1.Cells[0].Value.ToString();
                textBox8.Text = gridViewRow1.Cells[1].Value.ToString();
                if (gridViewRow1.Cells[2].Value.ToString() == "是")
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


        //修改菜品价格
        private void button7_Click(object sender, EventArgs e)
        {
            XmlClass.modifyInfo($"Dishes_{dishTypeToLoad}.XML", "Name", textBox10.Text, "Price", textBox9.Text);
            MessageBox.Show("修改成功");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
        }


        //修改菜品状态——上架
        private void button11_Click(object sender, EventArgs e)
        {
            XmlClass.modifyInfo($"Dishes_{dishTypeToLoad}.XML", "Name", textBox11.Text, "OnOff", "是");
            MessageBox.Show("菜品已上架");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
        }

        //修改菜品状态——下架
        private void button12_Click(object sender, EventArgs e)
        {
            XmlClass.modifyInfo($"Dishes_{dishTypeToLoad}.XML", "Name", textBox11.Text, "OnOff", "否");
            MessageBox.Show("菜品已下架");

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
        }

        //返回管理员界面
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            managerForm1.Show();
            this.Close();
        }


        //选中荤菜类
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            dishTypeToLoad = "Meat";

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
        }

        //选中蔬菜类
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            dishTypeToLoad = "Vegetables";

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
        }

        //选中汤类
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            dishTypeToLoad = "Soups";

            //重新加载dataGridView
            DataTableLoadClass dataTableLoadClass1 = new DataTableLoadClass();
            dataTableLoadClass1.tableLoad_Dish(dishTypeToLoad);
            dataGridView1.DataSource = dataTableLoadClass1.table_Dish;
        }
    }
}
