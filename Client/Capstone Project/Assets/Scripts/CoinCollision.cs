using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    //캐릭터가 코인에 닿았을 때 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("코인획득!");

        Managers.Coin.AcquireCoin(gameObject);
    }

}
