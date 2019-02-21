﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 飞机管理器脚本
/// </summary>
public class ShipManager : MonoBehaviour {

    private GameObject model;
    private GameObject player;
	
	void Awake() {
        //读取角色信息
        string ship = PlayerPrefs.GetString("PlayerName");
        int speed = PlayerPrefs.GetInt("PlayerSpeed");
        int rotate = PlayerPrefs.GetInt("PlayerRotate");

        //动态加载模型，实例化角色
        model= Resources.Load<GameObject>(ship);
        player = GameObject.Instantiate(model,Vector3.zero,Quaternion.identity) as GameObject;


        //给角色添加组件，设置属性
        Ship myShip = player.AddComponent<Ship>();
        myShip.Speed = speed;
        myShip.Rotate = rotate;

        player.tag = "Player";
        Debug.Log("标签设置");
        player.GetComponent<Transform>().localScale =new Vector3(2,2,2);
	}
	

}
