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

    byte waitingForConnect = 0;
    byte numOfWaitingPacket = 0;

    bool rankPacketArrival = false;
    bool myPagePacketArrival = false;
    bool loadStarPacketArrival = false;
    bool waitForPacketToReconnect = false;
    bool connectedToServer = false;

    public Action ConnectAction;
    public Action<ArraySegment<byte>> SendAction;

    
    Queue<ArraySegment<byte>> sendingPendingList = new Queue<ArraySegment<byte>>();


    float time = 0;

    public byte NumOfWaitingConnection
    {
        get { return waitingForConnect; }
        set { waitingForConnect = value; }
    }

    public byte NumOfWaitingPacket
    {
        get { return numOfWaitingPacket; }
        set { numOfWaitingPacket = value; }
    }

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

    public Queue<ArraySegment<byte>> SendingPendingList
    {
        get { return sendingPendingList; }
        set { sendingPendingList = value; }
    }

    public void ConnectAndSend(ArraySegment<byte> sendBuff, bool waitForResponse = false)
    {
        if (numOfWaitingPacket == 0 || sendingPendingList.Count == 0) // 전송 대기를 하고 있는 패킷이 없다면 소켓 연결
            ConnectToServer();

        sendingPendingList.Enqueue(sendBuff); // pendingList에 패킷을 넣어놓고 연결이 완료되었을 시 전송한다.

        if (waitForResponse) // Response를 기다려야 하는 연결이라면 numofWaitingPacket + 1 을 함으로써 소켓 연결 유지한다
            numOfWaitingPacket++;
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
        ipAddr = IPAddress.Parse("3.39.181.102"); //
        endPoint = new IPEndPoint(ipAddr, 7777);

        //host = Dns.GetHostName();
        //ipHost = Dns.GetHostEntry("NLB-CodingIsland-9cfc1fd3f5aa14cc.elb.ap-northeast-2.amazonaws.com");
        //ipAddr = ipHost.AddressList[0];
        //endPoint = new IPEndPoint(ipAddr, 7777);

        connector = new Connector();

        ConnectAction += ConnectToServer;
        SendAction += Send; // Stateless 전송 위해 SendAction에 등록 후 Connect가 완료되면 Send;

        //connector.Connect(endPoint,
        //    () => { return _session; },
        //    1);
    }

    public void ConnectToServer() // Stateless 서버를 위한 메소드
    {

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
