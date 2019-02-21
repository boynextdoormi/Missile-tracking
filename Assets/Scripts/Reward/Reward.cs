using UnityEngine;
using System.Collections;

/// <summary>
/// 奖励物品控制
/// </summary>
public class Reward : MonoBehaviour {
    private Transform m_Transform;
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
	}

	void Update () {
        m_Transform.Rotate(Vector3.left);
	}

    void OnDestroy()
    {
        Debug.Log("我销毁了");
        SendMessageUpwards("RewardCountDown");//给父体物体发送消息
    }

}
