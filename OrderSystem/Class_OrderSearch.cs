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
    /// 该类用于订单查询的实现
    /// </summary>
    public class OrderSearchClass
    {
        /// <summary>
        /// 订单信息（按菜名查询）
        /// </summary>
        public DataTable table_SearchByName = new DataTable();

        /// <summary>
        /// 按菜名查询订单,将信息存入table_SearchByName
        /// </summary>
        /// <param name="Name">菜名</param>
        public void searchByName(string Name)
        {
            table_SearchByName.Columns.Add("单次");
            table_SearchByName.Columns.Add("菜名");
            table_SearchByName.Columns.Add("单价");
            table_SearchByName.Columns.Add("份数");
            table_SearchByName.Columns.Add("总价");
            table_SearchByName.Columns.Add("点菜员");
            table_SearchByName.Columns.Add("日期");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Bills_Summary.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Once");

            //搜索全部订单
            if (Name == "全部")
            {
                foreach (XmlNode xn in nodeList)
                {
                    DataRow dr = table_SearchByName.NewRow();
                    dr["单次"] = xn.ChildNodes[0].InnerText;
                    dr["菜名"] = xn.ChildNodes[1].InnerText;
                    dr["单价"] = xn.ChildNodes[2].InnerText;
                    dr["份数"] = xn.ChildNodes[3].InnerText;
                    dr["总价"] = xn.ChildNodes[4].InnerText;
                    dr["点菜员"] = xn.ChildNodes[5].InnerText;
                    dr["日期"] = xn.ChildNodes[6].InnerText;
                    table_SearchByName.Rows.Add(dr);
                }
                return;
            }

            //按菜名查找订单
            foreach (XmlNode xn in nodeList)
            {
                if (Name == xn.ChildNodes[1].InnerText)
                {
                    DataRow dr = table_SearchByName.NewRow();
                    dr["单次"] = xn.ChildNodes[0].InnerText;
                    dr["菜名"] = xn.ChildNodes[1].InnerText;
                    dr["单价"] = xn.ChildNodes[2].InnerText;
                    dr["份数"] = xn.ChildNodes[3].InnerText;
                    dr["总价"] = xn.ChildNodes[4].InnerText;
                    dr["点菜员"] = xn.ChildNodes[5].InnerText;
                    dr["日期"] = xn.ChildNodes[6].InnerText;
                    table_SearchByName.Rows.Add(dr);
                }

            }
        }


        /// <summary>
        /// 订单信息（按点菜员查询）
        /// </summary>
        public DataTable table_SearchByEmployee = new DataTable();

        /// <summary>
        /// 按员工ID查询订单,将信息存入table_SearchByEmployee
        /// </summary>
        /// <param name="ID">员工ID</param>
        public void searchBySingleEmployee(string ID)
        {
            table_SearchByEmployee.Columns.Add("单次");
            table_SearchByEmployee.Columns.Add("菜名");
            table_SearchByEmployee.Columns.Add("单价");
            table_SearchByEmployee.Columns.Add("份数");
            table_SearchByEmployee.Columns.Add("总价");
            table_SearchByEmployee.Columns.Add("点菜员");
            table_SearchByEmployee.Columns.Add("日期");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Bills_Summary.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Once");

            //搜索全部订单
            if (ID == "全部")
            {
                foreach (XmlNode xn in nodeList)
                {
                    DataRow dr = table_SearchByEmployee.NewRow();
                    dr["单次"] = xn.ChildNodes[0].InnerText;
                    dr["菜名"] = xn.ChildNodes[1].InnerText;
                    dr["单价"] = xn.ChildNodes[2].InnerText;
                    dr["份数"] = xn.ChildNodes[3].InnerText;
                    dr["总价"] = xn.ChildNodes[4].InnerText;
                    dr["点菜员"] = xn.ChildNodes[5].InnerText;
                    dr["日期"] = xn.ChildNodes[6].InnerText;
                    table_SearchByEmployee.Rows.Add(dr);
                }
                return;
            }

            //按员工查找订单
            foreach (XmlNode xn in nodeList)
            {
                if (ID == xn.ChildNodes[5].InnerText)
                {
                    DataRow dr = table_SearchByEmployee.NewRow();
                    dr["单次"] = xn.ChildNodes[0].InnerText;
                    dr["菜名"] = xn.ChildNodes[1].InnerText;
                    dr["单价"] = xn.ChildNodes[2].InnerText;
                    dr["份数"] = xn.ChildNodes[3].InnerText;
                    dr["总价"] = xn.ChildNodes[4].InnerText;
                    dr["点菜员"] = xn.ChildNodes[5].InnerText;
                    dr["日期"] = xn.ChildNodes[6].InnerText;
                    table_SearchByEmployee.Rows.Add(dr);
                }

            }
        }


        /// <summary>
        /// 订单信息（按日期查询）
        /// </summary>
        public DataTable table_SearchByTime = new DataTable();

        /// <summary>
        ///  按日期查询订单,将信息存入table_SearchByTime
        /// </summary>
        /// <param name="timeStart">起始时间</param>
        /// <param name="timeEnd">终止时间</param>
        public void searchByTime(DateTime timeStart, DateTime timeEnd)
        {
            table_SearchByTime.Columns.Add("单次");
            table_SearchByTime.Columns.Add("菜名");
            table_SearchByTime.Columns.Add("单价");
            table_SearchByTime.Columns.Add("份数");
            table_SearchByTime.Columns.Add("总价");
            table_SearchByTime.Columns.Add("点菜员");
            table_SearchByTime.Columns.Add("日期");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Bills_Summary.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Once");


            //按时间查找订单
            foreach (XmlNode xn in nodeList)
            {
                if (timeStart <= DateTime.Parse(xn.ChildNodes[6].InnerText) && timeEnd >= DateTime.Parse(xn.ChildNodes[6].InnerText))
                {
                    DataRow dr = table_SearchByTime.NewRow();
                    dr["单次"] = xn.ChildNodes[0].InnerText;
                    dr["菜名"] = xn.ChildNodes[1].InnerText;
                    dr["单价"] = xn.ChildNodes[2].InnerText;
                    dr["份数"] = xn.ChildNodes[3].InnerText;
                    dr["总价"] = xn.ChildNodes[4].InnerText;
                    dr["点菜员"] = xn.ChildNodes[5].InnerText;
                    dr["日期"] = xn.ChildNodes[6].InnerText;
                    table_SearchByTime.Rows.Add(dr);
                }

            }
        }


        //////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 汇总信息（按菜名汇总）
        /// </summary>
        public DataTable table_Name_Summary = new DataTable();


        /// <summary>
        /// 按菜名汇总订单,将信息存入table_Name_Summary
        /// </summary>
        public void searchByName_Summary()
        {
            string Name;
            int Number;
            int Sum;
            bool isDishAdded;

            table_Name_Summary.Columns.Add("菜名");
            table_Name_Summary.Columns.Add("份数");
            table_Name_Summary.Columns.Add("总价");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Bills_Summary.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Once");


            foreach (XmlNode xn in nodeList)
            {
                Name = xn.ChildNodes[1].InnerText;
                Number = int.Parse(xn.ChildNodes[3].InnerText);
                Sum = int.Parse(xn.ChildNodes[4].InnerText);
                isDishAdded = false;

                foreach (DataRow dr in table_Name_Summary.Rows)
                {
                    if (dr["菜名"].ToString() == Name)
                    {
                        dr["份数"] = (Convert.ToInt16(dr["份数"]) + Number).ToString();
                        dr["总价"] = (Sum + Convert.ToUInt16(dr["总价"])).ToString();

                        isDishAdded = true;
                    }
                }
                if (!isDishAdded)
                {
                    DataRow dr = table_Name_Summary.NewRow();
                    dr["菜名"] = Name;
                    dr["份数"] = Number.ToString();
                    dr["总价"] = Sum.ToString();
                    table_Name_Summary.Rows.Add(dr);
                }
            }


        }


        /// <summary>
        /// 汇总信息（按日期汇总）
        /// </summary>
        public DataTable table_Time_Summary = new DataTable();


        /// <summary>
        /// 按日期汇总订单,将信息存入table_Time_Summary
        /// </summary>
        public void searchByTime_Summary()
        {
            string Date;
            int Number;
            int Sum;
            bool isDishAdded;

            table_Time_Summary.Columns.Add("日期");
            table_Time_Summary.Columns.Add("份数");
            table_Time_Summary.Columns.Add("总价");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Bills_Summary.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Once");


            foreach (XmlNode xn in nodeList)
            {
                Date = xn.ChildNodes[6].InnerText;
                Number = int.Parse(xn.ChildNodes[3].InnerText);
                Sum = int.Parse(xn.ChildNodes[4].InnerText);
                isDishAdded = false;

                foreach (DataRow dr in table_Time_Summary.Rows)
                {
                    if (dr["日期"].ToString() == Date)
                    {
                        dr["份数"] = (Convert.ToInt16(dr["份数"]) + Number).ToString();
                        dr["总价"] = (Sum + Convert.ToUInt16(dr["总价"])).ToString();

                        isDishAdded = true;
                    }
                }
                if (!isDishAdded)
                {
                    DataRow dr = table_Time_Summary.NewRow();
                    dr["日期"] = Date;
                    dr["份数"] = Number.ToString();
                    dr["总价"] = Sum.ToString();
                    table_Time_Summary.Rows.Add(dr);
                }
            }


        }

        /// <summary>
        /// 汇总信息（按点菜员汇总）
        /// </summary>
        public DataTable table_Employee_Summary = new DataTable();


        /// <summary>
        /// 按点菜员汇总订单,将信息存入table_Name_Summary
        /// </summary>
        public void searchByEmployee_Summary()
        {
            string Employee;
            int Number;
            int Sum;
            bool isDishAdded;

            table_Employee_Summary.Columns.Add("点菜员");
            table_Employee_Summary.Columns.Add("份数");
            table_Employee_Summary.Columns.Add("总价");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Bills_Summary.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Once");


            foreach (XmlNode xn in nodeList)
            {
                Employee = xn.ChildNodes[5].InnerText;
                Number = int.Parse(xn.ChildNodes[3].InnerText);
                Sum = int.Parse(xn.ChildNodes[4].InnerText);
                isDishAdded = false;

                foreach (DataRow dr in table_Employee_Summary.Rows)
                {
                    if (dr["点菜员"].ToString() == Employee)
                    {
                        dr["份数"] = (Convert.ToInt16(dr["份数"]) + Number).ToString();
                        dr["总价"] = (Sum + Convert.ToUInt16(dr["总价"])).ToString();

                        isDishAdded = true;
                    }
                }
                if (!isDishAdded)
                {
                    DataRow dr = table_Employee_Summary.NewRow();
                    dr["点菜员"] = Employee;
                    dr["份数"] = Number.ToString();
                    dr["总价"] = Sum.ToString();
                    table_Employee_Summary.Rows.Add(dr);
                }
            }


        }





    }
}
