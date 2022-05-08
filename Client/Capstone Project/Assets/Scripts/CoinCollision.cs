using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    //캐릭터가 코인에 닿았을 때 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"충돌한 물체: {collision.gameObject}");
        if (collision.gameObject.name.Contains("Character"))
        {
            Debug.Log("코인획득!");
            Managers.Coin.AcquireCoin(gameObject);
        }
    }

}
