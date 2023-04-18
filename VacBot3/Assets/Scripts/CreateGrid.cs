using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public class Node
    {
        public bool isPlaceable;
        public Vector3 cellPosition;
        public Transform obj;

        public Node(bool isPlaceable, Vector3 cellPosition, Transform obj)
        {
            this.isPlaceable = isPlaceable;
            this.cellPosition = cellPosition;
            this.obj = obj;
        }
    }

    public Transform cleanTile;
    [SerializeField] private int height;
    [SerializeField] private int width;
    private Node[,] nodes;


    public void CreateGrid1() {
        nodes = new Node[width, height];
        var name = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Vector3 worldPosition = new Vector3(i-6, j, 8);
                Transform obj = Instantiate(cleanTile, worldPosition, Quaternion.identity);
                obj.name = "Cell " + name;
                obj.transform.eulerAngles = new Vector3(
                    obj.transform.eulerAngles.x + 90,
                    obj.transform.eulerAngles.y,
                    obj.transform.eulerAngles.z
                    );
                obj.GetComponent<TileSelection>().xPos = i;
                obj.GetComponent<TileSelection>().yPos = j;

                nodes[i, j] = new Node(true, worldPosition, obj);
                name++;

            }
        }
    }

    private void Start()
    {
        CreateGrid1();
    }
}
