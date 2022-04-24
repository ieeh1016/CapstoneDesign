using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClearPopup : UI_Popup
{
    int acquiredStars;
    
    public override void Init()
    {
        base.Init();
        acquiredStars = Managers.Stage.CompletedConditionList.Count;

        for (int i = 0; i < acquiredStars; i++)
        {
            string completedConditionName = Managers.Stage.CompletedConditionList[i];
           if (completedConditionName.Contains("Coin"))
            {
                gameObject.transform.Find("Coin").gameObject.SetActive(true);
            }
           else if (completedConditionName.Contains("Code"))
            {
                gameObject.transform.Find("Code").gameObject.SetActive(true);
            }
           else
            {
                gameObject.transform.Find("Map").gameObject.SetActive(true);
            }
        }
    }
   
    //public override void Init()
    //{
    //    base.Init();
    //    acquiredStars = Managers.Stage.CompletedConditionList.Count;
    //}
}
