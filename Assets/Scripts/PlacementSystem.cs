using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject blockPlacePosition;
    [SerializeField] GameObject[] blockPrefs;
    public List<GameObject> existedBlock;
    public int currentBlockID = 0;
    public bool possiblePlaceBlock;

    private void Start()
    {
        UIManager.uiManager.UpdateItemBackgroundColor(0);
    }

    private void Update()
    {
        SelectBlock();
        PlaceBlock();
        ExitGame();
    }

    void SelectBlock()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBlockID = 0;
            UIManager.uiManager.UpdateItemBackgroundColor(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentBlockID = 1;
            UIManager.uiManager.UpdateItemBackgroundColor(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBlockID = 2;
            UIManager.uiManager.UpdateItemBackgroundColor(2);
        }
    }

    public void SpawnBlock(Vector3 pos, int blockID)
    {
        GameObject spawnBlock = Instantiate(blockPrefs[blockID], pos, Quaternion.identity);
        existedBlock.Add(spawnBlock);
        int amount = int.Parse(UIManager.uiManager.itemAmounts[blockID].text) - 1;
        Debug.Log(blockID);
        UIManager.uiManager.itemAmounts[blockID].text = amount.ToString();
    }

    void PlaceBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (possiblePlaceBlock)
            {
                SpawnBlock(blockPlacePosition.transform.position, currentBlockID);
            }
        }
    }

    void SaveGame()
    {
        if (existedBlock.Count == 0)
        {
            return;
        }

        List<SaveLoadBlocks> saveLoadBlocks = new List<SaveLoadBlocks>();
        for (int i = 0; i < existedBlock.Count; i++)
        {
            saveLoadBlocks.Add(new SaveLoadBlocks { 
                id = existedBlock[i].GetComponent<Boxes>().ID - 1,
                pos = existedBlock[i].transform.position
            });
        }
        string jsonBlock = JsonUtility.ToJson(saveLoadBlocks);
        File.WriteAllText(Application.dataPath + "save_block.txt", jsonBlock);
        Debug.Log("Saved blocks");

        List<SaveLoadBlocksAmount> saveLoadBlocksAmount = new List<SaveLoadBlocksAmount>();
        for (int i = 0; i < 3; i++)
        {
            saveLoadBlocksAmount.Add(new SaveLoadBlocksAmount { amount = int.Parse(UIManager.uiManager.itemAmounts[i].text) });
        }
        string jsonAmount = JsonUtility.ToJson(saveLoadBlocksAmount);
        File.WriteAllText(Application.dataPath + "save_amount.txt", jsonAmount);
        Debug.Log("Saved amount");
    }

    void LoadGame()
    {
        if (File.Exists(Application.dataPath + "save_block.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "save_block.txt");
            //List<SaveLoadBlocks> saveLoadBlocks = JsonUtility.FromJson<SaveLoadBlocks>(saveString);
        }

        if (File.Exists(Application.dataPath + "save_amount.txt"))
        {

        }
    }

    void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SaveGame();
            Application.Quit();
        }
    }


}
