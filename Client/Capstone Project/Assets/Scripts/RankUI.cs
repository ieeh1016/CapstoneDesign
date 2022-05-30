using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    float waitTime = 0;
    byte rankNum = 30;
    public IEnumerator WaitForPacket()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void Start()
    {
        Managers.Network.RankPacketArrival = false;
        Managers.Login.LoadTop30();
    }


    // Start is called before the first frame update
    public void SetUI()
    {

        Transform rank = gameObject.transform.Find("Rank");
        for (int i = 1; i <= rankNum; i++)
        {
            Transform ranki = rank.Find("Scroll View").Find("Viewport").Find("Content").Find($"Rank{i}");
            Text name = ranki.Find("Name").gameObject.GetComponent<Text>();
            ChallengeRankerInfo ranker;

            if (Managers.User.ChallengeTop30.TryGetValue(i, out ranker) == false)
                break;

            name.text = ranker.userName;

            Text stars = ranki.Find("StarN").gameObject.GetComponent<Text>();

            stars.text = ranker.totalStars.ToString();
        }

        Transform myRank = rank.Find("MyRank");
        myRank.Find("Name").GetComponent<Text>().text = Managers.User.Name;
        myRank.Find("RankN").GetComponent<Text>().text = Managers.User.Ranking.ToString();
        myRank.Find("StarN").GetComponent<Text>().text = Managers.User.TotalStars.ToString();
    }

    private void Update()
    {
        if (Managers.Network.RankPacketArrival == true || Managers.User.ChallangeStageInfo.Count != 0)
        {
            SetUI();
        }
    }


    // Update is called once per frame
}
