using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage : MonoBehaviour
{
    public AudioClip audioSourceBgm;

    //public AudioSource audioSourceEffects;
    // Start is called before the first frame update
    void Awake()
    {
        PacketEvent.Init();
        Managers.Music.SetBackGroundMusic("Casual Game Music 05");
        Managers.Music.MusicPlayer.Play();
        //audioSourceEffects = Managers.Music.GetSoundEffect("Pop(1)");

        //audioSourceEffects.volume = Managers.Music.GetButtonVolume();
        if (StageManager.ToMain == true)
        {
            GameObject.Find("MainCanvas").transform.Find("MainPageUI").gameObject.SetActive(false);
            GameObject.Find("MainCanvas").gameObject.SetActive(true);
            GameObject.Find("MainCanvas").transform.Find("BasicStage_Select").gameObject.SetActive(true);
            StageManager.ToMain = false;

        }
        else if(StageManager.ToMain2 == true)
        {
            GameObject.Find("MainCanvas").transform.Find("MainPageUI").gameObject.SetActive(false);
            GameObject.Find("MainCanvas").gameObject.SetActive(true);
            GameObject.Find("MainCanvas").transform.Find("ConditionStage_Select").gameObject.SetActive(true);
            StageManager.ToMain2 = false;
        }
        else if (StageManager.ToMain3 == true)
        {
            GameObject.Find("MainCanvas").transform.Find("MainPageUI").gameObject.SetActive(false);
            GameObject.Find("MainCanvas").gameObject.SetActive(true);
            GameObject.Find("MainCanvas").transform.Find("LoopStage_Select").gameObject.SetActive(true);
            StageManager.ToMain3 = false;
        }
        
        else if (StageManager.ToMain4 == true)
        {
            GameObject.Find("MainCanvas").transform.Find("MainPageUI").gameObject.SetActive(false);
            GameObject.Find("MainCanvas").gameObject.SetActive(true);
            GameObject.Find("MainCanvas").transform.Find("ChallengeStage_Select").gameObject.SetActive(true);
            StageManager.ToMain4 = false;
        }
        else if (StageManager.ToMain5 == true)
        {
            GameObject.Find("MainCanvas").transform.Find("MainPageUI").gameObject.SetActive(false);
            GameObject.Find("MainCanvas").gameObject.SetActive(true);
            GameObject.Find("MainCanvas").transform.Find("SecondStudy_Stage_Select").gameObject.SetActive(true);
            StageManager.ToMain5 = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
