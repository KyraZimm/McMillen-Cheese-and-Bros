using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField] private float speed;
    [Header("TEMP: For Testing Only")]
    [SerializeField] string[] itemsToSpawn;
    [SerializeField] float spawnInterval;
    [SerializeField] Transform spawnPoint;

    public Vector2 BeltVector { get; private set; }
    public Vector2 BeltVectorNormalized { get; private set; }
    public List<BeltItem> ItemsOnBelt { get; private set; }

    private float timeLastItemSpawned = 0;
    private Vector2 startPoint;
    private Vector2 endPoint;

    private void Awake() {
        ItemsOnBelt = new List<BeltItem>();

        //calculate start and end points of belt
        float widthFromCenter = transform.localScale.x / 2;
        startPoint = transform.position + (-transform.right * widthFromCenter);
        endPoint = transform.position + (transform.right * widthFromCenter);

        //set belt vector for future caluclations
        BeltVector = endPoint - startPoint;
        BeltVectorNormalized = BeltVector.normalized;
    }

    private void FixedUpdate() {
        foreach (BeltItem item in ItemsOnBelt) {
            Vector2 newTargetPos = (Vector2)item.transform.position + (BeltVectorNormalized * speed * Time.fixedDeltaTime);
            item.MoveToPos(newTargetPos);
        }

        if (Time.time - timeLastItemSpawned >= spawnInterval) {
            SpawnItem();
            timeLastItemSpawned = Time.time;
        }
    }

    private void SpawnItem() {
        string newItemToSpawn = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];
        BeltItem newItem = Instantiate(ItemReference.Instance.ItemPrefabs[newItemToSpawn], spawnPoint).GetComponent<BeltItem>();
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
