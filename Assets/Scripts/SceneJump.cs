using UnityEngine;
using UnityEngine.SceneManagement;//场景管理器
using System.Collections;

public class SceneJump : MonoBehaviour {

	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("02");
        }
	}
}
