using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace OrderSystem
{
    /// <summary>
    /// 这个类用于实现登录和初次使用时激活系统
    /// </summary>
    public class LoginClass
    {
        /// <summary>
        /// 创建员工和管理员的信息文件，激活系统
        /// </summary>
        public static void initilze()
        {
            createEmployeeXmlDoc("EmployeeInformation.xml");
            createEmployerXmlDoc("EmployerInformation.xml");
        }

        /// <summary>
        /// 用于检测系统是否已经激活
        /// </summary>
        /// <returns>如果系统已激活，返回true，否则返回false</returns>
        public static bool isInilized()
        {
            XmlDocument testDoc = new XmlDocument();
            bool isInilized = true;
            try
            {
                testDoc.Load("EmployeeInformation.xml");
            }
            catch (Exception)
            {
                isInilized = false;
            }
            return isInilized;

        }


        /// <summary>
        /// 储存登录许可
        /// </summary>
        private static bool isLoginAllowed = false;

        /// <summary>
        /// 用于判断是否允许登录
        /// </summary>
        public static bool IsLoginAllowed   //运用只读属性来作为登录许可，实现数据封装
        {
            get
            {
                return isLoginAllowed;
            }
            //set                                  //设置了set则外部可修改私有字段isLoginAllowed的值
            //{                                    //还可在set中添加验证逻辑if( )，确保设置的值符合特定条件
            //    isLoginAllowed = value;
            //}
        }

        /// <summary>
        /// 进行登录验证，修改isLoginAllowed的值，返回登录提示信息，传出用户ID
        /// </summary>
        /// <param name="xmlPath">登录信息文件的路径</param>
        /// <param name="Username">用户输入的用户名</param>
        /// <param name="Password">用户输入的密码</param>
        /// <param name="ID">若验证通过，传出用户ID</param>
        /// <returns>返回登录提示信息，
        ///          若用户名密码正确，返回“欢迎XXX，您上次登录时间为yyyy-MM-dd HH:mm:ss”
        ///          若用户名不存在，返回“用户名不存在”
        ///          若密码错误，返回“密码错误”
        ///          若账号已停用，返回“您的账号已停用”
        /// </returns>
        public static string strCheck(string xmlPath,string Username,string Password,out string ID)
        {
            string result="该用户名不存在";
            isLoginAllowed = false;
            ID = "";
            //判断是否为管理员
            bool isEmployer;
            if(xmlPath== "EmployerInformation.xml")
            {
                isEmployer = true;
            }
            else
            {
                isEmployer = false;
            }
            //读取登录信息文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList nodeList;
            if (isEmployer)
            {
                nodeList = xmlDoc.SelectNodes("//Employer");
            }
            else
            {
                nodeList = xmlDoc.SelectNodes("//Employee");
            }
            //查找用户名，进行登录验证
            foreach (XmlNode xn in nodeList)
            {
                string u = xn.ChildNodes[2].InnerText;
                string p = xn.ChildNodes[3].InnerText;
                //查找用户名是否存在,若不存在result即为初始值"该用户名不存在"
                if (u == Username)
                {
                    //验证密码是否正确
                    if (p == Password)
                    {
                        //判断用户是否为员工，若是员工判断其账号是否停用
                        if (!isEmployer && xn.ChildNodes[5].InnerText == "否")
                        {
                            result = "您的账号已停用";
                        }
                        //密码正确，获取登录许可，更新登录日志
                        else
                        {
                            result = "欢迎" + xn.ChildNodes[1].InnerText + "，您上次的登录时间为" + xn.ChildNodes[4].InnerText;
                            isLoginAllowed = true;
                            ID = xn.ChildNodes[0].InnerText;
                            xn.ChildNodes[4].InnerText = DateTime.Now.ToString();
                            xmlDoc.Save(xmlPath);
                        }
                    }
                    else
                    {
                        result = "密码错误";
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 创建员工信息xml文件
        /// </summary>
        /// <param name="xmlPath">文件创建路径</param>
        private static void createEmployeeXmlDoc(string xmlPath)
        {
            XElement xe = new XElement(
                new XElement("EmployeeInformation",
                    new XElement("Employee",
                        new XElement("ID","001"),
                        new XElement("Name","正弦"),
                        new XElement("Username","sin"),
                        new XElement("Passward","123"),
                        new XElement("Date","2025-05-20 05:20:20"),
                        new XElement("OnOff","是")
                    ),
                    new XElement("Employee",
                        new XElement("ID", "002"),
                        new XElement("Name", "fei"),
                        new XElement("Username", "fei208"),
                        new XElement("Passward", "321"),
                        new XElement("Date", "2025-05-20 05:20:20"),
                        new XElement("OnOff", "是")
                    ),
                    new XElement("Employee",
                        new XElement("ID", "003"),
                        new XElement("Name", "jia"),
                        new XElement("Username", "jia208"),
                        new XElement("Passward", "321"),
                        new XElement("Date", "2025-05-20 05:20:20"),
                        new XElement("OnOff", "是")
                    )
                )
                );

            //指定编码格式
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            //写入器
            XmlWriter xw = XmlWriter.Create(xmlPath,settings);
            xe.Save(xw);

            xw.Flush();
            xw.Close();
        }

        /// <summary>
        /// 创建员工信息xml文件
        /// </summary>
        /// <param name="xmlPath">文件创建路径</param>
        private static void createEmployerXmlDoc(string xmlPath)
        {
            XElement xe = new XElement(
                new XElement("EmployerInformation",
                    new XElement("Employer",
                        new XElement("ID", "001"),
                        new XElement("Name", "正弦"),
                        new XElement("Username", "sin"),
                        new XElement("Passward", "123"),
                        new XElement("Date", "2025-05-20 05:20:20")
                    ),
                    new XElement("Employer",
                        new XElement("ID", "002"),
                        new XElement("Name", "余弦"),
                        new XElement("Username", "cos"),
                        new XElement("Passward", "321"),
                        new XElement("Date", "2025-05-20 05:20:20")
                    ),
                    new XElement("Employer",
                        new XElement("ID", "003"),
                        new XElement("Name", "正切"),
                        new XElement("Username", "tan"),
                        new XElement("Passward", "456"),
                        new XElement("Date", "2025-05-20 05:20:20")
                    )
                )
                );

            //对写入器配置进行设置
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            //写入器
            XmlWriter xw = XmlWriter.Create(xmlPath, settings);
            xe.Save(xw);

            xw.Flush();
            xw.Close();
        }

    }
}
