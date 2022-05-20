using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;
using UnityEngine;

namespace DummyClient
{
	class ServerSession : PacketSession
	{
		public override void OnConnected(EndPoint endPoint)
		{
			Managers.Network.Connected = true;
			Debug.Log($"OnConnected : {endPoint}");
			//Console.WriteLine($"OnConnected : {endPoint}");			
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			Managers.Network.Connected = false;
			Debug.Log($"OnDisconnected : {endPoint}");
			//Console.WriteLine($"OnDisconnected : {endPoint}");
		}

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			PacketManager.Instance.OnRecvPacket(this, buffer);
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
	}
}
