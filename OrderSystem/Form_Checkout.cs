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
    public partial class CheckoutForm : Form
    {
        OrderForm OrderForm1;
        ListView ListView2;
        string EmployeeId;
        CheckoutUserControl checkoutUserControl1;

        //构造函数
        public CheckoutForm(OrderForm OrderForm1,ListView ListView2,string EmployeeId)
        {
            //传参
            this.OrderForm1 = OrderForm1;
            this.ListView2 = ListView2;
            this.EmployeeId = EmployeeId;

            //添加自定义控件CheckoutUserControl
            CheckoutUserControl checkoutUserControl1 = new CheckoutUserControl(ListView2, EmployeeId);
            this.Controls.Add(checkoutUserControl1);
            checkoutUserControl1.Location = new Point(106, 28);
            this.checkoutUserControl1 = checkoutUserControl1;

            InitializeComponent(); 
        }  


        //界面初始化
        private void CheckoutForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(515, 175);

            //账单显示
            checkoutUserControl1.show();
        }


        //返回点菜界面
        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm1.Show();
            this.Hide();

        }


        //结账，保存账单，自动返回点菜界面
        private void button2_Click(object sender, EventArgs e)
        {
            checkoutUserControl1.checkout();
            MessageBox.Show($"结账成功，本次消费{checkoutUserControl1.Sum}元");
            
            //返回点菜界面 
            OrderForm1.Show();
            this.Close();
        }
    }
}
