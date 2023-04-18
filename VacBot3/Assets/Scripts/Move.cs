using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static int move = 0;


    public static void IncreaseMove() 
    {
        move += 1;
    }

    public static void resetMove()
    {
        move = 0;
    }
}
