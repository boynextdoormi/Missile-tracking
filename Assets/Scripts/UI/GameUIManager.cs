using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// 游戏界面UI逻辑控制
/// </summary>
public class GameUIManager : MonoBehaviour
{
    private GameObject GamePanel;//游戏面板
    private GameObject OverPanel;//结束面板

    private GameObject m_buttonControl1;//控制器1
    private GameObject button_Reset;//重新开始按钮

    private UILabel lable_Star;//星数UI
    private UILabel lable_Time;//时间UI

    private int time;//时间

    private int Time
    {
        get { return time; }
        set { time = value;
        UpdateTimeLable(time); }
    }

    //OverPanelInfo
    private UILabel heightScore;//最高分数
    private UILabel starNum;//星数
    private UILabel timeNum;//时间分数


	void Start () {
        GamePanel = GameObject.Find("GamePanel");
        OverPanel = GameObject.Find("OverPanel");
        m_buttonControl1 = GameObject.Find("ButtonControl");
        button_Reset = GameObject.Find("Reset");

        lable_Star = GameObject.Find("StarNum1").GetComponent<UILabel>();
        lable_Star.text = "0";
        lable_Time = GameObject.Find("Time1").GetComponent<UILabel>();
        lable_Time.text = "0:0";
        StartCoroutine("AddTime");

        heightScore =GameObject.Find("Score/ScoreNum").GetComponent <UILabel>();
        starNum =GameObject.Find("Star/StarNum").GetComponent <UILabel>();
        timeNum = GameObject.Find("Time/TimeNum").GetComponent<UILabel>();

        UIEventListener.Get(button_Reset).onClick = ResetButtonClick;

        GamePanel.SetActive(true);
        OverPanel.SetActive(false);//结束面板默认隐藏
    }


    /// <summary>
    /// 更新星数UI
    /// </summary>
    /// <param name="star"></param>
    public void UpdateStarNum(int star)
    {
        lable_Star.text = star.ToString();
    }

    private void UpdateTimeLable(int t)
    {
        if (t < 60)
        {
            lable_Time.text = "0" +":"+ t;
        }
        else
        {
            lable_Time.text = t/60 + ":" + t % 60;
        }
    }

    /// <summary>
    /// 显示结束面板
    /// </summary>
    public void ShowOverPanel()
    {
        m_buttonControl1.SetActive(false);
        OverPanel.SetActive(true);
        StopAddTime();
        SetOverPanelInfo();
    }

    /// <summary>
    /// 按下重置按钮
    /// </summary>
    /// <param name="go"></param>

    private void ResetButtonClick(GameObject go)
    {
        SceneManager.LoadScene("Start");
    }


    IEnumerator AddTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Time++;
        }
    }

    /// <summary>
    /// 停止时间协程
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }

    /// <summary>
    /// 给结束面板赋值
    /// </summary>
    private void SetOverPanelInfo()
    {
        int t= int.Parse(lable_Star.text);
        starNum.text = "+"+t * 10;
        timeNum.text = "+" + time.ToString();
        int tempHeightScore = t * 10 + time;
        heightScore.text = tempHeightScore.ToString();

        //存储最高分
        PlayerPrefs.SetInt("HeightScore", tempHeightScore);
        //存储金币数
        PlayerPrefs.SetInt("GoldNum", t * 10);
    }
}
