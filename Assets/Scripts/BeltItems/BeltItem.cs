using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeltItem : MonoBehaviour {
    [SerializeField] ItemTag itemType;
    
    private Rigidbody2D rb;
    private ConveyorBelt parentBelt;

    public ItemTag Type { get { return itemType; } }

    public static BeltItem HeldItem;

    private void Awake() { rb = GetComponent<Rigidbody2D>(); }

    public void Init(ConveyorBelt beltWhichMadeItem) { parentBelt = beltWhichMadeItem; }

    public void MoveToPos(Vector2 targetPos) { rb.MovePosition(targetPos); }

    private void Update() {
        if (HeldItem == this) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveToPos(pos);
        }
    }

    private void OnMouseDown() { MouseDown(); }
    private void OnMouseUp() { MouseUp(); }

    protected virtual void MouseDown() {
        if (HeldItem != null)
            return;

        HeldItem = this;
        parentBelt.RemoveItemFromBelt(this);
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
        //MoveToPos(parentBelt.ProjectOntoBelt(transform.position));
        transform.position = parentBelt.ProjectOntoBelt(transform.position); //NOTE: this is a fast fix, eventually a better state machine for belt items should be implemented
        parentBelt.AddItemToBelt(this);
    }

    public virtual ScoreItem AsScoreItem() {
        return new ScoreItem(Type, false);
    }

}
