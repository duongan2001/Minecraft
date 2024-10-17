using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlocksData
{
    public int id;
    public float[] position;

    public BlocksData(Boxes block)
    {
        id = block.ID - 1;
        position = new float[3];
        position[0] = block.transform.position.x;
        position[1] = block.transform.position.y;
        position[2] = block.transform.position.z;
    }
}
