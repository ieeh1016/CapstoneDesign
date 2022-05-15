using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    float waitTime = 0;
    public IEnumerator WaitForPacket()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void Start()
    {
        Managers.User.RankPacketArrival = false; // 패킷이 도착할 때까지 대기하기 위한 변수
        Managers.Login.LoadTop30(); 

        while(Managers.User.RankPacketArrival == false) // 서버로부터 패킷이 도착할 때까지 대기
        {
            // busy wait for rank packet
            waitTime += Time.deltaTime;
            if (waitTime >= 3)
                return;
        }

        //StartCoroutine("WaitForPacket");

        Debug.Log("SetUI 블렸엉라ㅓ");

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
