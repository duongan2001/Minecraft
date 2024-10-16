using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{   //Player View
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject targetPositionGreen;
    [SerializeField] GameObject targetPositionRed;
    GameObject existedBlock;
    GameObject possibleDestroyBlock;
    int blockID;
    [SerializeField] Grid grid;
    Vector3Int gridPosition;

    [SerializeField] PlacementSystem placeSystem;

    private void Update()
    {
        DestroyExistedBlock();
    }

    private void FixedUpdate()
    {
        targetPositionGreen.transform.position = HitPosition();
        gridPosition = grid.WorldToCell(targetPositionGreen.transform.position);
        targetPositionGreen.transform.position = gridPosition + new Vector3(0.5f, 0.5f, 0.5f);

        targetPositionRed.transform.position = HitPosition();
        targetPositionRed.transform.position = gridPosition + new Vector3(0.5f, 0.5f, 0.5f);

        CheckPlaceBlockPossible();
    }

    void CheckPlaceBlockPossible()
    {
        if (targetPositionRed.transform.position != existedBlock.transform.position)
        {
            targetPositionGreen.SetActive(true);
            targetPositionRed.SetActive(false);
            placeSystem.possiblePlaceBlock = true;
        }
        else
        {
            targetPositionGreen.SetActive(false);
            targetPositionRed.SetActive(true);
            placeSystem.possiblePlaceBlock = false;
        }
    }

    public Vector3 HitPosition()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        Vector3 targetPosition;
        if (Physics.Raycast(ray, out hit, 20f, layerMask))
        {
            targetPosition = hit.point;
            existedBlock = hit.collider.gameObject;
            possibleDestroyBlock = existedBlock;
            blockID = hit.collider.gameObject.GetComponent<Boxes>().ID - 1;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        else
        {
            targetPosition = Vector3.zero;
            existedBlock = targetPositionGreen;
            possibleDestroyBlock = null;
            blockID = -1;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
        }
        return targetPosition;
    }

    void DestroyExistedBlock()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (targetPositionRed.transform.position == existedBlock.transform.position)
            {
                Destroy(possibleDestroyBlock);
                UIManager.uiManager.UpdateItemAmounts(blockID);
            }
        }
    }
}
