using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public int xPos;
    [SerializeField] public int yPos;

    public int GetId()
    {
        return id;
    }
}
