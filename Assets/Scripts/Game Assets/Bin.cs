using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {
    [SerializeField] List<ScoreItem> itemTypesToProcess; //any of these items will be scored by the bin and removed. other items will be put back on the belt.
    [SerializeField] List<ScoreItem> desiredItemTypes; //this is the item type that the bin is actually looking for

    public bool CanProccessItem(BeltItem item) {
        ScoreItem inputItem = item.AsScoreItem();
        foreach (ScoreItem canProcess in itemTypesToProcess) {
            if (canProcess.Match(inputItem))
                return true;
            Debug.Log("I can process");
        }
        Debug.Log("I cannot process");
        return false;

    }

    public void PlaceItem(BeltItem item) {
        ScoreItem inputItem = item.AsScoreItem();
        foreach (ScoreItem desiredItem in desiredItemTypes) {
            if (desiredItem.Match(inputItem)){
                Debug.Log("I match");
                ScoreKeeper.Instance.ModifyScore(inputItem, desiredItem);
                Destroy(item.gameObject);
                return;
            }
            else
            {
                Debug.Log("I don't match");
            }
        }
        
        //NOTE: this is a messy way to return a desired value, look into refactoring at some point
        ScoreKeeper.Instance.ModifyScore(item.AsScoreItem(), desiredItemTypes[0]);

        //destroy item
        Destroy(item.gameObject);
    }
}
