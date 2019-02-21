using UnityEngine;
using System.Collections;

/// <summary>
/// 奖励物品管理
/// </summary>
public class RewardManager : MonoBehaviour {
    private Transform m_Transform;
    private GameObject prefab_reward;//奖励物品预制体

    private int rewardCount = 0;//当前场景中存在多少奖励物品
    private int rewardMaxCount = 3;//当前场景中最多存在多少奖励物品

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        prefab_reward = Resources.Load<GameObject>("reward");
        InvokeRepeating("CreatReward",5.0f,5.0f);//调用生成奖励物品
	}


    /// <summary>
    /// 生成奖励物品
    /// </summary>
    private void CreatReward()
    {
        //存在的奖励物品不能超过最大值
        if (rewardCount < rewardMaxCount)
        {
            //导弹生成的范围
            Vector3 pos = new Vector3(Random.Range(-630, 530), 0, Random.Range(-330, 630));
            GameObject.Instantiate(prefab_reward,pos,Quaternion.identity,m_Transform);//父物体重载形式
            rewardCount++;
        }
    }

    /// <summary>
    /// 停止奖励物品的生成
    /// </summary>
    public void StopCreat()
    {
        CancelInvoke();
    }

    /// <summary>
    /// 当前的奖励物品数量--
    /// </summary>
    public void RewardCountDown()
    {
        rewardCount--;
        Debug.Log("我可以生成新的");
    }
}
