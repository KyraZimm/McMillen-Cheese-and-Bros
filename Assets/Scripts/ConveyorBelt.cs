using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField] private float speed;

    public Vector2 BeltVector { get; private set; }
    public Vector2 BeltVectorNormalized { get; private set; }
    public List<BeltItem> ItemsOnBelt { get; private set; }

    private void Awake() {
        ItemsOnBelt = new List<BeltItem>();

        //calculate start and end points of belt
        float widthFromCenter = transform.localScale.x / 2;
        Vector2 start = transform.position + (-transform.right * widthFromCenter);
        Vector2 end = transform.position + (transform.right * widthFromCenter);

        //set belt vector for future caluclations
        BeltVector = end - start;
        BeltVectorNormalized = BeltVector.normalized;
    }

    private void FixedUpdate() {
        foreach (BeltItem item in ItemsOnBelt) {
            Vector2 newTargetPos = (Vector2)item.transform.position + (BeltVectorNormalized * speed * Time.fixedDeltaTime);
            item.MoveToPos(newTargetPos);
        }
    }

    public void AddItemToBelt(BeltItem newItem) {
        ItemsOnBelt.Add(newItem);
    }
    public void RemoveItemFromBelt(BeltItem oldItem) {
        ItemsOnBelt.Remove(oldItem);
    }

    public void OnTriggerEnter2D(Collider2D col) {
        BeltItem item = col.GetComponent<BeltItem>();
        if (item != null && !ItemsOnBelt.Contains(item))
            AddItemToBelt(item);
    }

    public void OnTriggerExit2D(Collider2D col) {
        BeltItem item = col.GetComponent<BeltItem>();
        if (item != null && ItemsOnBelt.Contains(item))
            RemoveItemFromBelt(item);
    }
}
