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
    }
   
    //public override void Init()
    //{
    //    base.Init();
    //    acquiredStars = Managers.Stage.CompletedConditionList.Count;
    //}
}
