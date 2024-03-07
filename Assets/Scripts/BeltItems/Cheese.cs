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

    public override ScoreItem AsScoreItem() {
        return new ScoreItem(Type, IsGood);
    }
}
