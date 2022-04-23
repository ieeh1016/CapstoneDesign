using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("@PacketEvent");
        if (root == null)
        {
            root = new GameObject() { name = "@PacketEvent" };
            root.AddComponent<PacketEvent>();

        }
        
        DontDestroyOnLoad(root);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendRequestChallengeTop30()
    {
        C_Request_Challenge_Top30Rank packet = new C_Request_Challenge_Top30Rank();


        Managers.Network.Send(packet.Write());
    }

    public void SendRequestMyPage()
    {
        C_Request_Challenge_MyPage packet = new C_Request_Challenge_MyPage();

        packet.UId = Managers.User.UID;

        Managers.Network.Send(packet.Write());
    }
}
