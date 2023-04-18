using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System.Threading;

public class Play : MonoBehaviour
{
    public float timer = 0.2f;
    public bool play = false;
    public static List<Tuple<int, int>> visited = null;
    public static List<Tuple<int, int>> obs = null;
    public static List<Tuple<int, int>> currPath = null;
    public static int algorithm = 0;
    public static bool impossible = false;



    // Update is called once per frame
    void FixedUpdate()
    {
        if (play)
        {
            

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0)
            {
                PlayAgent();
                timer = 0.2f;
                //timer = 1;
            }
            
        }
    }

    

    public int dirtCount() {
        GameObject[] gridTiles = GameObject.FindGameObjectsWithTag("Grid");
        int count = 0;
        foreach (GameObject tile in gridTiles)
        {
            if (tile.GetComponent<TileSelection>().id == 1)
            {
                count++;
            }
        }
        return count;
    }
    public bool isBump(int x, int y) {
        if (x > 7 || y < 0 || y > 7 || x < 0) {
            Debug.Log("outside of the grid");
            return true;
            
        }
        GameObject[] gridTiles = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject tile in gridTiles)
        {
            if (tile.GetComponent<TileSelection>().id == 2)
            {
                int obsx = tile.GetComponent<TileSelection>().xPos;
                int obsy = tile.GetComponent<TileSelection>().yPos;

                if (obsx == x && obsy == y)
                {
                    return true;
                }
            }
        }

        return false;
    
    }
    //This will start the agent and give it the vacuums initial position
    public void PlayAgent() {
        GameObject vacuum = null;
        GameObject[] gridTiles;
        int dirt = dirtCount();
        int x = -1;
        int y = -1;

        if (dirt > 0 && impossible == false)
        {
            //Store all the grid tiles
            gridTiles = GameObject.FindGameObjectsWithTag("Grid");
            foreach (GameObject tile in gridTiles)
            {
                //Set the vacuum tiles position in the grid
                if (tile.GetComponent<TileSelection>().id == 3)
                {
                    vacuum = tile;
                    x = tile.GetComponent<TileSelection>().xPos;
                    y = tile.GetComponent<TileSelection>().yPos;
                }
            }
            //Call the selected algorithm
            if (vacuum != null)
            {
                if (algorithm == 0)
                {
                    vacuum.GetComponent<Clean>().move(vacuum, x, y);
                }
                else if(algorithm == 1)
                {
                    vacuum.GetComponent<CleanAlgo>().move(vacuum, x, y);
                }
            }

            Move.IncreaseMove();
        }
        else
        {
            play = false;
        }


    }
}
