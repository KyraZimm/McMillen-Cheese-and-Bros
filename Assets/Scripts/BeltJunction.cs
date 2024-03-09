using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private string itemTag;
    public ItemTag beltSetting;
    public SpriteRenderer lever;

    [Header("TEMP: UI display")]
    [SerializeField] SpriteRenderer cheeseSelectedWindow;
    [SerializeField] Sprite cheddarSprite;
    [SerializeField] Sprite grueyereSprite;

    private void Start() {
        beltSetting = ItemTag.Cheddar; 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();
        ScoreKeeper.Instance.ModifyScore(item.AsScoreItem(), new ScoreItem(beltSetting, true));
    }

    private void OnMouseDown()
    {
        if (beltSetting == ItemTag.Cheddar) // if the belt is set to cheddar
        {
            //set junction tag to gruyere
            beltSetting = ItemTag.Gruyere;
            
        }
        else if (beltSetting == ItemTag.Gruyere) // if the belt is set to gruyere
        {
            //set junction tag to cheddar
            beltSetting = ItemTag.Cheddar;
        }

        lever.flipX = beltSetting == ItemTag.Cheddar ? true : false;
        Debug.Log(beltSetting);

        cheeseSelectedWindow.sprite = beltSetting == ItemTag.Cheddar ? cheddarSprite : grueyereSprite;
    }
}
