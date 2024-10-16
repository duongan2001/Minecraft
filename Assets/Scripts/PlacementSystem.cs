using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void SpawnBlock(Vector3 pos, int blockID)
    {
        GameObject spawnBlock = Instantiate(blockPrefs[blockID], pos, Quaternion.identity);
        existedBlock.Add(spawnBlock);
        int amount = int.Parse(UIManager.uiManager.itemAmounts[blockID].text) - 1;
        Debug.Log(blockID);
        UIManager.uiManager.itemAmounts[blockID].text = amount.ToString();
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
}
