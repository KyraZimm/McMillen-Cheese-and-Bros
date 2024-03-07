using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {
    [Header("Items That Can Be Put In Bin")]
    [SerializeField] List<ScoreItem> itemTypesToProcess; //any of these items will be scored by the bin. other items will be put back on the belt.
    [Space(5)]
    [Header("Item That Bin Wants")]
    [SerializeField] ScoreItem desiredItemType; //this is the item type that the bin is actually looking for

    public bool CanProccessItem(BeltItem item) {
        return itemTypesToProcess.Contains(item.AsScoreItem());
    }

    public void PlaceItem(BeltItem item) {
        ScoreKeeper.Instance.ModifyScore(item, desiredItemType);

        //destroy item
        Destroy(item.gameObject);
    }
}
