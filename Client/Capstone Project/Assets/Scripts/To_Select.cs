using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class To_Select : MonoBehaviour
{
    // Start is called before the first frame update
    public void Basic_Stage_Click()
    {
        StageManager.Basic = true;
    }
    public void Codition_Stage_Click()
    {
        StageManager.Codition = true;
    }
    public void Loop_Stage_Click()
    {
        StageManager.Loop = true;
    }
    
     public void Challenge_Stage_Click()
    {
        StageManager.Challenge = true;
    }

    public void SecondStudy_Stage_Click()
    {
        StageManager.Challenge = false;
        StageManager.SecondStudy = true;
    }

    public void Basic_Stage_Click_false()
    {
        StageManager.Basic = false;
    }
    public void Codition_Stage_Click_false()
    {
        StageManager.Codition = false;
    }
    public void Loop_Stage_Click_false()
    {
        StageManager.Loop = false;
    }

    public void Challenge_Stage_Click_false()
    {
        StageManager.Challenge = false;
    }
    public void SecondStudy_Stage_Click_false()
    {
        StageManager.SecondStudy = false;
    }


}
