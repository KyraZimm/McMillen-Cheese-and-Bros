using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeltItem : MonoBehaviour {
    [SerializeField] ItemType itemType;
    [SerializeField] Rigidbody rb;

    public enum ItemType { Cheddar, Gruyere, Trash, Recycling, Compost }
    public ItemType Type { get { return itemType; } }

    private Vector2 moveUnitPerSecond;

    public void Init() {
        moveUnitPerSecond = Vector2.zero;
    }

    public void SetMoveSpeed(Vector2 dir, float speed) { moveUnitPerSecond = dir.normalized * speed; }
    private void FixedUpdate() {
        if (moveUnitPerSecond != null && moveUnitPerSecond != Vector2.zero)
            rb.MovePosition(moveUnitPerSecond * Time.fixedDeltaTime);
    }
}
