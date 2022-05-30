using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

class PacketHandler
{
    //static public Action UIHandle = null;

    public static void S_Challenge_Load_StarHandler(PacketSession session, IPacket packet)
    {
        Debug.Log("S_Challenge_Load_StarHandler called");
        S_Challenge_Load_Star pkt = packet as S_Challenge_Load_Star;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeInfoByPacket(pkt);

        Managers.Network.LoadStarPacketArrival = true;
        
    }

    public static void S_Challenge_MyPageHandler(PacketSession session, IPacket packet)
    {
        Debug.Log("S_Challenge_MyPageHandler called");
        S_Challenge_MyPage pkt = packet as S_Challenge_MyPage;
        ServerSession serverSession = session as ServerSession;

        Managers.User.Name = pkt.name;
        Managers.User.Ranking = pkt.ranking;
        Managers.User.TotalStars = pkt.TotalStars;

        Managers.Network.MyPagePacketArrival = true;

    }

    public static void S_Challenge_Top30RankHandler(PacketSession session, IPacket packet)
    {
        Debug.Log("S_Challenge_Top30RankHandler called");
        S_Challenge_Top30Rank pkt = packet as S_Challenge_Top30Rank;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeTop30(pkt);

        Managers.Network.RankPacketArrival = true;

        Debug.Log($"S_Challenge_Top30RankHandler 에서의 RankPacketArrival: {Managers.Network.RankPacketArrival}");

        //UIHandle.Invoke();
        //GameObject.Find("RankUI").GetComponent<RankUI>().SetUI();
    }

    /*public static void S_ChallengeTop30Handler(PacketSession session, IPacket packet)
    {
        S_ChallengeTop30 pkt = packet as S_ChallengeTop30;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeTop30(pkt);
    }*/
}
