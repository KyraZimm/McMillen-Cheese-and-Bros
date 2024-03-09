using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {
    [SerializeField] List<ScoreItem> itemTypesToProcess; //any of these items will be scored by the bin and removed. other items will be put back on the belt.
    [SerializeField] ScoreItem desiredItemType; //this is the item type that the bin is actually looking for

    public bool CanProccessItem(BeltItem item) {
        return itemTypesToProcess.Contains(item.AsScoreItem());
    }

    public void PlaceItem(BeltItem item) {
        ScoreKeeper.Instance.ModifyScore(item.AsScoreItem(), desiredItemType);

        //destroy item
        Destroy(item.gameObject);
    }
}
