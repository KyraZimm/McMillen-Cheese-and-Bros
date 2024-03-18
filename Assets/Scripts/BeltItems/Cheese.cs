using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : BeltItem {
    [SerializeField] SpriteRenderer qualityFilter; //TEMP: will eventually replace with a good/bad quality animation
    public bool IsGood { get; private set; }

    private bool hasBeenChecked = false;
    private static float[] sniffValues = { 1f, .66f, .47f, .2f, 0f};
    private static int currSniffIndex = 0;

    public void SetQuality(bool cheeseIsGood) {
        IsGood = cheeseIsGood;
        qualityFilter.sprite = SpriteReference.Instance.GetSprite(cheeseIsGood ? "GoodCheeseIndicator" : "BadCheeseIndicator");
        qualityFilter.enabled = false;
    }

    void ShowQuality() {
        qualityFilter.enabled = true;
        qualityFilter.color = IsGood ? new Color(0, 0, 0, sniffValues[currSniffIndex]) : new Color(0, 0, 0, sniffValues[currSniffIndex]);

        if (currSniffIndex < sniffValues.Length - 1)
            currSniffIndex++;
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

    public override ScoreItem AsScoreItem() {
        return new ScoreItem(Type, IsGood);
    }

    public static void RefreshSniff() { currSniffIndex = 0; }

}
