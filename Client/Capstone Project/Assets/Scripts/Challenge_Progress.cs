using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Challenge_Progress : MonoBehaviour
{
    Transform _challenge = GameObject.Find("ChallengeStage_Select").transform;
    public Toggle toggle;
    void Start()
    {
        byte a = 0;
        for(ushort i = 1; i <= 10; i++)
        {
            Managers.User.ChallangeStageInfo.TryGetValue(i, out a);

            Transform Level_Open = _challenge.Find($"Btn_Level{i}_open");
            Transform Level_Close = _challenge.Find($"Btn_Level{i}_closed");

            Level_Open.gameObject.SetActive(true);
            Level_Close.gameObject.SetActive(false);

            //Text name = .Find("Name").gameObject.GetComponent<Text>();
            //for (int j = 1; j<= a; j++)
            //{
            //    toggle = Level_Open.Find($"Toggle-Star{j}"); // on

            //}
        }
    }
}