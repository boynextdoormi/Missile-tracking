using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;


/// <summary>
/// 商城功能模块数据操作
/// </summary>
public class ShopData {

    //定义一个存储XML数据的实体集合
    public  List<ShopItem> shopList =new List<ShopItem>();

    //商品购买状态集合
    public List<int> shopState = new List<int>();

    //金币数
    public int goldCount = 0;
    //最高分数
    public int heightScore = 0;

    /// <summary>
    /// 通过指定的路径读取XML文档
    /// </summary>
    /// <param name="path"></param>
    public void ReadXmlByPath(string path)
    {
        XmlDocument doc =new XmlDocument();
        //doc.Load(path);//从路径读取
        doc.LoadXml(path);//从文档中读取
        XmlNode root =doc.SelectSingleNode("Shop");
        XmlNodeList nodeList =root.ChildNodes;
        foreach(XmlNode node in nodeList)
        {
            string speed = node.ChildNodes[0].InnerText;
            string rotate = node.ChildNodes[1].InnerText;
            string model = node.ChildNodes[2].InnerText;
            string price = node.ChildNodes[3].InnerText;
            string id = node.ChildNodes[4].InnerText;
            //遍历完毕后直接打印输出
            //string info =string.Format("speed:{0},rotate:{1},model:{2},price:{3}",speed,rotate,model,price);
            //Debug.Log(info);

            //遍历完毕后，直接存储到LIst集合中
            ShopItem item = new ShopItem(id,speed,rotate,model,price);
            shopList.Add(item);
        }
    }

    /// <summary>
    /// 读取金币和最高分数
    /// </summary>
    public void ReadScoreAndGold(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        goldCount = int.Parse(nodeList[0].InnerText);
        heightScore = int.Parse(nodeList[1].InnerText);
        //读取商品状态
        for (int i = 2; i < 6; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }

    }

    /// <summary>
    /// 更新XML数据
    /// </summary>
    /// <param name="path"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void UpdateXMLData(string path,string key,string value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            if (node.Name  == key)
            {
                node.InnerText = value;
                doc.Save(path);
            }
        }
         
    }

}
