using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketEvent : MonoBehaviour
{
    //static PacketEvent s_instance;

    //public static PacketEvent Instance
    //{
    //    get
    //    {
    //        Init();
    //        return s_instance;
    //    }
    //}
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //public static void Init()
    //{
    //    if (s_instance == null)
    //    {
    //        GameObject root = GameObject.Find("@PacketEvent");
    //        if (root == null)
    //        {
    //            root = new GameObject() { name = "@PacketEvent" };
    //            root.AddComponent<PacketEvent>();

    //        }

    //        DontDestroyOnLoad(root);
    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //public void SendRequestChallengeTop30()
    //{
    //    C_Request_Challenge_Top30Rank packet = new C_Request_Challenge_Top30Rank();


    //    Managers.Network.ConnectAndSend(packet.Write(), true);
    //}

    //public void SendRequestMyPage()
    //{
    //    C_Request_Challenge_MyPage packet = new C_Request_Challenge_MyPage();

    //    packet.UId = Managers.User.UID;

    //    Managers.Network.ConnectAndSend(packet.Write(), true);
    //}
}
