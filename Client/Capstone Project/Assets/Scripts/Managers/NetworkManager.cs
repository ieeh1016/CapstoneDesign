using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager
{
    public enum PacketID
    {
        MyName = 1,
        MyPage = 2,
        Load30 = 3,
    }

	//bool connected = false;

	ServerSession _session = new ServerSession();
	string host;
	IPHostEntry ipHost;
	IPAddress ipAddr;
	IPEndPoint endPoint;

	Connector connector;


	//float time = 0;


	public void Send(ArraySegment<byte> sendBuff)
	{

        //while (connected == false)
        //{
        //    Debug.Log($"NetworkManager's connected:{connected}");
        //    time += Time.deltaTime;
        //    if (time >= 50.0f)
        //    {
        //        time = 0;
        //        return;
        //    }
        //    // busy wait
        //}
        //Debug.Log($"Send:{connected}");
        _session.Send(sendBuff);
	}

	public void Init()
    {
        // DNS (Domain Name System)

        host = Dns.GetHostName();
        ipHost = Dns.GetHostEntry(host);
        ipAddr = IPAddress.Parse("3.39.181.102"); // AWS EC2 Instance의 IP 주소를 IPAdress 객체로 변환
        endPoint = new IPEndPoint(ipAddr, 7777);

        //host = Dns.GetHostName();
        //ipHost = Dns.GetHostEntry(host);
        //ipAddr = ipHost.AddressList[0];
        //endPoint = new IPEndPoint(ipAddr, 7777);

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
