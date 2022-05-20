using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSetting
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    GameObject root = GameObject.Find("@LoginSetting");
    //    if (root == null)
    //    {
    //        root = new GameObject() { name = "@LoginSetting" };
    //        root.AddComponent<LoginSetting>();

    //    }

    //    DontDestroyOnLoad(root);
    //}

    public void SendRequestNameInput()
    {
        C_Request_Name_input packet = new C_Request_Name_input();

        packet.Uid = Managers.User.UID;
        packet.name = Managers.User.Name;
        Managers.Network.Send(packet.Write());
    }

    public void SendRequestLoadStar()
    {
        C_Request_Load_Star packet = new C_Request_Load_Star();

        packet.UId = Managers.User.UID;

        Managers.Network.Send(packet.Write());
    }

    public void SendRequestMyPage()
    {
        C_Request_Challenge_MyPage packet = new C_Request_Challenge_MyPage();

        packet.UId = Managers.User.UID;

        Managers.Network.Send(packet.Write());
    }

    public void LoadTop30()
    {
        C_Request_Challenge_Top30Rank packet = new C_Request_Challenge_Top30Rank();

        Managers.Network.Send(packet.Write());
    }
}
