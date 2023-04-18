using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{

    public TextMeshProUGUI moves;


    //public Move move;

    // Update is called once per frame
    void Update()
    {
        moves.text = "Moves made: " + Move.move;
    }


}
