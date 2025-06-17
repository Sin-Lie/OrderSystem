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
    public partial class ComboboxSearchByName : UserControl
    {
        public ComboboxSearchByName()
        {
            InitializeComponent();
        }

        //菜名
        public string dishName="全部";

        //加载Combobox的Items
        private void ComboboxSearchByName_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("全部");
            comboBox1.Text = "全部";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Dishes_Vegetables.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Dish");
            foreach(XmlNode xn in nodeList)
            {
                comboBox1.Items.Add(xn.ChildNodes[0].InnerText);
            }

            
            xmlDoc.Load("Dishes_Meat.XML");
            nodeList = xmlDoc.SelectNodes("//Dish");
            foreach (XmlNode xn in nodeList)
            {
                comboBox1.Items.Add(xn.ChildNodes[0].InnerText);
            }

            xmlDoc.Load("Dishes_Soups.XML");
            nodeList = xmlDoc.SelectNodes("//Dish");
            foreach (XmlNode xn in nodeList)
            {
                comboBox1.Items.Add(xn.ChildNodes[0].InnerText);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dishName = comboBox1.Text;
        }


    }
}
