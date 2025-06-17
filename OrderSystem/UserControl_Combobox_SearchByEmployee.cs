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
    public partial class ComboboxSearchByEmployee : UserControl
    {
        public ComboboxSearchByEmployee()
        {
            InitializeComponent();
        }

        //员工ID
        public string ID;

        //加载Combobox的Items
        private void ComboboxSearchByEmployee_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("全部");
            comboBox1.Text = "全部";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("EmployeeInformation.xml");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Employee");
            foreach (XmlNode xn in nodeList)
            {
                comboBox1.Items.Add(xn.ChildNodes[0].InnerText);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID = comboBox1.Text;
        }
    }
}
