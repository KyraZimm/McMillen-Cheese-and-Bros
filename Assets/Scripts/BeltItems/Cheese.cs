using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : BeltItem {
    public bool IsGood { get; private set; }

    private bool hasBeenChecked = false;

    public void SetQuality(bool cheeseIsGood) {
        IsGood = cheeseIsGood;
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
        Debug.Log("cheese is " + (IsGood ? "good" : "bad"));
    }


}
