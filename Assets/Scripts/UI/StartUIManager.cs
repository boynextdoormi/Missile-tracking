using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour {
    private GameObject m_StartPanel;//开始面板
    private GameObject m_SetingsPanel;//设置面板

    private GameObject button_Seting;//设置按钮
    private GameObject button_Close;//关闭按钮
    private GameObject button_Play;//Tap按钮


	void Start () {

        m_StartPanel = GameObject.Find("StartPanel");
        m_SetingsPanel = GameObject.Find("SetingsPanel");

        button_Seting = GameObject.Find("Seting");
        button_Close = GameObject.Find("Close");
        button_Play = GameObject.Find("Play");

        UIEventListener.Get(button_Seting).onClick = SetingButtonOnClick;
        UIEventListener.Get(button_Close).onClick = CloseButtonOnClick;
        UIEventListener.Get(button_Play).onClick = PlayButtonOnClick;

        m_StartPanel.SetActive(true);
        m_SetingsPanel.SetActive(false);
	}
	
	private void SetingButtonOnClick (GameObject  go) 
    {
        //首先判断设置面板是否已经显示，如果显示，就不再执行设置
        if (m_SetingsPanel.activeSelf ==false)
        {
            Debug.Log("设置按钮被点击");
            m_SetingsPanel.SetActive(true);
        }
	}

    /// <summary>
    /// 关闭按钮单击
    /// </summary>
    /// <param name="go"></param>

    private void CloseButtonOnClick(GameObject go)
    {
            Debug.Log("关闭按钮被点击");
            m_SetingsPanel.SetActive(false);
    }

    /// <summary>
    /// Tap按钮单击
    /// </summary>
    /// <param name="go"></param>
    private void PlayButtonOnClick(GameObject go)
    {
        Debug.Log("Tap按钮被点击"); 
        //跳转场景
        SceneManager.LoadScene("Game");
        
    }


    /// <summary>
    /// 设置开始按钮的状态，显示隐藏
    /// </summary>
    public void SetPlayButtonState(int state)
    {
        if (state == 1)
        {
            button_Play.SetActive(true);
        }
        else if(state == 0)
        {
            button_Play.SetActive(false);
        }
    }
}
