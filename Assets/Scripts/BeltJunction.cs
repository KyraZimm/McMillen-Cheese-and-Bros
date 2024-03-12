using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJunction : MonoBehaviour {

    private ItemTag beltSetting;
    public SpriteRenderer lever;

    [Header("TEMP: UI display")]
    [SerializeField] SpriteRenderer cheeseSelectedWindow;
    [SerializeField] Sprite cheddarSprite;
    [SerializeField] Sprite grueyereSprite;

    private void Awake() {
        ChangeBeltSetting(true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BeltItem item = collision.GetComponent<BeltItem>();
        if (item != null) {
            ScoreKeeper.Instance.ModifyScore(item.AsScoreItem(), new ScoreItem(beltSetting, true));
            Destroy(item.gameObject);
        }
    }

    private void OnMouseDown() {
        ChangeBeltSetting(!(beltSetting == ItemTag.Cheddar));
    }

    private void ChangeBeltSetting(bool isCheddarNotGruyere) {
        beltSetting = isCheddarNotGruyere ? ItemTag.Cheddar : ItemTag.Gruyere;
        lever.flipX = isCheddarNotGruyere;
        cheeseSelectedWindow.sprite = isCheddarNotGruyere ? cheddarSprite : grueyereSprite;
    }
}
