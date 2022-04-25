using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform rank = gameObject.transform.Find("Rank");
        for (int i = 1; i <= 3; i++)
        {
            Transform ranki = rank.Find($"Rank{i}");
            Text name = ranki.Find("Text").gameObject.GetComponent<Text>();
            ChallengeRankerInfo ranker;
            Managers.User.ChallengeTop30.TryGetValue(i, out ranker);
            
            name.text = ranker.userName;

            Text stars = ranki.Find($"rank{i}_star").Find("Text").GetComponent<Text>();

            stars.text = ranker.totalStars.ToString();
        }
    }

    // Update is called once per frame
}
