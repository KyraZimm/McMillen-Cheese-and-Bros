using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {
    [Header("Items That Can Be Put In Bin")]
    [SerializeField] List<ItemTag> itemTypesToProcess;
    [SerializeField] bool canProcessGoodCheese;
    [SerializeField] bool canProcessBadCheese;
    [Space(5)]
    [Header("Items That Bin Wants")]
    [SerializeField] List<ItemTag> desiredItemTypes;
    [SerializeField] bool desiresGoodCheese; //if cheese is not on the list of desired items, this setting doesn't matter

    public bool CanProccessItem(BeltItem item) {
        if (itemTypesToProcess.Contains(item.Type)) {
            if (item is not Cheese) {
                return true;
            }
            else {
                Cheese cheese = (Cheese)item;
                if ((cheese.IsGood && canProcessGoodCheese) || (!cheese.IsGood && canProcessBadCheese)) {
                    return true;
                }
            }
        }

        //if item is not to be processed, reject it
        return false;
    }

    public void PlaceItem(BeltItem item) {
        //NOTE: this is a messy way to pass items to the scorekeeper. Should revisit this and clean it up once the ScoreKeeper is refactored

        //pass to scorekeeper to score the item
        if (desiredItemTypes.Contains(item.Type))
            ScoreKeeper.Instance.ModifyScore(item, item.Type, desiresGoodCheese); //if the bin desires this item type, make sure it is scored as correct input
        else
            ScoreKeeper.Instance.ModifyScore(item, desiredItemTypes[0], desiresGoodCheese); //if the bin does not want this item type, score it against the 1st priority type

        //destroy item
        Destroy(item.gameObject);
    }
}
