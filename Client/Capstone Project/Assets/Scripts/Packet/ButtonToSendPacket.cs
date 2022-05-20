using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToSendPacket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        //if (gameObject.name.Contains("Rank"))
        //    btn.onClick.AddListener(GameObject.Find("@PacketEvent").GetComponent<PacketEvent>().SendRequestChallengeTop30);
        //else if(gameObject.name.Contains("MyPage"))
        //    btn.onClick.AddListener(GameObject.Find("@PacketEvent").GetComponent<PacketEvent>().SendRequestMyPage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
