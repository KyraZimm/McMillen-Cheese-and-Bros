using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private string itemTag;
    public ItemTag beltSetting;

    private void Start()
    {
        beltSetting = ItemTag.Cheddar;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();
        ScoreKeeper.Instance.ModifyScore(item, beltSetting);
    }

    private void OnMouseDown()
    {
        if (beltSetting == ItemTag.Cheddar) // if the belt is set to cheddar
        {
            //set junction tag to gruyere
            beltSetting = ItemTag.Gruyere;
            Debug.Log(beltSetting);
        }
        else if (beltSetting == ItemTag.Gruyere) // if the belt is set to gruyere
        {

            //set junction tag to cheddar
            beltSetting = ItemTag.Cheddar;
            Debug.Log(beltSetting);
        }
    }
}
