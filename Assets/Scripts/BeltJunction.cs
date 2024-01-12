using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private string itemTag;
    private string beltSetting;

    private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();
        Cheese cheese = collision.GetComponent<Cheese>();
        itemTag = collision.gameObject.tag;
        beltSetting = gameObject.tag;

        if (item == null)
            return;

        if (item is Cheese) 
        {
            if  (cheese.IsGood == false)
            {
                //if bad send message to the ScoreKeeper that a bad cheese was sent to the belts
            }
            if (cheese.IsGood == true && itemTag == beltSetting)
            {
                //if matches the junction, send message to ScoreKeeper that a good cheese was sent to the correct belt
            }
            if (cheese.IsGood == true && itemTag != beltSetting)
            {
                //if doesn't match, send a message to ScoreKeeper that a good cheese was sent to the incorrect belt
            }
        }
        else 
        {
            //send message to the ScoreKeeper that a non-cheese item got sent to the belts
        }

    }
}
