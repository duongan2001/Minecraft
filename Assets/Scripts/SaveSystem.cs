using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveBlocks(Boxes[] block)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/blocks.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        BlocksData[] data = new BlocksData[block.Length];

        for (int i = 0; i < block.Length; i++)
        {
            data[i] = new BlocksData(block[i]);           
        }        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static BlocksData loadBlocks()
    {
        string path = Application.persistentDataPath + "/blocks.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BlocksData data = formatter.Deserialize(stream) as BlocksData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
