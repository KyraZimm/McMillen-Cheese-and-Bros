using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //check for messages from other scripts (BeltJunction, TrashBin, RecyclingBin, CompostBin)
        //reference the scriptable object for how much the score changes because of that message
        //change the score by that amount


    }

    //Options for outcome
        // Good cheese, disposed of correctly
        // Good cheese, disposed of incorrectly
        // Bad cheese, disposed of correctly
        // Bad cheese, disposed of incorrectly
        // Trash, disposed of correctly
        // Trash disposed of incorrectly
        // Recycling, disposed of correctly
        // Recycling disposed of incorrectly
        // Compost, disposed of correctly
        // Compost disposed of incorrectly

    // Handled by BeltJuntion
        // Bad cheese on belt
        // Good cheese, correct belt
        // Good cheese, wrong belt
        // Trash on belt
        // Recycling on belt
        // Compost on belt

}
