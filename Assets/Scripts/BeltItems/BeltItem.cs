using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeltItem : MonoBehaviour {
    [SerializeField] ItemTag itemType;
    
    private Rigidbody2D rb;
    private ConveyorBelt parentBelt;
    public ItemTag Type { get { return itemType; } }

    private void Awake() { rb = GetComponent<Rigidbody2D>(); }

    public void Init(ConveyorBelt beltWhichMadeItem) { parentBelt = beltWhichMadeItem; }

    public void MoveToPos(Vector2 targetPos) { rb.MovePosition(targetPos); }

    public void OnMouseSelect() { parentBelt.RemoveItemFromBelt(this); }

    public void OnMouseDeselect() {
        //check if item should be deposited into bin
        Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        for (int i = 0; i < overlappingColliders.Length; i++) {
            Bin bin = overlappingColliders[i].GetComponent<Bin>();
            if (bin != null) {
                bin.PlaceItem(this);
                return;
            }
        }

        //else, place item back on belt
        MoveToPos(parentBelt.ProjectOntoBelt(transform.position));
    }
}
