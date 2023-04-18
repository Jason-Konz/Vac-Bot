using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will destroy all the objects in the grid
//Then call CreateGrid()
public class ResetGrid : MonoBehaviour
{

    public GameObject gameManager;

    public void Reset()
    {
        GameObject[] gridTiles = GameObject.FindGameObjectsWithTag("Grid");
        foreach(GameObject tile in gridTiles)
        {
            GameObject.Destroy(tile);
        }
        gameManager.GetComponent<CreateGrid>().CreateGrid1();
        Move.resetMove();
        Play.visited = null;
        Play.obs = null;

    }
}
