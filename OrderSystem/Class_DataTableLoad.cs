using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OrderSystem
{
    /// <summary>
    /// 加载DaTable，用于信息检索
    /// </summary>
    public class DataTableLoadClass
    {
        /// <summary>
        /// 所有员工信息
        /// </summary>
        public DataTable table_EmployeeAll = new DataTable();

        /// <summary>
        /// 加载所有员工信息
        /// </summary>
        public void tableLoad_EmployeeAll()
        {
            table_EmployeeAll.Columns.Add("员工号");
            table_EmployeeAll.Columns.Add("姓名");
            table_EmployeeAll.Columns.Add("用户名");
            table_EmployeeAll.Columns.Add("是否可用");
            table_EmployeeAll.Columns.Add("最后一次登录时间");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("EmployeeInformation.xml");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Employee");

            foreach (XmlNode xn in nodeList)
            {
                DataRow dr = table_EmployeeAll.NewRow();

                dr["员工号"] = xn.ChildNodes[0].InnerText;
                dr["姓名"] = xn.ChildNodes[1].InnerText;
                dr["用户名"] = xn.ChildNodes[2].InnerText;
                dr["是否可用"] = xn.ChildNodes[5].InnerText;
                dr["最后一次登录时间"] = xn.ChildNodes[4].InnerText;
                table_EmployeeAll.Rows.Add(dr);
            }
        }


        /// <summary>
        /// 已停用员工信息
        /// </summary>
        public DataTable table_EmployeeOff = new DataTable();

        /// <summary>
        /// 加载已停用员工信息
        /// </summary>
        public void tableLoad_EmployeeOff()
        {
            table_EmployeeOff.Columns.Add("员工号");
            table_EmployeeOff.Columns.Add("姓名");
            table_EmployeeOff.Columns.Add("用户名");
            table_EmployeeOff.Columns.Add("是否可用");
            table_EmployeeOff.Columns.Add("最后一次登录时间");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("EmployeeInformation.xml");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Employee");

            foreach (XmlNode xn in nodeList)
            {
                if (xn.ChildNodes[5].InnerText == "否")
                {
                    DataRow dr = table_EmployeeOff.NewRow();

                    dr["员工号"] = xn.ChildNodes[0].InnerText;
                    dr["姓名"] = xn.ChildNodes[1].InnerText;
                    dr["用户名"] = xn.ChildNodes[2].InnerText;
                    dr["是否可用"] = xn.ChildNodes[5].InnerText;
                    dr["最后一次登录时间"] = xn.ChildNodes[4].InnerText;
                    table_EmployeeOff.Rows.Add(dr);
                }
            }
        }


        /// <summary>
        /// 菜品信息（特定菜品种类）
        /// </summary>
        public DataTable table_Dish = new DataTable();

        /// <summary>
        /// 获取指定菜品种类的菜品信息
        /// </summary>
        /// <param name="dishType">菜品种类</param>
        public void tableLoad_Dish(string dishType)
        {
            table_Dish.Columns.Add("菜名");
            table_Dish.Columns.Add("单价");
            table_Dish.Columns.Add("是否可用");
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load($"Dishes_{dishType}.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Dish");

            foreach (XmlNode xn in nodeList)
            {
                DataRow dr = table_Dish.NewRow();

                dr["菜名"] = xn.ChildNodes[0].InnerText;
                dr["单价"] = xn.ChildNodes[1].InnerText;
                dr["是否可用"] = xn.ChildNodes[2].InnerText;
                table_Dish.Rows.Add(dr);
            }
        }


    }
}
