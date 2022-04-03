using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int blockId;
    private int blockType;
    // Start is called before the first frame update
    public int BlockId
    {
        get { return blockId; }
        set { blockId = value; }
    }

    public int BlockType
    {
        get { return blockType; }
        set { BlockType = value; }
    }
}
