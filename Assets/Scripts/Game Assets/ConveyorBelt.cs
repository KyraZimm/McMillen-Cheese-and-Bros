using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour, ILevelLoadField {
    [SerializeField] private float speed;
    [SerializeField] private Transform start;
    [SerializeField] private float beltLength;

    public Vector2 BeltVector { get; private set; }
    public Vector2 BeltVectorNormalized { get; private set; }
    public List<BeltItem> ItemsOnBelt { get; private set; }

    protected Vector2 startPoint;
    protected Vector2 endPoint;

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos() {
        startPoint = (Vector2)start.position;
        endPoint = start.position + (Vector3.right * beltLength);
        Debug.DrawLine(start.position, start.position + (Vector3.right * beltLength), Color.white);
    }
#endif

    protected virtual void Awake() {
        ItemsOnBelt = new List<BeltItem>();

        //calculate start and end points of belt
        startPoint = (Vector2)start.position;
        endPoint = start.position + (Vector3.right * beltLength);

        //set belt vector for future caluclations
        BeltVector = endPoint - startPoint;
        BeltVectorNormalized = BeltVector.normalized;
    }

    protected virtual void FixedUpdate() {
        //move items on belt
        for (int i = ItemsOnBelt.Count-1; i >= 0; i--) {

            //clear out null items
            if (ItemsOnBelt[i] == null) {
                ItemsOnBelt.RemoveAt(i);
                continue;
            }

            Vector2 newTargetPos = (Vector2)ItemsOnBelt[i].transform.position + (BeltVectorNormalized * speed * Time.fixedDeltaTime);
            ItemsOnBelt[i].MoveToPos(newTargetPos);
        }
    }

    void ILevelLoadField.OnLevelLoad(LevelValues levelToLoad) {
        //clear all belts
        foreach (BeltItem item in ItemsOnBelt)
            Destroy(item.gameObject);
    }

    public virtual void AddItemToBelt(BeltItem newItem) {
        if (newItem != null && !ItemsOnBelt.Contains(newItem)) {
            ItemsOnBelt.Add(newItem);
            newItem.transform.position = startPoint;
            newItem.AllowPickup(false);
        }
           
    }
    public virtual void RemoveItemFromBelt(BeltItem oldItem) {
        if (oldItem != null && ItemsOnBelt.Contains(oldItem)) {
            ItemsOnBelt.Remove(oldItem);
            Destroy(oldItem.gameObject); //destroy gameobject at end of belt
        }
    }

}
