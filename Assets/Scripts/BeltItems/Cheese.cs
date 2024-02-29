using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : BeltItem {
    [SerializeField] SpriteRenderer qualityFilter; //TEMP: will eventually replace with a good/bad quality animation
    public bool IsGood { get; private set; }

    private bool hasBeenChecked = false;

    public void SetQuality(bool cheeseIsGood) {
        IsGood = cheeseIsGood;
        qualityFilter.color = cheeseIsGood ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f);
        qualityFilter.enabled = false;
    }

    protected override void MouseDown() {
        if (!hasBeenChecked)
            return;

        base.MouseDown();
    }
    protected override void MouseUp() {
        if (!hasBeenChecked) {
            ShowQuality();
            hasBeenChecked = true;
        }
        else {
            base.MouseUp();
        }
    }

    void ShowQuality() {
        qualityFilter.enabled = true;
    }

    public override bool Match(BeltItem itemToCompare) {
        if (itemToCompare is not Cheese)
            return false;

        Cheese cheeseToCompare = (Cheese)itemToCompare;
        return (cheeseToCompare.Type == this.Type && cheeseToCompare.IsGood == this.IsGood);
    }
    public override bool Match(ItemTag itemToCompare, bool lookingForGoodCheese) {
        return (itemToCompare == this.Type && lookingForGoodCheese == this.IsGood);
    }
}
