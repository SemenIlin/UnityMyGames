using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] RoadPrefabs;
    public Transform PlayerTransform;
    
    private float startBlockXPos = 0;
    private int blockCount = 7;
    private float blockLength = 0;
    private List<GameObject> CurrentBocks = new List<GameObject>();

    void Start()
    {
        startBlockXPos = PlayerTransform.position.x + 15;
        blockLength = 30;

        StartGame();
    }

    public void StartGame()
    {
        PlayerTransform.GetComponent<PlayerMovement>().ResetPosition();
        foreach(var go in CurrentBocks)
        {
            Destroy(go);
        }
        CurrentBocks.Clear();

        for (int i = 0; i < blockCount; ++i)
        {
            SpawnBlock();
        }
    }

    void LateUpdate()
    {
        CheckForSpawn();   
    }

    private void CheckForSpawn()
    { 
        if(CurrentBocks[0].transform.position.x - PlayerTransform.position.x < -25)
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    private void SpawnBlock()
    {
        GameObject block = Instantiate(RoadPrefabs[Random.Range(0, RoadPrefabs.Length)], transform);
        Vector3 blockPos;

        if(CurrentBocks.Count > 0)
        {
            blockPos = CurrentBocks[CurrentBocks.Count - 1].transform.position + new Vector3(blockLength, 0, 0);
        }
        else
        {
            blockPos = new Vector3(startBlockXPos, 0, 0);
        }

        block.transform.position = blockPos;

        CurrentBocks.Add(block);
    }

    private void DestroyBlock()
    {
        Destroy(CurrentBocks[0]);
        CurrentBocks.RemoveAt(0);
    }
}
