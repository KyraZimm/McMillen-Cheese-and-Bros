using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField] private float speed;

    public Vector2 BeltVector { get; private set; }
    public Vector2 BeltVectorNormalized { get; private set; }
    public List<BeltItem> ItemsOnBelt { get; private set; }

    public void Awake() {
        ItemsOnBelt = new List<BeltItem>();

        //calculate start and end points of belt
        float widthFromCenter = transform.localScale.x / 2;
        Vector2 start = transform.position + (-transform.right * widthFromCenter);
        Vector2 end = transform.position + (transform.right * widthFromCenter);

        //set belt vector for future caluclations
        BeltVector = end - start;
        BeltVectorNormalized = BeltVectorNormalized.normalized;
    }

    public void AddItemToBelt(BeltItem newItem) {
        ItemsOnBelt.Add(newItem);
        newItem.SetMoveSpeed(BeltVectorNormalized, speed);
    }
}
