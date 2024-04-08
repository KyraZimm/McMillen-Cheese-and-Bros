using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] Transform clockHand;

    private float startingAngle = 90f; //start at 9AM
    private float endingAngle = -150f; //end at 5PM


    private void FixedUpdate() {
        if (LevelManager.Instance.CurrState == LevelManager.LevelState.Playing) {
            float timeRemaining = LevelManager.Instance.ItemSpawnSettings.DayLengthInSeconds - (Time.time - LevelManager.Instance.TimeAtLastStateChange);
            float handRangeTravelled = (LevelManager.Instance.ItemSpawnSettings.DayLengthInSeconds-timeRemaining) / LevelManager.Instance.ItemSpawnSettings.DayLengthInSeconds;
            float handAngle = startingAngle + ((endingAngle - startingAngle) * handRangeTravelled);
            clockHand.rotation = Quaternion.Euler(0, 0, handAngle);
        }
    }

    //Lydia's original code:

    /*public float DayLength; //seconds
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
                                     //Kyra Review: Coroutines like InvokeRepeating are expensive and should probably be replaced with Update() and deltaTime
                                     //handPos.eulerAngles += _someAngularVelocity * (timeYouWantInSeconds * Time.deltaTime);
    }

    // Update is called once per frame
    void CheckTime() 
    {
        if (rotationLeft > 0f)
        {

            //move hand degreesRotation
            handPos.eulerAngles += new Vector3(0, 0, degreesRotation*-1);
            //set new rotationLeft
            rotationLeft = rotationLeft - degreesRotation;

            //Debug.Log(rotationLeft);
        }

        else if (rotationLeft == 0f)
        {
            //do nothing
            Debug.Log("Time's up");
        }

    }*/

}
