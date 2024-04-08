using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Transform spawnPoint;

    public Vector2 BeltVector { get; private set; }
    public Vector2 BeltVectorNormalized { get; private set; }
    public List<BeltItem> ItemsOnBelt { get; private set; }

    private float timeLastItemSpawned = 0;
    private Vector2 startPoint;
    private Vector2 endPoint;

    private void Awake() {
        ItemsOnBelt = new List<BeltItem>();

        //calculate start and end points of belt
        //float widthFromCenter = Mathf.Abs(transform.position.x - spawnPoint.position.x);
        float widthFromCenter = gameObject.GetComponent<BoxCollider2D>().size.x;
        startPoint = (Vector2)spawnPoint.position;
        endPoint = spawnPoint.position + (transform.right * 2 * widthFromCenter);

        //set belt vector for future caluclations
        BeltVector = endPoint - startPoint;
        BeltVectorNormalized = BeltVector.normalized;
    }

    private void FixedUpdate() {
        if (LevelManager.Instance.CurrState != LevelManager.LevelState.Playing)
            return;

        //move items on belt
        for (int i = ItemsOnBelt.Count-1; i >= 0; i--) {

            //clear out null items
            if (ItemsOnBelt[i] == null) {
                RemoveItemFromBelt(ItemsOnBelt[i]);
                continue;
            }

            Vector2 newTargetPos = (Vector2)ItemsOnBelt[i].transform.position + (BeltVectorNormalized * speed * Time.fixedDeltaTime);
            ItemsOnBelt[i].MoveToPos(newTargetPos);
        }

        //if ready, spawn new cheese
        if (Time.time - timeLastItemSpawned >= LevelManager.Instance.ItemSpawnSettings.SpawnInterval) {
            SpawnItem();
            timeLastItemSpawned = Time.time;
        }
    }

    private void SpawnItem() {
        //get random item to spawn
        string newItemToSpawn = LevelManager.Instance.ItemSpawnSettings.GetRandomItem();
        BeltItem newItem = Instantiate(ItemReference.Instance.ItemPrefabs[newItemToSpawn], spawnPoint.position, Quaternion.identity).GetComponent<BeltItem>();

        //if item is cheese, determine quality
        if (newItem is Cheese) {
            Cheese newCheese = newItem as Cheese;
            newCheese.SetQuality(Random.Range(0f, 1f) >= LevelManager.Instance.ItemSpawnSettings.ChanceOfBadCheese);
        }

        //initialize new item, put on belt
        newItem.Init(this);
        AddItemToBelt(newItem);
    }

    public void AddItemToBelt(BeltItem newItem) {
        if (newItem != null && !ItemsOnBelt.Contains(newItem))
            ItemsOnBelt.Add(newItem);
    }
    public void RemoveItemFromBelt(BeltItem oldItem) {
        if(oldItem != null && ItemsOnBelt.Contains(oldItem))
            ItemsOnBelt.Remove(oldItem);
    }

    /*private void OnTriggerEnter2D(Collider2D col) {
        BeltItem item = col.GetComponent<BeltItem>();
        if (item != null && !ItemsOnBelt.Contains(item))
            AddItemToBelt(item);
    }

    private void OnTriggerExit2D(Collider2D col) {
        BeltItem item = col.GetComponent<BeltItem>();
        if (item != null && ItemsOnBelt.Contains(item))
            RemoveItemFromBelt(item);
    }*/

    public Vector2 ProjectOntoBelt(Vector2 pointToProject) {
        //belt is always completely horizontal, so we can just snap to the y-coord
        Vector2 proj = pointToProject;
        proj.y = startPoint.y;
        if (proj.x < startPoint.x)
            proj.x = startPoint.x;
        else if (proj.x > endPoint.x)
            proj.x = endPoint.x;
        return proj;
    }
}
