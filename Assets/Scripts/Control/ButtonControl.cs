using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
    private Ship m_Ship;

    private GameObject left;
    private GameObject right;
    


	void Start () { 

        left = GameObject.Find("Left");
        right = GameObject.Find("Right");

        m_Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        UIEventListener.Get(left).onPress = LeftPress;
        UIEventListener.Get(right).onPress = RightPress;
	}


    private void LeftPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            Debug.Log("left is pressed");
            m_Ship.IsLeft = true;
        }
        else 
        {
            Debug.Log("left is over");
            m_Ship.IsLeft = false;
        }
    }

    private void RightPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            Debug.Log("right is pressed");
            m_Ship.IsRight = true;
        }
        else
        {
            Debug.Log("right is over");
            m_Ship.IsRight = false;
        }
    }


	void Update () {
       
	}
}
