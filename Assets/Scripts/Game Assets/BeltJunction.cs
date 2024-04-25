using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private ItemTag beltSetting;
    public SpriteRenderer lever;

    [SerializeField] ConveyorBelt gruyereBelt;
    [SerializeField] ConveyorBelt cheddarBelt;
    [SerializeField] SpriteRenderer cheeseSelectedWindow;

    private void Awake() { ChangeBeltSetting(true); }

    //edit: exclusively let interactable belts hand things to junctions, since we don't want other belts to accidentally overlap
    /*private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();
        if (item != null) {
            ScoreKeeper.Instance.ModifyScore(item.AsScoreItem(), new ScoreItem(beltSetting, true));
            if (beltSetting == ItemTag.Gruyere)
                gruyereBelt.AddItemToBelt(item);
            else
                cheddarBelt.AddItemToBelt(item);
        }
    }*/

    public void SortItem(BeltItem itemToSort) {
        if (itemToSort == null)
            return;

        ScoreKeeper.Instance.ModifyScore(itemToSort.AsScoreItem(), new ScoreItem(beltSetting, true));
        if (beltSetting == ItemTag.Gruyere)
            gruyereBelt.AddItemToBelt(itemToSort);
        else
            cheddarBelt.AddItemToBelt(itemToSort);
    }

    private void OnMouseDown() { ChangeBeltSetting(!(beltSetting == ItemTag.Cheddar)); }

    private void ChangeBeltSetting(bool isCheddarNotGruyere) {
        beltSetting = isCheddarNotGruyere ? ItemTag.Cheddar : ItemTag.Gruyere;
        lever.flipY = isCheddarNotGruyere;
        cheeseSelectedWindow.sprite = isCheddarNotGruyere ? SpriteReference.Instance.GetSprite("Cheddar") : SpriteReference.Instance.GetSprite("Gruyere");
    }
}
