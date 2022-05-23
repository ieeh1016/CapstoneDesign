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
        
    }

    public void OnEnable()
    {
        //Managers.User.RankPacketArrival = false; // 패킷이 도착할 때까지 대기하기 위한 변수
        Managers.Login.LoadTop30();
        
        //while (Managers.User.RankPacketArrival == false) // 서버로부터 패킷이 도착할 때까지 대기
        //{
        //    Debug.Log($"While문 에서의 RankPacketArriavl: {Managers.User.RankPacketArrival}");
        //    // busy wait for rank packet
        //    waitTime += Time.deltaTime;
        //    if (waitTime >= 10)
        //    {
        //        waitTime = 0;
        //        return;
        //    }
        //}

        //StartCoroutine("WaitForPacket");

    }

    // Start is called before the first frame update
    public void SetUI()
    {
        Debug.Log("SetUI 블렸엉라ㅓ");

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
        Debug.Log($"RankUI 에서의 RankPacketArriavl: {Managers.User.RankPacketArrival}");
        if (Managers.User.RankPacketArrival == true)
        {
            Debug.Log($"RankUI 에서의 RankPacketArriavl: {Managers.User.RankPacketArrival}");
            SetUI();
            Managers.User.RankPacketArrival = false;
        }
    }


    // Update is called once per frame
}
