using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OrderSystem
{
    public partial class OrderFormUserControl : UserControl
    {
        public OrderFormUserControl()
        {
            InitializeComponent();
        }

        //界面初始化
        private void OrderFormUserControl_Load(object sender, EventArgs e)
        {
            //菜名类名初始化
            treeView1.Nodes.Add("0", "菜名");
            treeView1.Nodes["0"].Nodes.Add("00", "蔬菜类");
            treeView1.Nodes["0"].Nodes.Add("01", "荤菜类");
            treeView1.Nodes["0"].Nodes.Add("02", "汤类");
            //菜单详情初始化
            listView1.View = View.Details;
            listView1.Columns.Add("菜单", 150);
            listView1.Columns.Add("单价", 150);
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            //已点菜品初始化
            listView2.View = View.Details;
            listView2.Columns.Add("菜单", 100);
            listView2.Columns.Add("单价", 65);
            listView2.Columns.Add("份数", 65);
            listView2.FullRowSelect = true;
            listView2.GridLines = true;

            //新增双击点菜及移除事件
            listView1.MouseDoubleClick += OrderByDoubleClick;
            listView2.MouseDoubleClick += RemoveByDoubleClick;
        }

        //菜品详情显示
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Name == "00")
            {
                listView1.Items.Clear();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Dishes_Vegetables.XML");
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Dish");

                int i = 0;
                foreach (XmlNode xn in nodeList)
                {
                    listView1.Items.Add(xn.ChildNodes[0].InnerText);
                    listView1.Items[i].SubItems.Add(xn.ChildNodes[1].InnerText);
                    i++;
                }
            }
            else if (treeView1.SelectedNode.Name == "01")
            {
                listView1.Items.Clear();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Dishes_Meat.XML");
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Dish");

                int i = 0;
                foreach (XmlNode xn in nodeList)
                {
                    listView1.Items.Add(xn.ChildNodes[0].InnerText);
                    listView1.Items[i].SubItems.Add(xn.ChildNodes[1].InnerText);
                    i++;
                }
            }
            else if (treeView1.SelectedNode.Name == "02")
            {
                listView1.Items.Clear();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Dishes_Soups.XML");
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Dish");

                int i = 0;
                foreach (XmlNode xn in nodeList)
                {
                    listView1.Items.Add(xn.ChildNodes[0].InnerText);
                    listView1.Items[i].SubItems.Add(xn.ChildNodes[1].InnerText);
                    i++;
                }
            }

        }

        //点菜
        public void order()
        {
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                int m = -1;
                for (int j = 0; j < listView2.Items.Count; j++)
                {
                    if (listView1.SelectedItems[i].Text == listView2.Items[j].Text)
                    {
                        m = j;
                    }
                }
                if (m == -1)
                {
                    listView2.Items.Add(listView1.SelectedItems[i].Text);
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(listView1.SelectedItems[i].SubItems[1].Text);
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add("1");
                }
                else
                {
                    listView2.Items[m].SubItems[2].Text = (int.Parse(listView2.Items[m].SubItems[2].Text) + 1).ToString();
                }
            }
            listView1.SelectedItems.Clear();
        }

        //删除一份
        public void removeSingleOrder()
        {
            for (int i = listView2.SelectedItems.Count - 1; i > -1; i--)
            {
                if (int.Parse(listView2.SelectedItems[i].SubItems[2].Text) == 1)
                {
                    listView2.SelectedItems[i].Remove();
                }
                else
                {
                    listView2.SelectedItems[i].SubItems[2].Text = (int.Parse(listView2.SelectedItems[i].SubItems[2].Text) - 1).ToString();
                }
            }
            listView2.SelectedItems.Clear();
        }


        //双击点菜
        private void OrderByDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    int m = -1;
                    for (int j = 0; j < listView2.Items.Count; j++)
                    {
                        if (listView1.SelectedItems[i].Text == listView2.Items[j].Text)
                        {
                            m = j;
                        }
                    }
                    if (m == -1)
                    {
                        listView2.Items.Add(listView1.SelectedItems[i].Text);
                        listView2.Items[listView2.Items.Count - 1].SubItems.Add(listView1.SelectedItems[i].SubItems[1].Text);
                        listView2.Items[listView2.Items.Count - 1].SubItems.Add("1");
                    }
                    else
                    {
                        listView2.Items[m].SubItems[2].Text = (int.Parse(listView2.Items[m].SubItems[2].Text) + 1).ToString();
                    }
                }
                listView1.SelectedItems.Clear();
            }
        }


        //双击移除菜品
        private void RemoveByDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                for (int i = listView2.SelectedItems.Count - 1; i > -1; i--)
                {
                    if (int.Parse(listView2.SelectedItems[i].SubItems[2].Text) == 1)
                    {
                        listView2.SelectedItems[i].Remove();
                    }
                    else
                    {
                        listView2.SelectedItems[i].SubItems[2].Text = (int.Parse(listView2.SelectedItems[i].SubItems[2].Text) - 1).ToString();
                    }
                }
                listView2.SelectedItems.Clear();
            }
        }


    }
}
