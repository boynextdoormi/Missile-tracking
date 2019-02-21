using UnityEngine;
using System.Collections;
using System.Xml;//引入XML操作相关的函数

/// <summary>
/// XML操作演示
/// </summary>

public class XMLDemo : MonoBehaviour {
    
    //定义一个字段存储xml路径
    private string xmlPath = "Assets/Dates/web.xml";

	void Start () {
        ReadXMLByPath(xmlPath);
	}

    /// <summary>
    /// 通过路径读取XML中的数据进行显示
    /// </summary>
    /// <param name="path">XML的路径</param>
    private void ReadXMLByPath(string path)
    { 
       XmlDocument doc =new XmlDocument();//实例化XML对象
       doc.Load(path);//加载XML
       XmlNode root = doc.SelectSingleNode("Web");//获取根节点
       XmlNodeList nodeList = root.ChildNodes;//获取所有的子元素
        foreach(XmlNode node in nodeList)//遍历输出
        {
            int id = int.Parse(node.Attributes["id"].Value);
            string name =node.ChildNodes[0].InnerText;
            string url = node.ChildNodes[1].InnerText;


            Debug.Log(id + "--" + name + "--" + url );
        }
    }
}
