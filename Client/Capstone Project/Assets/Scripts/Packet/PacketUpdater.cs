using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("@PacketUpdater");
        if (root == null)
        {
            root = new GameObject() { name = "@PacketUpdater" };
            root.AddComponent<PacketUpdater>();

        }
        DontDestroyOnLoad(root);
    }

    // Update is called once per frame
    void Update()
    {
        Managers.Network.Update();
    }
}
