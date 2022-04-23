using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
    public static void S_Reply_Name_input(PacketSession session, IPacket packet)
    {
        S_Reply_Name_input pkt = packet as S_Reply_Name_input;
        ServerSession serverSession = session as ServerSession;

        // TODO
    }

    public static void S_Challenge_Load_Star(PacketSession session, IPacket packet)
    {
        S_Challenge_Load_Star pkt = packet as S_Challenge_Load_Star;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeInfoByPacket(pkt);
    }

    public static void S_Challenge_MyPage(PacketSession session, IPacket packet)
    {
        S_Challenge_MyPage pkt = packet as S_Challenge_MyPage;
        ServerSession serverSession = session as ServerSession;

        Managers.User.Name = pkt.name;
        Managers.User.TotalStars = pkt.TotalStars;
        Managers.User.Ranking = pkt.ranking;
    }

    public static void S_Challenge_Top30Rank(PacketSession session, IPacket packet)
    {
        S_Challenge_Top30Rank pkt = packet as S_Challenge_Top30Rank;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeTop30(pkt);
    }

    /*public static void S_ChallengeTop30Handler(PacketSession session, IPacket packet)
    {
        S_ChallengeTop30 pkt = packet as S_ChallengeTop30;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeTop30(pkt);
    }*/
}
