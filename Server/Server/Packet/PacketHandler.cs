using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Server.DB.DbManager;

class PacketHandler
{
    public static void C_Request_Name_inputHandler(PacketSession session, IPacket packet) //
    {
        C_Request_Name_input pkt = packet as C_Request_Name_input;
        ClientSession clientSession = session as ClientSession;
        S_Challenge_Load_Star s_pkt = new S_Challenge_Load_Star();
        Dictionary<byte, byte> star_Dic = Server.DB.DbManager.user_Name_Input(pkt.Uid, pkt.name);
        foreach (KeyValuePair<byte, byte> items in star_Dic)
        {
            s_pkt.stageStars.Add(new S_Challenge_Load_Star.StageStar()
            {
                stageId = items.Key,
                numberOfStars = items.Value
            });
        }
        clientSession.Send(s_pkt.Write());
        session.Disconnect();

        Console.WriteLine("전송완료");
    }

    public static void C_Request_Challenge_MyPageHandler(PacketSession session, IPacket packet)
    {
        C_Request_Challenge_MyPage pkt = packet as C_Request_Challenge_MyPage;
        ClientSession clientSession = session as ClientSession;
        S_Challenge_MyPage s_pkt = new S_Challenge_MyPage();
        Data_Structure data_set = Server.DB.DbManager.Challenge_MyPage(pkt.UId);

        s_pkt.name = data_set.name;
        s_pkt.ranking = data_set.ranking;
        s_pkt.TotalStars = data_set.TotalStars;

        clientSession.Send(s_pkt.Write());
        session.Disconnect();
    }

    public static void C_Request_Challenge_Top30RankHandler(PacketSession session, IPacket packet) //
    {
        Console.WriteLine("C_Request_Challenge_Top30 Received");
        C_Request_Challenge_Top30Rank pkt = packet as C_Request_Challenge_Top30Rank;
        ClientSession clientSession = session as ClientSession;
        S_Challenge_Top30Rank s_pkt = new S_Challenge_Top30Rank();
        List<S_Challenge_Top30Rank.Rank> top_30 = new List<S_Challenge_Top30Rank.Rank>();
        s_pkt.ranks = Server.DB.DbManager.Study_ChallengeTop30(top_30);

        clientSession.Send(s_pkt.Write());
        session.Disconnect();
    }

    public static void C_ChallengeUpdateStarsHandler(PacketSession session, IPacket packet) //
    {
        C_ChallengeUpdateStars pkt = packet as C_ChallengeUpdateStars;
        ClientSession clientSession = session as ClientSession;
        Server.DB.DbManager.challenge_UpdateStar(pkt.UId, pkt.stageId, pkt.numberOfStars);
        session.Disconnect();
    }
}