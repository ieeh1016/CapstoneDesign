using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    public IEnumerator WaitForPacket()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void Start()
    {
        StartCoroutine("WaitForPacket");

        Debug.Log("SetUI ºí·È¾û¶ó¤Ã");
        Transform rank = gameObject.transform.Find("Rank");
        for (int i = 1; i <= 3; i++)
        {
            Transform ranki = rank.Find($"Rank{i}");
            Text name = ranki.Find("Name").gameObject.GetComponent<Text>();
            ChallengeRankerInfo ranker;
            Managers.User.ChallengeTop30.TryGetValue(i, out ranker);

            name.text = ranker.userName;

            Text stars = ranki.Find("Text").gameObject.GetComponent<Text>();
            
            stars.text = ranker.totalStars.ToString();
        }
    }

    // Start is called before the first frame update
    public void SetUI()
    {

        
    }




    // Update is called once per frame
}
