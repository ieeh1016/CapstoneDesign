using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;
using UnityEngine;

namespace DummyClient
{
	//public class ServerSession : PacketSession
	//{
	//	public override void OnConnected(EndPoint endPoint)
	//	{
	//		Managers.Network.Connected = true;
	//		Debug.Log($"OnConnected : {endPoint}");

	//		if (Managers.Network.SendingPendingList.Count != 0) // Connect가 되면 패킷 하나를 보낸다.
 //           {
	//			Managers.Network.SendAction.Invoke(Managers.Network.SendingPendingList.Dequeue());
 //           }
	//		//Console.WriteLine($"OnConnected : {endPoint}");			
	//	}

	//	public override void OnDisconnected(EndPoint endPoint)
	//	{
	//		Managers.Network.Connected = false;
	//		Debug.Log($"OnDisconnected : {endPoint}");

	//		if (Managers.Network.SendingPendingList.Count != 0)
 //           {
	//			Managers.Network.ConnectAction.Invoke(); // 만약 전송을 대기하는 패킷이 있다면 Connect
 //           }
	//		//Console.WriteLine($"OnDisconnected : {endPoint}");
	//	}

	//	public override void OnRecvPacket(ArraySegment<byte> buffer)
	//	{
	//		Debug.Log("OnRecvPacket 호출됨");
	//		PacketManager.Instance.OnRecvPacket(this, buffer);
	//	}

	//	public override void OnSend(int numOfBytes)
	//	{
	//		//Console.WriteLine($"Transferred bytes: {numOfBytes}");
	//	}
	//}
}
