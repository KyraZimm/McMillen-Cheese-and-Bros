using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();

        if (item == null)
            return;

        if (item is Cheese) {
            //if good, do thing
            //if bad, do thing
        }
        else {

        }
        

    }
}
