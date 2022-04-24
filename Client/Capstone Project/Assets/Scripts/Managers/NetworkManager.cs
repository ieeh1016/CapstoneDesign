using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager
{
	ServerSession _session = new ServerSession();
	string host;
	IPHostEntry ipHost;
	IPAddress ipAddr;
	IPEndPoint endPoint;

	Connector connector;

	public void Send(ArraySegment<byte> sendBuff)
	{
		_session.Send(sendBuff);
	}

	public void Init()
    {
		// DNS (Domain Name System)
		host = Dns.GetHostName();
		ipHost = Dns.GetHostEntry(host);
		ipAddr = ipHost.AddressList[0];
		endPoint = new IPEndPoint(ipAddr, 7777);

		connector = new Connector();

		connector.Connect(endPoint,
			() => { return _session; },
			1);	
	}

	public void ConnectToServer()
    {
		connector.Connect(endPoint,
			() => { return _session; },
			1);
	}

    public void Update()
	{
		List<IPacket> list = PacketQueue.Instance.PopAll();
		foreach (IPacket packet in list)
			PacketManager.Instance.HandlePacket(_session, packet);
	}


}
