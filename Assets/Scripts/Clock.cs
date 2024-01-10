using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float DayLength; //seconds
    public float Countdown; //seconds
    private float degreesRotation; //distance per second
    private float rotationLeft; //remaining distance

    public Transform handPos;

    // Start is called before the first frame update
    void Start()
    {
        degreesRotation = 360 / DayLength;
        // if we want a 9-5 we should move the hand to start at 9 and instead of 360 use 240
        rotationLeft = 360;

        InvokeRepeating("CheckTime", Countdown, 1.0f); //after 3 seconds every 1 second
    }

    // Update is called once per frame
    void CheckTime()
    {
        if (rotationLeft > 0f)
        {

            //move hand degreesRotation
            handPos.eulerAngles += new Vector3(0, 0, degreesRotation*-1);
            //set new timeLeft
            rotationLeft = rotationLeft - degreesRotation;

            Debug.Log(rotationLeft);
        }

        else if (rotationLeft == 0f)
        {
            //do nothing
            Debug.Log("Time's up");
        }

    }

}
