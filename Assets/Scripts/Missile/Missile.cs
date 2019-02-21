using UnityEngine;
using System.Collections;

/// <summary>
/// 导弹控制脚本
/// </summary>
public class Missile : MonoBehaviour {
    private Transform m_Transform;
    private Transform player_Transform;
    private GameObject Smoke03;//爆炸特效
    //导弹默认的前方
    private Vector3 normalForward =Vector3.forward;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Smoke03 = Resources.Load<GameObject>("Smoke03");
	}

	void Update () {
        m_Transform.Translate(Vector3.forward);

        //导弹追踪角色-------------------------------------------------
        //计算方向（导弹与角色之间的向量）
        Vector3 dir = player_Transform.position - m_Transform.position;
        //插值计算导弹要旋转的角度
        normalForward = Vector3.Lerp(normalForward,dir ,Time.deltaTime);
        //改变导弹的方向
        m_Transform.rotation = Quaternion.LookRotation(normalForward);

	}

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag =="Missile")//导弹与导弹相撞
        {
            GameObject.Destroy(gameObject);//相撞后摧毁自身
            GameObject.Instantiate(Smoke03, m_Transform.position, Quaternion.identity);//爆炸特效
        }
    }
}
