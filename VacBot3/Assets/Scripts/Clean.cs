using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject cleanTile;
    public GameObject vacuumTile;

    string[] actions = { "left", "right", "up", "down" };

    public GameObject getTile(int x, int y) {
        GameObject gridTile = null;
        GameObject[] gridTiles = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject tile in gridTiles)
        {
            if (tile.GetComponent<TileSelection>().xPos == x && tile.GetComponent<TileSelection>().yPos == y)
            {
                gridTile = tile;
                return gridTile;
            }
        }
        return null;

    }

    //This function will look at what is at the current spot and then choose which 
    //way to move next
    public void move(GameObject vacuum, int x, int y)
    {
        int randNum = Random.Range(0, 4);
        Debug.Log($"random {randNum}");

        string action = actions[randNum];

        if (action == "left")
        {
            int newX = x - 1;
            //Check to see if the new position is a valid postion
            bool bump = PlayButton.GetComponent<Play>().isBump(newX, y);
            if (bump == false)
            {
                GameObject tile = getTile(newX, y);
                cleanTile.GetComponent<TileSelection>().xPos = x;
                cleanTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(cleanTile, vacuum.gameObject.transform.position, cleanTile.transform.rotation);

                vacuumTile.GetComponent<TileSelection>().xPos = newX;
                vacuumTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(vacuumTile, tile.gameObject.transform.position, vacuumTile.transform.rotation);

                tile.GetComponent<Replace>().getRidOf();
                vacuum.GetComponent<Replace>().getRidOf();
            }
        }
        else if (action == "right")
        {
            int newX = x + 1;
            bool bump = PlayButton.GetComponent<Play>().isBump(newX, y);
            if (bump == false)
            {
                GameObject tile = getTile(newX, y);
                cleanTile.GetComponent<TileSelection>().xPos = x;
                cleanTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(cleanTile, vacuum.gameObject.transform.position, cleanTile.transform.rotation);

                vacuumTile.GetComponent<TileSelection>().xPos = newX;
                vacuumTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(vacuumTile, tile.gameObject.transform.position, vacuumTile.transform.rotation);

                tile.GetComponent<Replace>().getRidOf();
                vacuum.GetComponent<Replace>().getRidOf();
            }
        }
        else if (action == "up") 
        {
            int newY = y + 1;
            bool bump = PlayButton.GetComponent<Play>().isBump(x, newY);
            if (bump == false)
            {
                GameObject tile = getTile(x, newY);
                cleanTile.GetComponent<TileSelection>().xPos = x;
                cleanTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(cleanTile, vacuum.gameObject.transform.position, cleanTile.transform.rotation);

                vacuumTile.GetComponent<TileSelection>().xPos = x;
                vacuumTile.GetComponent<TileSelection>().yPos = newY;
                Instantiate(vacuumTile, tile.gameObject.transform.position, vacuumTile.transform.rotation);

                tile.GetComponent<Replace>().getRidOf();
                vacuum.GetComponent<Replace>().getRidOf();
            }
        }
        else if (action == "down")
        {
            int newY = y - 1;
            bool bump = PlayButton.GetComponent<Play>().isBump(x, newY);
            if (bump == false)
            {
                GameObject tile = getTile(x, newY);
                cleanTile.GetComponent<TileSelection>().xPos = x;
                cleanTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(cleanTile, vacuum.gameObject.transform.position, cleanTile.transform.rotation);

                vacuumTile.GetComponent<TileSelection>().xPos = x;
                vacuumTile.GetComponent<TileSelection>().yPos = newY;
                Instantiate(vacuumTile, tile.gameObject.transform.position, vacuumTile.transform.rotation);

                tile.GetComponent<Replace>().getRidOf();
                vacuum.GetComponent<Replace>().getRidOf();
            }
        }
    }
}
