using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CleanAlgo : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject cleanTile;
    public GameObject vacuumTile;

    private List<Tuple<int, int>> visited;
    private List<Tuple<int, int>> obs;
    private List<Tuple<int, int>> currPath;

    public GameObject canvas;


    //public TextMeshPro imp;

    public GameObject getTile(int x, int y)
    {
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

        Debug.Log("Got to the new algorithm");
        if (Play.visited == null)
        {
            visited = new List<Tuple<int, int>>
            {
                Tuple.Create(x, y)
            };

        }
        else
        {
            visited = Play.visited;
        }
        if (Play.obs == null)
        {
            obs = new List<Tuple<int, int>>();

        }
        else
        {
            obs = Play.obs;
        }
        if (Play.currPath == null)
        {
            currPath = new List<Tuple<int, int>>
            {
                Tuple.Create(x, y)
            };

        }
        else
        {
            currPath = Play.currPath;
        }

        Debug.Log($"visited {visited}");

        List<string> actions = new List<string>
        {
            "left",
            "right",
            "up",
            "down"
        };

        //int randNum = UnityEngine.Random.Range(0, 4);
        //Debug.Log($"random {randNum}");

        string action = actions[0];

        bool choosing = true;

        while (choosing)
        {
            Debug.Log("Choosing");

            if (action == "left")
            {
                int newX = x - 1;
                //Check to see if the new position is a valid postion

                Tuple<int, int> nextMove = new Tuple<int, int>(newX, y);

                bool bump = PlayButton.GetComponent<Play>().isBump(newX, y);
                if (bump == false)
                {
                    if (visited.Contains(nextMove) == false)
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
                        visited.Add(nextMove);
                        currPath.Add(nextMove);
                        choosing = false;
                    }
                    else
                    {
                        //Remove the string that we just checked from actions
                        actions.Remove("left");
                        //randNum = UnityEngine.Random.Range(0, actions.Count);

                        action = actions[0];
                    }
                }
                else
                {
                    obs.Add(nextMove);
                    visited.Add(nextMove);
                    //Remove the string that we just checked from actions
                    actions.Remove("left");
                    //randNum = UnityEngine.Random.Range(0, actions.Count);

                    action = actions[0];
                }
            }

            else if (action == "right")
            {
                int newX = x + 1;

                Tuple<int, int> nextMove = new Tuple<int, int>(newX, y);

                bool bump = PlayButton.GetComponent<Play>().isBump(newX, y);
                if (bump == false)
                {
                    if (visited.Contains(nextMove) == false)
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
                        visited.Add(nextMove);
                        currPath.Add(nextMove);
                        choosing = false;
                    }
                    else
                    {
                        //Remove the string that we just checked from actions
                        actions.Remove("right");
                        //randNum = UnityEngine.Random.Range(0, actions.Count);

                        action = actions[0];
                    }
                }
                else
                {
                    obs.Add(nextMove);
                    visited.Add(nextMove);
                    //Remove the string that we just checked from actions
                    actions.Remove("right");
                    //randNum = UnityEngine.Random.Range(0, actions.Count);

                    action = actions[0];
                }
            }
            else if (action == "up")
            {
                int newY = y + 1;
                Tuple<int, int> nextMove = new Tuple<int, int>(x, newY);

                bool bump = PlayButton.GetComponent<Play>().isBump(x, newY);
                if (bump == false)
                {
                    if (visited.Contains(nextMove) == false)
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
                        visited.Add(nextMove);
                        currPath.Add(nextMove);
                        choosing = false;
                    }
                    else
                    {
                        //Remove the string that we just checked from actions
                        actions.Remove("up");
                        //randNum = UnityEngine.Random.Range(0, actions.Count);

                        action = actions[0];
                    }
                }
                else
                {
                    obs.Add(nextMove);
                    visited.Add(nextMove);
                    //Remove the string that we just checked from actions
                    actions.Remove("up");
                    //randNum = UnityEngine.Random.Range(0, actions.Count);

                    action = actions[0];
                }
            }
            else if (action == "down")
            {
                int newY = y - 1;
                Tuple<int, int> nextMove = new Tuple<int, int>(x, newY);

                bool bump = PlayButton.GetComponent<Play>().isBump(x, newY);
                if (bump == false)
                {
                    if (visited.Contains(nextMove) == false)
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
                        visited.Add(nextMove);
                        currPath.Add(nextMove);
                        choosing = false;
                    }
                    else
                    {
                        //Remove the string that we just checked from actions
                        actions.Remove("down");
                        //randNum = UnityEngine.Random.Range(0, actions.Count);

                        action = "";
                    }
                }
                else
                {
                    obs.Add(nextMove);
                    visited.Add(nextMove);
                    //Remove the string that we just checked from actions
                    actions.Remove("down");
                    //randNum = UnityEngine.Random.Range(0, actions.Count);

                    action = "";
                }
            }
            else
            {
                Debug.Log("GETS IN ELSE");
                //This needs to be a backtrack variable
                Tuple<int, int> coords = currPath.Last();
                Debug.Log($"coords {coords.Item1} {coords.Item2}");
                currPath.RemoveAt(currPath.Count - 1);
                GameObject tile = getTile(coords.Item1, coords.Item2);
                cleanTile.GetComponent<TileSelection>().xPos = x;
                cleanTile.GetComponent<TileSelection>().yPos = y;
                Instantiate(cleanTile, vacuum.gameObject.transform.position, cleanTile.transform.rotation);



                vacuumTile.GetComponent<TileSelection>().xPos = coords.Item1;
                vacuumTile.GetComponent<TileSelection>().yPos = coords.Item2;

                Instantiate(vacuumTile, tile.gameObject.transform.position, vacuumTile.transform.rotation);

                tile.GetComponent<Replace>().getRidOf();
                vacuum.GetComponent<Replace>().getRidOf();

                if (currPath.Count == 0)
                {
                    Play.impossible = true;
                    Debug.Log("if");
                    //Debug.Log(canvas.transform.GetChild(1));

                    Debug.Log(canvas.GetComponent<TextMeshProUGUI>().enabled);
                    canvas.GetComponent<TextMeshProUGUI>().enabled = false;
                    Debug.Log("after");


                }

                choosing = false;

            }
        }

        //This is where I will store both the lists in the play script
        Play.visited = visited;
        Play.obs = obs;
        Play.currPath = currPath;
    }
}
