using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;
using UnityEngine;

namespace DummyClient
{
	public class ServerSession : PacketSession
	{
		public override void OnConnected(EndPoint endPoint)
		{
			//Managers.Network.Connected = true;
			Debug.Log($"OnConnected : {endPoint}");
			//Console.WriteLine($"OnConnected : {endPoint}");			
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			//Managers.Network.Connected = false;
			Debug.Log($"OnDisconnected : {endPoint}");
			//Console.WriteLine($"OnDisconnected : {endPoint}");
		}

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			Debug.Log("OnRecvPacket 호출됨");
			PacketManager.Instance.OnRecvPacket(this, buffer);
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
	}
}
