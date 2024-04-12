using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeltItem : MonoBehaviour {

    [SerializeField] ItemTag itemType;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;

    public ItemTag Type { get { return itemType; } }
    public Rigidbody2D RB { get { return rb; } }
    public Collider2D Col { get { return col; } }


    private ConveyorBelt parentBelt;
    private bool flaggedAsPotentialHeldItem = false;
    private Vector2 mousePosAtFlagging;
    private bool allowPickup = true;

    public static BeltItem HeldItem;

    public void Init(ConveyorBelt beltWhichMadeItem) { parentBelt = beltWhichMadeItem; }

    public void MoveToPos(Vector2 targetPos) { rb.MovePosition(targetPos); }

    protected virtual void Update() {
        if (!allowPickup)
            return;

        //if mouse has moved while clicking on item, treat this as a held item
        if (flaggedAsPotentialHeldItem) {
            float mouseDistMoved = Vector2.Distance(mousePosAtFlagging, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (mouseDistMoved > 0.05f) {
                HeldItem = this;
                parentBelt.RemoveItemFromBelt(this);
            }
        }

        //if item is held, move with mouse
        if (HeldItem == this) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveToPos(pos);
        }
    }

    private void OnMouseDown() { MouseDown(); }
    private void OnMouseUp() { MouseUp(); }

    protected virtual void MouseDown() {
        if (!allowPickup)
            return;

        flaggedAsPotentialHeldItem = true;
        mousePosAtFlagging = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void MouseUp() {
        if (HeldItem != this)
            return;

        HeldItem = null;

        //check if item should be deposited into bin
        Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        for (int i = 0; i < overlappingColliders.Length; i++) {
            Bin bin = overlappingColliders[i].GetComponent<Bin>();
            if (bin != null && bin.CanProccessItem(this)) {
                bin.PlaceItem(this);
                return;
            }
        }

        //else, place item back on belt
        parentBelt.AddItemToBelt(this);
    }

    public virtual ScoreItem AsScoreItem() {
        return new ScoreItem(Type, false);
    }

}
