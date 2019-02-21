using UnityEngine;
using System.Collections;


/// <summary>
/// 导弹生成器
/// </summary>
public class MissileManager : MonoBehaviour {
        private Transform m_Transform;
        private Transform[] creatPoints;//导弹产生点数组
        private GameObject prefab_Missile3;

	void Start () {
	    m_Transform =gameObject.GetComponent<Transform>();
        prefab_Missile3 = Resources.Load<GameObject>("Missile_3");
        creatPoints = GameObject.Find("CreatPointer").GetComponent<Transform>().GetComponentsInChildren<Transform>();

        //调用生成器
        InvokeRepeating("CreatMissile", 2.0f,5.0f);
	}
	
    /// <summary>
    /// 生成导弹的方法
    /// </summary>
	private void CreatMissile () 
    {
        int index = Random.Range(0,creatPoints.Length);//随机在四个生成点生成导弹
        GameObject.Instantiate(prefab_Missile3, creatPoints[index].position, Quaternion.identity, m_Transform);
	}

    /// <summary>
    /// 停止导弹生成器
    /// </summary>
    public void StopCreat()
    {
        CancelInvoke();//不写参数默认停止脚本中所有的Invoke 
    }
}
