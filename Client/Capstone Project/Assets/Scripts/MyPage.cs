using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPage : MonoBehaviour
{
    public Text ScriptTxt;
    int star_count = 0;

    private void Start()
    {
        //Managers.User.MyPagePacketArrival = false;


        star_count = Managers.User.TotalStars;
        ScriptTxt = gameObject.transform.Find("MyPage").Find("My Star").Find("Star").GetComponent<Text>();
        string str = star_count.ToString();
        ScriptTxt.text = str;
    }

    public void Load_MyPage()
    {
        star_count = Managers.User.TotalStars;
        ScriptTxt = GameObject.Find("Star").GetComponent<Text>();
        string str = star_count.ToString();
        ScriptTxt.text = str;
    }
}