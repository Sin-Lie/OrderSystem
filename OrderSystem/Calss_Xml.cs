using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace OrderSystem
{
    /// <summary>
    /// 该类用于实现对xml文件的信息进行新增、查找、修改操作
    /// </summary>
    public class XmlClass
    {

        /// <summary>
        /// 获取文件信息，一次只能获取一条信息，如果返回值为空，说明信息不存在
        /// </summary>
        /// <param name="xmlPath">文件地址</param>
        /// <param name="locationTagName">用于定位的标签名</param>
        /// <param name="locationInnerText">用于定位的标签的内部信息</param>
        /// <param name="detailLocationTagName">需获取的信息的标签名</param>
        /// <returns>以字符串形式，返回获取的信息</returns>
        public static string getInfo(string xmlPath, string locationTagName, string locationInnerText, string detailLocationTagName)
        {
            string result = "";
            string childnodeName = "";
            string rootName = "";
            getNodeName(xmlPath, out childnodeName, out rootName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList nodeList = xmlDoc.SelectNodes(childnodeName);

            foreach (XmlNode xn in nodeList)
            {
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    if (locationTagName == xn.ChildNodes[i].Name.ToString() && locationInnerText == xn.ChildNodes[i].InnerText.ToString())
                    {
                        for (int j = 0; j < xn.ChildNodes.Count; j++)
                        {
                            if (detailLocationTagName == xn.ChildNodes[j].Name.ToString())
                            {
                                result = xn.ChildNodes[j].InnerText.ToString();
                                break;
                            }
                        }
                    }
                    if (result != "") { break; }
                }
                if (result != "") { break; }
            }

            return result;
        }


        /// <summary>
        /// 修改文件信息
        /// </summary>
        /// <param name="xmlPath">文件地址</param>
        /// <param name="locationTagName">用于定位的标签名</param>
        /// <param name="locationInnerText">用于定位的内部信息</param>
        /// <param name="detailLocationTagName">需修改的信息的标签名</param>
        /// <param name="newInfo">修改后信息</param>
        public static void modifyInfo(string xmlPath, string locationTagName, string locationInnerText, string detailLocationTagName, string newInfo)
        {
            string childnodeName = "";
            string rootName = "";
            getNodeName(xmlPath, out childnodeName, out rootName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList nodeList = xmlDoc.SelectNodes(childnodeName);

            foreach (XmlNode xn in nodeList)
            {
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    if (locationTagName == xn.ChildNodes[i].Name.ToString() && locationInnerText == xn.ChildNodes[i].InnerText.ToString())
                    {
                        for (int j = 0; j < xn.ChildNodes.Count; j++)
                        {
                            if (detailLocationTagName == xn.ChildNodes[j].Name.ToString())
                            {
                                xn.ChildNodes[j].InnerText = newInfo;
                            }
                        }
                    }
                }
            }
            xmlDoc.Save(xmlPath);
        }


        /// <summary>
        /// 指示待添加信息的唯一标识是否已经存在
        /// </summary>
        public static bool IdExists = false;


        /// <summary>
        /// //添加员工
        /// </summary>
        /// <param name="xmlPath">员工信息文件地址</param>
        /// <param name="Name">员工姓名</param>
        /// <param name="Username">用户名</param>
        /// <param name="Passward">密码</param>
        public static void addInfo(string xmlPath, string Name, string Username, string Passward)
        {
            string childnodeName = "";
            string rootName = "";
            getNodeName(xmlPath, out childnodeName, out rootName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode root = xmlDoc.SelectSingleNode(rootName);
            XmlNodeList nodeList = xmlDoc.SelectNodes(childnodeName);

            //检测用户名是否存在
            foreach (XmlNode xn in nodeList)
            {
                if (Username == xn.ChildNodes[2].InnerText)
                {
                    IdExists = true;
                    return;
                }
            }
            IdExists = false;

            //获取可用ID
            string ID = getAvailableId("EmployeeID");

            //创建节点
            XmlElement xe = xmlDoc.CreateElement("Employee");
            XmlElement xeSub1 = xmlDoc.CreateElement("ID");
            xeSub1.InnerText = ID;
            XmlElement xeSub2 = xmlDoc.CreateElement("Name");
            xeSub2.InnerText = Name;
            XmlElement xeSub3 = xmlDoc.CreateElement("Username");
            xeSub3.InnerText = Username;
            XmlElement xeSub4 = xmlDoc.CreateElement("Passward");
            xeSub4.InnerText = Passward;
            XmlElement xeSub5 = xmlDoc.CreateElement("Date");
            xeSub5.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            XmlElement xeSub6 = xmlDoc.CreateElement("OnOff");
            xeSub6.InnerText = "是";

            //拼接xe节点
            xe.AppendChild(xeSub1);
            xe.AppendChild(xeSub2);
            xe.AppendChild(xeSub3);
            xe.AppendChild(xeSub4);
            xe.AppendChild(xeSub5);
            xe.AppendChild(xeSub6);
            //将xe节点拼接进入根节点
            root.AppendChild(xe);

            //保存
            xmlDoc.Save(xmlPath);
        }


        /// <summary>
        /// 添加菜品
        /// </summary>
        /// <param name="Name">菜名</param>
        /// <param name="Price">单价</param>
        /// <param name="Type">菜品种类</param>
        public static void addInfo(string Name, string Price, string Type)
        {
            string childnodeName = "";
            string rootName = "";
            string xmlPath = "";

            //获取存储路径
            if (Type == "荤菜类")
            {
                xmlPath = "Dishes_Meat.XML";
            }
            else if (Type == "蔬菜类")
            {
                xmlPath = "Dishes_Vegetables.XML";
            }
            else if (Type == "汤类")
            {
                xmlPath = "Dishes_Soups.XML";
            }


            getNodeName(xmlPath, out childnodeName, out rootName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode root = xmlDoc.SelectSingleNode(rootName);
            XmlNodeList nodeList = xmlDoc.SelectNodes(childnodeName);

            //检测菜名是否存在
            foreach (XmlNode xn in nodeList)
            {
                if (Name == xn.ChildNodes[0].InnerText)
                {
                    IdExists = true;
                    return;
                }
            }

            //添加菜品至总菜品
            addInfo(Name, Price);
            //检测菜名在总菜品中是否存在
            if (IdExists) { return; }

            IdExists = false;

            //创建节点
            XmlElement xe = xmlDoc.CreateElement("Dish");
            XmlElement xeSub1 = xmlDoc.CreateElement("Name");
            xeSub1.InnerText = Name;
            XmlElement xeSub2 = xmlDoc.CreateElement("Price");
            xeSub2.InnerText = Price;
            XmlElement xeSub3 = xmlDoc.CreateElement("OnOff");
            xeSub3.InnerText = "是";

            //拼接xe节点
            xe.AppendChild(xeSub1);
            xe.AppendChild(xeSub2);
            xe.AppendChild(xeSub3);

            //将xe节点拼接进入根节点
            root.AppendChild(xe);

            

            //保存菜品至分类菜品
            xmlDoc.Save(xmlPath);
        }


        /// <summary>
        /// 添加菜品至总菜品
        /// </summary>
        /// <param name="Name">菜名</param>
        /// <param name="Price">单价</param>
        private static void addInfo(string Name, string Price)
        {
            string childnodeName = "";
            string rootName = "";
            string xmlPath = "AllDishes.XML";

            getNodeName(xmlPath, out childnodeName, out rootName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode root = xmlDoc.SelectSingleNode(rootName);
            XmlNodeList nodeList = xmlDoc.SelectNodes(childnodeName);

            //检测菜名是否存在
            foreach (XmlNode xn in nodeList)
            {
                if (Name == xn.ChildNodes[0].InnerText)
                {
                    IdExists = true;
                    return;
                }
            }
            IdExists = false;

            //创建节点
            XmlElement xe = xmlDoc.CreateElement("Dish");
            XmlElement xeSub1 = xmlDoc.CreateElement("Name");
            xeSub1.InnerText = Name;
            XmlElement xeSub2 = xmlDoc.CreateElement("Price");
            xeSub2.InnerText = Price;
            XmlElement xeSub3 = xmlDoc.CreateElement("OnOff");
            xeSub3.InnerText = "是";

            //拼接xe节点
            xe.AppendChild(xeSub1);
            xe.AppendChild(xeSub2);
            xe.AppendChild(xeSub3);

            //将xe节点拼接进入根节点
            root.AppendChild(xe);

            //保存
            xmlDoc.Save(xmlPath);
        }


        /// <summary>
        /// 添加账单
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="Number"></param>
        /// <param name="Sum"></param>
        /// <param name="Employee"></param>
        public static void addInfo(string ID, string Name, string Price, string Number, string Sum, string Employee)
        {
            string childnodeName = "";
            string rootName = "";
            string xmlPath = "Bills_Summary.XML";
            getNodeName(xmlPath, out childnodeName, out rootName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode root = xmlDoc.SelectSingleNode(rootName);
            XmlNodeList nodeList = xmlDoc.SelectNodes(childnodeName);

            //创建节点
            XmlElement xe = xmlDoc.CreateElement("Once");
            XmlElement xeSub1 = xmlDoc.CreateElement("ID");
            xeSub1.InnerText = ID;
            XmlElement xeSub2 = xmlDoc.CreateElement("Name");
            xeSub2.InnerText = Name;
            XmlElement xeSub3 = xmlDoc.CreateElement("Price");
            xeSub3.InnerText = Price;
            XmlElement xeSub4 = xmlDoc.CreateElement("Number");
            xeSub4.InnerText = Number;
            XmlElement xeSub5 = xmlDoc.CreateElement("Sum");
            xeSub5.InnerText = Sum;
            XmlElement xeSub6 = xmlDoc.CreateElement("Employee");
            xeSub6.InnerText = Employee;
            XmlElement xeSub7 = xmlDoc.CreateElement("Date");
            xeSub7.InnerText = DateTime.Now.ToString("yyyy-MM-dd");

            //拼接xe节点
            xe.AppendChild(xeSub1);
            xe.AppendChild(xeSub2);
            xe.AppendChild(xeSub3);
            xe.AppendChild(xeSub4);
            xe.AppendChild(xeSub5);
            xe.AppendChild(xeSub6);
            xe.AppendChild(xeSub7);
            //将xe节点拼接进入根节点
            root.AppendChild(xe);

            //保存
            xmlDoc.Save(xmlPath);
        }



        /// <summary>
        /// 根据文件路径查找根节点和子节点
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="childnodeName">输出根节点的孩子节点</param>
        /// <param name="rootName">输出根节点</param>
        private static void getNodeName(string xmlPath, out string childnodeName, out string rootName)
        {
            childnodeName = "";
            rootName = "";
            if (xmlPath == "EmployeeInformation.xml")
            {
                childnodeName = "//Employee";
                rootName = "EmployeeInformation";
            }
            else if (xmlPath == "EmployerInformation.xml")
            {
                childnodeName = "//Employer";
                rootName = "EmployerInformation";
            }
            else if (xmlPath == "Dishes_Meat.XML")
            {
                childnodeName = "//Dish";
                rootName = "Menu";
            }
            else if (xmlPath == "Dishes_Vegetables.XML")
            {
                childnodeName = "//Dish";
                rootName = "Menu";
            }
            else if (xmlPath == "Dishes_Soups.XML")
            {
                childnodeName = "//Dish";
                rootName = "Menu";
            }
            else if (xmlPath == "Bills_Summary.XML")
            {
                childnodeName = "//Once";
                rootName = "Order";
            }
            else if (xmlPath == "AllDishes.XML")
            {
                childnodeName = "//Dish";
                rootName = "Menu";
            }
        }      


        /// <summary>
        /// 获取可用的员工号或点单单次，并更新可用ID
        /// </summary>
        /// <param name="tagName">需获取信息的标签名</param>
        /// <returns>返回可用的员工号或点单单次</returns>
        public static string getAvailableId(string tagName)
        {
            string result = "";
            int i;

            //获取可用ID
            i = (tagName == "OrderID") ? 0 : (tagName == "EmployeeID") ? 1 : 2;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("IdBase.xml");
            XmlNode xn = xmlDoc.SelectSingleNode("ID");
            result = xn.ChildNodes[i].InnerText;

            //更新可用ID
            if (tagName == "OrderID")
            {
                xn.ChildNodes[i].InnerText = (int.Parse(result) + 1).ToString("D6");
            }
            else if (tagName == "EmployeeID" || tagName == "EmployerID")
            {
                xn.ChildNodes[i].InnerText = (int.Parse(result) + 1).ToString("D3");
            }
            xmlDoc.Save("IdBase.xml");

            return result;
        }


        /// <summary>
        /// 读取可用ID （只读）
        /// </summary>
        /// <param name="tagName">需获取信息的标签名</param>
        /// <returns>返回可用的员工号或点单单次</returns>
        public static string readAvailableId(string tagName)
        {
            string result = "";
            int i;

            //获取可用ID
            i = (tagName == "OrderID") ? 0 : (tagName == "EmployeeID") ? 1 : 2;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("IdBase.xml");
            XmlNode xn = xmlDoc.SelectSingleNode("ID");
            result = xn.ChildNodes[i].InnerText;

            return result;
        }


    }
}
