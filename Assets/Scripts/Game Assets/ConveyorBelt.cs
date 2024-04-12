using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Transform start;

    public Vector2 BeltVector { get; private set; }
    public Vector2 BeltVectorNormalized { get; private set; }
    public List<BeltItem> ItemsOnBelt { get; private set; }

    protected Vector2 startPoint;
    protected Vector2 endPoint;

    protected virtual void Awake() {
        ItemsOnBelt = new List<BeltItem>();

        //calculate start and end points of belt
        //float widthFromCenter = Mathf.Abs(transform.position.x - spawnPoint.position.x);
        float widthFromCenter = gameObject.GetComponent<BoxCollider2D>().size.x;
        startPoint = (Vector2)start.position;
        endPoint = start.position + (transform.right * 2 * widthFromCenter);

        //set belt vector for future caluclations
        BeltVector = endPoint - startPoint;
        BeltVectorNormalized = BeltVector.normalized;
    }

    protected virtual void FixedUpdate() {
        if (LevelManager.Instance.CurrState != LevelManager.LevelState.Playing)
            return;

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

    public virtual void AddItemToBelt(BeltItem newItem) {
        if (newItem != null && !ItemsOnBelt.Contains(newItem)) {
            ItemsOnBelt.Add(newItem);
            newItem.transform.position = startPoint;
        }
           
    }
    public virtual void RemoveItemFromBelt(BeltItem oldItem) {
        if (oldItem != null && ItemsOnBelt.Contains(oldItem)) {
            ItemsOnBelt.Remove(oldItem);
            Destroy(oldItem.gameObject); //destroy gameobject at end of belt
        }
    }

}
