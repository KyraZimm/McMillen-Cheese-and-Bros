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
        float widthFromCenter = transform.position.x - spawnPoint.position.x;
        startPoint = (Vector2)spawnPoint.position;
        endPoint = spawnPoint.position + (transform.right * 2 * widthFromCenter);

        //set belt vector for future caluclations
        BeltVector = endPoint - startPoint;
        BeltVectorNormalized = BeltVector.normalized;
    }

    private void FixedUpdate() {
        if (LevelSettings.Instance.CurrState != LevelSettings.LevelState.Playing)
            return;

        foreach (BeltItem item in ItemsOnBelt) {
            Vector2 newTargetPos = (Vector2)item.transform.position + (BeltVectorNormalized * speed * Time.fixedDeltaTime);
            item.MoveToPos(newTargetPos);
        }

        if (Time.time - timeLastItemSpawned >= LevelSettings.Instance.ItemSpawnSettings.SpawnInterval) {
            SpawnItem();
            timeLastItemSpawned = Time.time;
        }
    }

    private void SpawnItem() {
        //get random item to spawn
        ItemTag newItemToSpawn = LevelSettings.Instance.ItemSpawnSettings.GetRandomItem();
        BeltItem newItem = Instantiate(ItemReference.Instance.ItemPrefabs[newItemToSpawn], spawnPoint.position, Quaternion.identity).GetComponent<BeltItem>();

        //if item is cheese, determine quality
        if (newItem is Cheese) {
            Cheese newCheese = newItem as Cheese;
            newCheese.SetQuality(Random.Range(0f, 1f) >= LevelSettings.Instance.ItemSpawnSettings.ChanceOfBadCheese);
        }

        //initialize new item
        newItem.Init(this);
    }

    public void AddItemToBelt(BeltItem newItem) {
        ItemsOnBelt.Add(newItem);
    }
    public void RemoveItemFromBelt(BeltItem oldItem) {
        ItemsOnBelt.Remove(oldItem);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        BeltItem item = col.GetComponent<BeltItem>();
        if (item != null && !ItemsOnBelt.Contains(item))
            AddItemToBelt(item);
    }

    private void OnTriggerExit2D(Collider2D col) {
        BeltItem item = col.GetComponent<BeltItem>();
        if (item != null && ItemsOnBelt.Contains(item))
            RemoveItemFromBelt(item);
    }

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
