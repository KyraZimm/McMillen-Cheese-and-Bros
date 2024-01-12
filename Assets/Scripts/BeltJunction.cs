using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private string itemTag;
    private ItemTag beltSetting;

    private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();
        ScoreKeeper.Instance.ModifyScore(item, beltSetting);
    }
}
