using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


		// DNS (Domain Name System)
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();

		connector.Connect(endPoint,
			() => { return _session; },
			1);

		GameObject root = GameObject.Find("@NetworkManager");
		if (root == null)
		{
			root = new GameObject() { name = "@NetworkManager" };

		}
		root.AddComponent<NetworkManager>();
		DontDestroyOnLoad(root);
	}

    void Start()
    {
		// DNS (Domain Name System)
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();

		connector.Connect(endPoint,
			() => { return _session; },
			1);

		GameObject root = GameObject.Find("@NetworkManager");
		if (root == null)
		{
			root = new GameObject() { name = "@NetworkManager" };

		}
		root.AddComponent<NetworkManager>();
		DontDestroyOnLoad(root);
	}

    public void Update()
	{
		List<IPacket> list = PacketQueue.Instance.PopAll();
		foreach (IPacket packet in list)
			PacketManager.Instance.HandlePacket(_session, packet);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

}
