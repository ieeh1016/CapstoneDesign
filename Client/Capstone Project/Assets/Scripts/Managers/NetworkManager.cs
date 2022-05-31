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


	ServerSession _session = new ServerSession();
	string host;
	IPHostEntry ipHost;
	IPAddress ipAddr;
	IPEndPoint endPoint;

	Connector connector;

    bool rankPacketArrival = false;
    bool myPagePacketArrival = false;
    bool loadStarPacketArrival = false;
    bool waitForPacketToReconnect = false;
    bool connectedToServer = false;
    bool waitForPacket = false;

    float time = 0;

    public bool RankPacketArrival
    {
        get { return rankPacketArrival; }
        set { rankPacketArrival = value; }
    }

    public bool MyPagePacketArrival
    {
        get { return myPagePacketArrival; }
        set { myPagePacketArrival = value; }
    }

    public bool LoadStarPacketArrival
    {
        get { return loadStarPacketArrival; }
        set { loadStarPacketArrival = value; }
    }

    public bool Connected
    {
        get { return connectedToServer; }
        set { connectedToServer = value; }
    }


    public void Send(ArraySegment<byte> sendBuff)
	{
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

    public void ConnectToServer() // Stateless 서버를 위한 메소드
    {
        while (waitForPacket)
        {
            // busy wait
        }

        Connected = false;

        _session = new ServerSession();

        connector.Connect(endPoint,
            () => { return _session; },
            1);
    }

	public void TryReConnectToServer() // Stateful 서버를 위한 메소드
    {

        if (rankPacketArrival && myPagePacketArrival && loadStarPacketArrival)
        {
            _session.Disconnect();
            _session = new ServerSession();

            connector.Connect(endPoint,
            () => { return _session; },
            1);

            waitForPacketToReconnect = false;
        }
        else
        {
            waitForPacketToReconnect = true;
        }
		
	}

    public void OnUpdate()
	{
        if (waitForPacketToReconnect)
            TryReConnectToServer();
        else
        {
            time += Time.deltaTime;
            if (time > 300f)
            {
                time = 0;
                TryReConnectToServer();
            }
        }
	}


}
