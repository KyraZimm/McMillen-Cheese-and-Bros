using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeltItem : MonoBehaviour {
    [SerializeField] ItemTag itemType;
    
    private Rigidbody2D rb;
    private Vector2 lastConfirmedPos;
    public ItemTag Type { get { return itemType; } }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveToPos(Vector2 targetPos) {
        rb.MovePosition(targetPos);
        lastConfirmedPos = targetPos;
    }


}
