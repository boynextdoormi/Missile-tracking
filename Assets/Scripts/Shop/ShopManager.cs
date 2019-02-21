using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;//文件操作相关API


/// <summary>
/// 商城模块总控制器
/// </summary>
public class ShopManager : MonoBehaviour {
    //商城数据对象
    private ShopData shopData;
    //XML路径
    //private string xmlPath = "Assets/Datas/ShopData.xml";
    private string xmlPath;

    //private string savePath = "Assets/Datas/SaveData.xml";
    private string savePath;
    private string content = "<SaveData><GoldCount>10000</GoldCount><HeightScore>97</HeightScore><ID0>1</ID0><ID1>0</ID1><ID2>0</ID2><ID3>0</ID3></SaveData>";

    //商城商品模板
    private GameObject ui_ShopItem;
    //左右按钮
    private GameObject LeftButton;
    private GameObject RightButton;
    //商城商品UI集合
    private List<GameObject> shopUI =new List<GameObject>();
    
    //界面UI
    private UILabel starNum;//结束面板星数
    private UILabel scoreNum;//结束面板分数
    

    //引用持有
    private StartUIManager m_StartUIManager;

    //要展现的物品索引
    private int index = 0;

	void Start () {
        savePath = Application.persistentDataPath + "/SaveData.xml";
        xmlPath = Resources.Load("ShopData").ToString();//静态文件只能读取
        if (!File.Exists(savePath))//第一次运行时写入
        {
            File.WriteAllText(savePath,content);
        }


	    //实例化商城数据对象
        shopData =new ShopData();
        //加载XML
        shopData.ReadXmlByPath(xmlPath);
        shopData.ReadScoreAndGold(savePath);

        Debug.Log(shopData.goldCount);
        Debug.Log(shopData.heightScore);

        for (int i = 0; i < shopData.shopState.Count; i++)
        {
            Debug.Log(shopData.shopState[i]);
        }

        ui_ShopItem = Resources.Load<GameObject>("UI/ShopItem");
        m_StartUIManager = GameObject.Find("UI Root").GetComponent<StartUIManager>();

        LeftButton = GameObject.Find("LeftButton");
        RightButton = GameObject.Find("RightButton");
        UIEventListener.Get(LeftButton).onClick = LeftButtonClick;
        UIEventListener.Get(RightButton).onClick = RightButtonClick;

        //同步UI与XML数据
        starNum = GameObject.Find("Star/StarNum").GetComponent<UILabel>();
        scoreNum = GameObject.Find("Score/ScoreNum").GetComponent<UILabel>();

        //读取PlayerPrefabs中的最高分数
        int tempHeightScore = PlayerPrefs.GetInt("HeightScore",0);
        if (tempHeightScore > shopData.heightScore)
        {
            //更新UI
            UpdateUIHeightScore(tempHeightScore);
            //更新XML,存储新的最高分
            shopData.UpdateXMLData(savePath, "HeightScore", tempHeightScore.ToString());
            //清空PlayerPrefs
            PlayerPrefs.SetInt("HeightScore", 0);
        }
        else
        {
            //更新UI
            UpdateUIHeightScore(shopData.heightScore);
        }

        //获取金币数量
        int tempGold = PlayerPrefs.GetInt("GoldNum",0);
        if (tempGold > 0)
        {
            int gold= tempGold + shopData.goldCount;
            Debug.Log("gold :"+gold);
            //更新UI
            UpdateUIGold(gold);
            //更新XML,存储新的最高分
            shopData.UpdateXMLData(savePath, "GoldCount", gold.ToString());
            //清空PlayerPrefs
            PlayerPrefs.SetInt("GoldNum",0);
        }
        else
        {
            UpdateUIGold(shopData.goldCount);
        }

        SetPlayerInfo(shopData.shopList[0]);//默认的模型

        CreatAllShopUI();
	}


    /// <summary>
    /// 更新最高分UI显示
    /// </summary>
    private void UpdateUIHeightScore(int heightScore)
    {
        scoreNum.text = heightScore.ToString();
    }

    /// <summary>
    /// 更新金币UI显示
    /// </summary>
    private void UpdateUIGold(int gold)
    {
        starNum.text = gold.ToString();//结束面板
    }
   

    /// <summary>
    /// 更新UI显示
    /// </summary>
    private void UpdateUI()
    {
        starNum.text = shopData.goldCount.ToString();
        scoreNum.text = shopData.heightScore.ToString();
    }

    /// <summary>
    ///创建商城模块中所有的商品
    /// </summary>
    private void CreatAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            //实例化单个商品UI
            GameObject item = NGUITools.AddChild(gameObject ,ui_ShopItem);
            //加载对应的飞机模型
            GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);

            //给商品UI赋值
            item.GetComponent<ShopItemUI>().SetUIValue(shopData.shopList[i].Id,shopData.shopList[i].Speed, shopData.shopList[i].Rotate, shopData.shopList[i].Price, ship,shopData.shopState[i]);
            shopUI.Add(item);//将生成的UI添加到集合中
            //隐藏UI
            ShopUIHideAndShow(index);
        }
    }


    /// <summary>
    /// 左侧按钮点击事件
    /// </summary>
    private void LeftButtonClick(GameObject go)
    {
        if (index >0)
        {
            index--;
            int temp = shopData.shopState[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
        }
        
    }

    /// <summary>
    /// 右侧按钮点击事件
    /// </summary>
    private void RightButtonClick(GameObject go)
    {
        if (index <shopUI.Count -1)
        {
            index ++;
            int temp =shopData.shopState[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index]);
            ShopUIHideAndShow(index);
        }
    }


    /// <summary>
    /// 商城UI的显示与隐藏
    /// </summary>
    private void ShopUIHideAndShow(int index)
    {
            for (int i = 0; i < shopUI.Count; i++)
			        {
			         shopUI[i].SetActive(false);
			        }
            shopUI[index].SetActive(true);
    }

    /// <summary>
    /// 计算商品价格
    /// </summary>
    private void CalcItemPrice(ShopItemUI item)
    {
        if (shopData.goldCount >= item.itemPrice)
        {
            Debug.Log("购买成功");
            //隐藏购买UI按钮
            item.BuyEnd();
            //减去所用的金币，更新UI
            shopData.goldCount -= item.itemPrice;
            UpdateUI();
            //更新XML中金币数量
            shopData.UpdateXMLData(savePath, "GoldCount", shopData.goldCount.ToString());
            //更新XML中商品状态
            shopData.UpdateXMLData(savePath, "ID"+item.itemId,"1");
            
        }
        else
        {
            Debug.Log("购买失败");
        }
    }

    /// <summary>
    /// 存储角色信息到PlayerPrefabs中去
    /// </summary>
    private void SetPlayerInfo(ShopItem item)
    {
        PlayerPrefs.SetString("PlayerName",item.Model);
        PlayerPrefs.SetInt("PlayerSpeed", int.Parse(item.Speed));
        PlayerPrefs.SetInt("PlayerRotate", int.Parse(item.Rotate));
    }
   
}

