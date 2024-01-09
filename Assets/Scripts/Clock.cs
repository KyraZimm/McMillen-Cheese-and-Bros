using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public bool clockIsRunning;
    public float DayLength;
    private float secondsToDegrees;

    public Transform handPos;

    public float startPos;
    public float endPos;

    // Start is called before the first frame update
    void Start()
    {
        handPos.Rotate(0f, 0f, startPos);
        clockIsRunning = true;
        //set the hand to startPos

        
    }

    // Update is called once per frame
    void Update()
    {
        // D = r*t
 //       handPos.Rotate(0f, 0f, );

  //      if    handPos.Rotate(0f, 0f, endPos);
  //      {
  //          Debug.log("Shift over!");
  //      }
        //stop the hand at end pos

        //end the level
    }
}
