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
		S_Reply_Name_input s_pkt = packet as S_Reply_Name_input;
		s_pkt.reply = Server.DB.DbManager.user_Name_Input(pkt.name, pkt.Uid);
		clientSession.Send(s_pkt.Write());
	}

	public static void C_Request_Load_Star_Handler(PacketSession session, IPacket packet) //
	{
		C_Request_Load_Star pkt = packet as C_Request_Load_Star;
		ClientSession clientSession = session as ClientSession;
		S_Challenge_Load_Star s_pkt = packet as S_Challenge_Load_Star;
		Dictionary<byte, byte> star_Dic = Server.DB.DbManager.Load_star(pkt.UId);
		foreach (KeyValuePair<byte, byte> items in star_Dic)
		{
			s_pkt.stageStars.Add(new S_Challenge_Load_Star.StageStar()
			{
				stageId = items.Key,
				numberOfStars = items.Value
			});
		}
		clientSession.Send(s_pkt.Write());
	}

	public static void C_Request_Challenge_MyPageHandler(PacketSession session, IPacket packet)
	{
		C_Request_Challenge_MyPage pkt = packet as C_Request_Challenge_MyPage;
		ClientSession clientSession = session as ClientSession;
		S_Challenge_MyPage s_pkt = packet as S_Challenge_MyPage;
		List<Server.DB.DbManager.Data_Structure> data_set = Server.DB.DbManager.Challenge_MyPage(pkt.UId);

		s_pkt.name = Convert.ToString(data_set.Select(x => x.name).Distinct());
		s_pkt.ranking = Convert.ToInt32(data_set.Select(x => x.ranking).Distinct());
		s_pkt.TotalStars = Convert.ToByte(data_set.Select(x => x.TotalStars).ToList());
		clientSession.Send(s_pkt.Write());
	}

	public static void C_Request_Challenge_Top30RankHandler(PacketSession session, IPacket packet) //
	{
		C_Request_Challenge_Top30Rank pkt = packet as C_Request_Challenge_Top30Rank;
		ClientSession clientSession = session as ClientSession;
		S_Challenge_Top30Rank s_pkt = packet as S_Challenge_Top30Rank;
		List<S_Challenge_Top30Rank.Rank> top_30 = new List<S_Challenge_Top30Rank.Rank>();
		s_pkt.ranks = Server.DB.DbManager.Study_ChallengeTop30(top_30);
		clientSession.Send(s_pkt.Write());
	}

	public static void C_ChallengeUpdateStarsHandler(PacketSession session, IPacket packet) //
	{
		C_ChallengeUpdateStars pkt = packet as C_ChallengeUpdateStars;
		ClientSession clientSession = session as ClientSession;
		Server.DB.DbManager.challenge_UpdateStar(pkt.UId, pkt.stageId, pkt.numberOfStars);
		session.Disconnect();
	}
}
