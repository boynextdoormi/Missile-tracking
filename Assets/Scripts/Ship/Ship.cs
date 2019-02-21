 using UnityEngine;
using System.Collections;
/// <summary>
/// 飞机控制器脚本
/// </summary>
public class Ship : MonoBehaviour {
    private Transform m_Transform;
    private MissileManager m_MissileManager;//导弹生成器引用
    private GameObject Smoke03;//爆炸特效
    private GameUIManager m_GameUIManager;

    private int rewardNum = 0;//本局获得多少奖励物品
    private int speed;
    private int rotate;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public int Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    private bool isLeft = false;//左转标识
    private bool isRight = false;//右转标识
    private bool isDeath = false;//角色是否死亡
        public bool IsLeft
        {
            get {return isLeft;}
            set {isLeft =value;}
        }
        public bool IsRight
        {
            get { return isRight; }
            set { isRight = value; }
        }

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        m_GameUIManager = GameObject.Find("UI Root").GetComponent<GameUIManager>();

        Smoke03 = Resources.Load<GameObject>("Smoke03");
	}
	

	void Update () {
        //只有角色活着的时候才能飞行与旋转
        if (isDeath ==false)
        {
            m_Transform.Translate(Vector3.forward *speed);
                    if (isLeft)
                {
                    m_Transform.Rotate(Vector3.up * -1 *rotate);
                }
                if (isRight)
                {
                    m_Transform.Rotate(Vector3.up * 1 *rotate);
                }
        }
	}

    private void OnTriggerEnter(Collider coll)
    {
        //和四周的边缘相撞
        if (coll.tag == "Border")
        {
            isDeath = true;
            m_GameUIManager.ShowOverPanel();
        }
        //飞机与导弹撞
        if (coll.tag == "Missile")
        {
            isDeath = true;//角色挂掉
            GameObject.Instantiate(Smoke03,m_Transform.position,Quaternion.identity);//爆炸特效
            m_MissileManager.StopCreat();//停止生成导弹
            GameObject.Destroy(coll.gameObject);//销毁导弹
            gameObject.SetActive(false);//隐藏自身
            m_GameUIManager.ShowOverPanel();
        }
        //飞机与奖励物品相撞
        if (coll.tag == "Reward")
        {
            rewardNum++;//累加奖励数量 
            GameObject.Destroy(coll.gameObject);//销毁奖励物品
            m_GameUIManager.UpdateStarNum(rewardNum);//同步UI
        }
    }
}
