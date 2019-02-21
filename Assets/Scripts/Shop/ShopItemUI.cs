using UnityEngine;
using System.Collections;

/// <summary>
/// 商城（Item）商品控制器
/// </summary>
public class ShopItemUI : MonoBehaviour {

    private Transform m_Transform;

    private UILabel ui_Speed;
    private UILabel ui_Rotate;
    private UILabel ui_Price;
    private GameObject shipParent;//飞机模型父物体
    private GameObject buyButton;//绑定购买事件
    private GameObject itemState;//商品状态

    public int itemPrice;//商品价格
    public int itemId;//商品编号

	void Awake () {
	     m_Transform =gameObject.GetComponent<Transform>();

         ui_Speed = m_Transform.FindChild("Speed/Speed_Num").GetComponent<UILabel>();
         ui_Rotate = m_Transform.FindChild("Rotate/Rotate_Num").GetComponent<UILabel>();
         ui_Price = m_Transform.FindChild("BuyButton/Price").GetComponent<UILabel>();
         shipParent = m_Transform.FindChild("Model").gameObject;
         itemState = m_Transform.FindChild("BuyButton").gameObject;

         buyButton = m_Transform.FindChild("BuyButton/bg").gameObject;
         UIEventListener.Get(buyButton).onClick = BuyButtonClick;

	}

    /// <summary>
    /// 给单个商品的UI赋值
    /// </summary>
    public void SetUIValue( string id,string speed,string rotate,string price,GameObject model,int state)
    {

        //给UI元素赋值
        ui_Speed.text = speed;
        ui_Rotate.text = rotate;
        ui_Price.text = price;

        itemPrice = int.Parse(price);
        itemId = int.Parse(id);

        //判断状态
        if (state ==1)
        {
            itemState.SetActive(false);
        }


        //实例化相关模型，设置相关细节参数
        GameObject ship = NGUITools.AddChild(shipParent, model);
        ship.layer = 8;//设置模型所处的层为NGUI层
        Transform ship_Transform = ship.GetComponent<Transform>();
        ship_Transform.FindChild("Mesh").gameObject.layer = 8;//给子物体设置层
        //设置飞机模型的缩放位置信息
        if (model.name == "Ship_4")
        {
            ship_Transform.localScale = new Vector3(3, 3, 3);//缩放
        }
        else
        {
            ship_Transform.localScale = new Vector3(8, 8, 8);//缩放
        }
        

        ship_Transform.localPosition = new Vector3(0,54,0);//位置
        Vector3 rot = new Vector3(-90, 0, 0);
        ship_Transform.localRotation = Quaternion.Euler(rot);//角度

    }

    /// <summary>
    /// 按下购买按钮
    /// </summary>
    /// <param name="go"></param>
    private void BuyButtonClick(GameObject go)
    {
        
        SendMessageUpwards("CalcItemPrice", this);//("函数名"，参数)
    }

    /// <summary>
    /// 购买成功
    /// </summary>
    public void BuyEnd()
    {
        itemState.SetActive(false);
    }
}
