using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeltItem : MonoBehaviour {
    [SerializeField] ItemType itemType;
    
    private Rigidbody2D rb;
    private Vector2 lastConfirmedPos;

    public enum ItemType { Cheddar, Gruyere, Trash, Recycling, Compost }
    public ItemType Type { get { return itemType; } }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveToPos(Vector2 targetPos) {
        rb.MovePosition(targetPos);
        lastConfirmedPos = targetPos;
    }


}
