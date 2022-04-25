using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRankerInfo
{
    public string userName = null;
    public int totalStars = 0;

    public ChallengeRankerInfo(string userName, int totalStars)
    {
        this.userName = userName;
        this.totalStars = totalStars;
    }
}
