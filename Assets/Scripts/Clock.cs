using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float DayLength;
    private float degreesRotation;
    private float timeLeft;

    public Transform handPos;

    // Start is called before the first frame update
    void Start()
    {
        //get handPos rotation in z
        //get DayLength

        degreesRotation = 360f / DayLength;
        // if we want a 9-5 we should move the hand to start at 9 and instead of 360 use 240
        timeLeft = 360f;
        //timeLeft = 360 which is assuming that one round of the timer is a full rotation from 12 to 12
            // timeLeft = 240 if we have an 8 hours shift
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0f)
        {
            //move hand degreesRotation
            handPos.eulerAngles += new Vector3(0, 0, timeLeft - degreesRotation);
            //set new timeLeft
            timeLeft = timeLeft - degreesRotation;
            Debug.Log(timeLeft);
        }

        else if (timeLeft == 0f)
        {
            //do nothing
            Debug.Log("Time's up");
        }

    }

}
