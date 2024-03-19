using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : BeltItem {
    [SerializeField] SpriteRenderer qualityFilter; //TEMP: will eventually replace with a good/bad quality animation
    public bool IsGood { get; private set; }

    //sniff-related properties
    private static float[] sniffValues = { 1f, .66f, .47f, .2f, 0f};
    private static int currSniffIndex = 0;
    private int sniffIndex;

    /*//input control
    private float timeAtClick;
    private const float TIME_FOR_LONG_CLICK = .2f;*/

    //to check for state of movement
    private bool flaggedAsPotentialHeldItem = false;
    private Vector2 mousePosAtFlagging;

    public void SetQuality(bool cheeseIsGood) {
        IsGood = cheeseIsGood;
        qualityFilter.sprite = SpriteReference.Instance.GetSprite(cheeseIsGood ? "GoodCheeseIndicator" : "BadCheeseIndicator");
        qualityFilter.enabled = false;
    }

    protected override void Update() {
        //if mouse has moved while clicking on cheese, treat this as a held item
        if (flaggedAsPotentialHeldItem) {
            float mouseDistMoved = Vector2.Distance(mousePosAtFlagging, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (mouseDistMoved > 0.01f) {
                base.MouseDown();
            }
        }

        base.Update();
    }

    void ShowQuality() {
        qualityFilter.enabled = true;
        qualityFilter.color = new Color(1, 1, 1, sniffValues[currSniffIndex]);
        sniffIndex = currSniffIndex;

        if (currSniffIndex < sniffValues.Length - 1)
            currSniffIndex++;
    }

    protected override void MouseDown() {
        flaggedAsPotentialHeldItem = true;
        mousePosAtFlagging = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    protected override void MouseUp() {
        if (HeldItem != this)
            ShowQuality();

        flaggedAsPotentialHeldItem = false;
        base.MouseUp();
    }

    public override ScoreItem AsScoreItem() {
        return new ScoreItem(Type, IsGood);
    }

    public static void RefreshSniff() { currSniffIndex = 0; }

}
