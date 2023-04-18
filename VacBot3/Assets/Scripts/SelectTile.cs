using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public Color goodlaserColor = Color.blue;
    public Color badlaserColor = Color.red;
    public float laserThickness = 0.05f;
    public float rayCastDistance = 15;
    public OVRInput.Button button = OVRInput.Button.Two;

    private GameObject laserCube;
    private MeshRenderer rend;

    public delegate void NewTile(int tileId);
    public static event NewTile NewTileSelected;
    public int selected;
    public GameObject dirt;
    public GameObject obstacle;
    public GameObject clean;
    public GameObject vacuum;
    public GameObject reset;
    public GameObject play;
    private int maxVacuum = 1;
    public int maxObs = 7;

    private int numVacuum = 0;
    private int numObs = 0;

    public Material selectedMat;
    public Material notSelectedMat;

    public GameObject random;
    public GameObject backtrack;
    public GameObject canvas;

    public bool playbool = false;





    void Start()
    {
        //create laser to represent raycast
        laserCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        laserCube.transform.parent = transform;

        //disable the laser initially
        laserCube.transform.localScale = new Vector3(0f, 0f, 0f);
        laserCube.transform.localPosition = new Vector3(0f, 0f, 0f);

        //set renderer poroperties of laser
        rend = laserCube.GetComponent<MeshRenderer>();
        rend.material = new Material(Shader.Find("Unlit/Color"));
        rend.material.color = badlaserColor;
        laserCube.GetComponent<BoxCollider>().enabled = false;

    }



    void FixedUpdate()
    {

        CastRayForSelection();

    }

    void CastRayForSelection()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
  
        bool hit = Physics.Raycast(ray, out hitInfo, rayCastDistance);
        float distance = hit ? hitInfo.distance : rayCastDistance;
        TileSelection selection = null;

        if (hitInfo.collider != null)
            selection = hitInfo.collider.gameObject.GetComponent<TileSelection>();


        if (OVRInput.Get(button))
        {
            laserCube.transform.localScale = new Vector3(laserThickness, laserThickness, distance);
            laserCube.transform.localPosition = new Vector3(0, 0, distance / 2f);

            //rend.material.color = laserColor;
            if (selection != null)
            {
                rend.material.color = goodlaserColor;

            }
            else
            {
                rend.material.color = badlaserColor;
            }
        }
        else
        {
            laserCube.transform.localScale = new Vector3(0f, 0f, 0f);
            laserCube.transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        if (OVRInput.GetUp(button))
        {
            if (hit && selection != null && selection.CompareTag("SelectionMenu"))
            {
                //Debug.Log(selection.GetId());
                selected = selection.GetId();
                //some event that changes the selected shape
                NewTileSelected?.Invoke(selection.GetId());

            }
            else if (hit && selection != null) {
                if (selected != selection.GetId())
                {

                    int Posx = selection.gameObject.GetComponent<TileSelection>().xPos;    
                    int Posy = selection.gameObject.GetComponent<TileSelection>().yPos;

                    //This is the dirt tile
                    if (selected == 1 && selection.CompareTag("Grid") && playbool == false)
                    {
                        dirt.GetComponent<TileSelection>().xPos = Posx;
                        dirt.GetComponent<TileSelection>().yPos = Posy;
                        Instantiate(dirt, selection.gameObject.transform.position, dirt.transform.rotation);
                        if (selection.gameObject.GetComponent<TileSelection>().id == 3)
                        {
                            numVacuum--;
                        }
                        else if(selection.gameObject.GetComponent<TileSelection>().id == 2)
                        {
                            numObs--;
                        }
                        selection.GetComponent<Replace>().getRidOf();
                    }
                    //This is the obstacle
                    else if (selected == 2 && selection.CompareTag("Grid") && playbool == false)
                    {
                        if (numObs < maxObs)
                        {
                            obstacle.GetComponent<TileSelection>().xPos = Posx;
                            obstacle.GetComponent<TileSelection>().yPos = Posy;
                            var newPos = selection.gameObject.transform.position;

                            Instantiate(obstacle, newPos, obstacle.transform.rotation);

                            if (selection.gameObject.GetComponent<TileSelection>().id == 3) {
                                numVacuum--;
                            }
                            selection.GetComponent<Replace>().getRidOf();

                            numObs++;
                        }
                    }
                    //This is the vacuum tile
                    else if (selected == 3 && selection.CompareTag("Grid") && playbool == false)
                    {
                        if (numVacuum < maxVacuum)
                        {
                            vacuum.GetComponent<TileSelection>().xPos = Posx;
                            vacuum.GetComponent<TileSelection>().yPos = Posy;
                            Instantiate(vacuum, selection.gameObject.transform.position, vacuum.transform.rotation);
                            if(selection.gameObject.GetComponent<TileSelection>().id == 2)
                            {
                                numObs--;
                            }
                            selection.GetComponent<Replace>().getRidOf();
                            numVacuum++;
                        }
                    }
                    //This is the clean tile
                    else if (selected == 0 && selection.CompareTag("Grid") && playbool == false)
                    {
                        clean.GetComponent<TileSelection>().xPos = Posx;
                        clean.GetComponent<TileSelection>().yPos = Posy;

                        Instantiate(clean, selection.gameObject.transform.position, clean.transform.rotation);
                        if (selection.gameObject.GetComponent<TileSelection>().id == 3)
                        {
                            numVacuum--;
                        }
                        else if(selection.gameObject.GetComponent<TileSelection>().id == 2)
                        {
                            numObs--;
                        }
                        selection.GetComponent<Replace>().getRidOf();
                    }
                    //This is the reset block
                    else if (selection.CompareTag("Reset"))
                    {
                        reset.GetComponent<ResetGrid>().Reset();
                        numVacuum = 0;
                        numObs = 0;
                        Play.impossible = false;
                        canvas.SetActive(false);
                        playbool = false;
                        Play.visited = null;
                        Play.obs = null;
                        Play.currPath = null;  
                    }
                    else if (selection.CompareTag("Play") && playbool == false)
                    {
                        play.GetComponent<Play>().play = true;
                        play.GetComponent<Play>().PlayAgent();
                        playbool = true;
                    }
                    else if (selection.CompareTag("Random"))
                    {
                        Play.algorithm = 0;
                        random.GetOrAddComponent<Renderer>().material = selectedMat;
                        backtrack.GetOrAddComponent<Renderer>().material = notSelectedMat;

                    }
                    else if (selection.CompareTag("Backtrack"))
                    {
                        Play.algorithm = 1;
                        backtrack.GetOrAddComponent<Renderer>().material = selectedMat;
                        random.GetOrAddComponent<Renderer>().material = notSelectedMat;

                    }
                }

            }
        }
    }
}