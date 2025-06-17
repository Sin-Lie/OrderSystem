using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    public partial class CheckoutUserControl : UserControl
    {
        ListView listView2;
        string EmployeeId;

        //构造函数
        public CheckoutUserControl(ListView listView2 ,string EmployeeId)
        {
            this.listView2 = listView2;
            this.EmployeeId = EmployeeId;
            InitializeComponent();
        }

        //构造函数
        public CheckoutUserControl()
        {
            InitializeComponent();
        }

        public int Sum = 0;

        /// <summary>
        /// 账单显示
        /// </summary>
        public void show()
        {
            //账单显示
            listView1.View = View.Details;
            listView1.Columns.Add("菜名", 150);
            listView1.Columns.Add("价格", 100);

            int sum=0;

            for (int i = 0; i < listView2.Items.Count; i++)
            {
                sum += int.Parse(listView2.Items[i].SubItems[1].Text) * int.Parse(listView2.Items[i].SubItems[2].Text);
                listView1.Items.Add(listView2.Items[i].Text);
                listView1.Items[i].SubItems.Add((int.Parse(listView2.Items[i].SubItems[1].Text) * int.Parse(listView2.Items[i].SubItems[2].Text)).ToString());
            }
            listView1.Items.Add("");
            listView1.Items.Add("总价");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add(sum.ToString());
            Sum = sum;
        }

        /// <summary>
        /// 确认结账，保存账单数据
        /// </summary>
        public void checkout()
        {
            //获取可用单号
            string ID = XmlClass.getAvailableId("OrderID");

            //存入账单数据
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                string sum = (int.Parse(listView2.Items[i].SubItems[1].Text) * int.Parse(listView2.Items[i].SubItems[2].Text)).ToString();
                XmlClass.addInfo(ID,listView2.Items[i].Text, listView2.Items[i].SubItems[1].Text, listView2.Items[i].SubItems[2].Text, sum, EmployeeId);
            }
        }
    }
}
