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
        //get DayLength
        //360 / DayLength = degreesRotation per frame
            // if we want a 9-5 we should move the hand to start at 9 and instead of 360 use 240
        //timeLeft = 360 which is assuming that one round of the timer is a full rotation from 12 to 12
            // timeLeft = 240 if we have an 8 hours shift
    }

    // Update is called once per frame
    void Update()
    {
        //if timeLeft > 0 
        //move hand degreesRotation
            // which means hand 0,0,Z rotation - 0,0,degreesRotation
        //timeLeft - degreesRotation
       
        //else if timeLeft = 0
        //stopHand
    }

}
