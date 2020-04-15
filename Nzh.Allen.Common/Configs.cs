using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Nzh.Allen.Common
{
    public class Configs
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            string fileDir = Path.GetDirectoryName(filePath);
            string systemConfigPath = Path.Combine(fileDir, "Configs/system.config");
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load(systemConfigPath);
            System.Xml.XmlNode xNode;
            System.Xml.XmlElement xElem1;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            return xElem1.GetAttribute("value");
        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value)
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            string fileDir = Path.GetDirectoryName(filePath);
            string systemConfigPath = Path.Combine(fileDir, "Configs/system.config");
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load(systemConfigPath);
            System.Xml.XmlNode xNode;
            System.Xml.XmlElement xElem1;
            System.Xml.XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", value);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(systemConfigPath);
        }
    }
}
