using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : BeltItem {
    [SerializeField] SpriteRenderer qualityFilter; //TEMP: will eventually replace with a good/bad quality animation
    public bool IsGood { get; private set; }

    //sniff-related properties
    private static float[] sniffValues = { 1f, .66f, .47f, .2f, 0f};
    private static int currSniffIndex = 0;
    private int sniffIndex = sniffValues.Length;

    public void SetQuality(bool cheeseIsGood) {
        IsGood = cheeseIsGood;
        qualityFilter.sprite = SpriteReference.Instance.GetSprite(cheeseIsGood ? "GoodCheeseIndicator" : "BadCheeseIndicator");
        qualityFilter.enabled = false;
    }


    void ShowQuality() {
        if (sniffIndex < currSniffIndex)
            return;

        qualityFilter.enabled = true;
        qualityFilter.color = new Color(1, 1, 1, sniffValues[currSniffIndex]);
        sniffIndex = currSniffIndex;

        if (currSniffIndex < sniffValues.Length - 1)
            currSniffIndex++;
    }

    protected override void MouseUp() {
        //if cheese was never held, show quality
        if (HeldItem != this)
            ShowQuality();
        
        base.MouseUp();
    }

    public override ScoreItem AsScoreItem() {
        return new ScoreItem(Type, IsGood);
    }

    public static void RefreshSniff() { currSniffIndex = 0; }

}
