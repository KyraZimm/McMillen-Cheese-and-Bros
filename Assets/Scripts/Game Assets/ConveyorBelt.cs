using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

    private DistanceComparer distanceSorter;

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

        distanceSorter = new DistanceComparer(endPoint);
    }

    private void FixedUpdate() {
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
        if (newItem != null && !ItemsOnBelt.Contains(newItem)) {
            ItemsOnBelt.Add(newItem); //add to list
            ProjectOntoBelt(newItem.transform); //project position onto belt
            ItemsOnBelt.Sort(distanceSorter); //sort list by distance from endpoint

            FixOverlappingItems();
        }
           
    }
    public void RemoveItemFromBelt(BeltItem oldItem) {
        if (oldItem != null && ItemsOnBelt.Contains(oldItem))
            ItemsOnBelt.Remove(oldItem);
    }

    private void ProjectOntoBelt(Transform objToProject) {
        //belt is always completely horizontal, so we can just snap to the y-coord
        Vector2 proj = objToProject.position;
        proj.y = startPoint.y;
        if (proj.x < startPoint.x)
            proj.x = startPoint.x;
        else if (proj.x > endPoint.x)
            proj.x = endPoint.x;

        objToProject.position = proj;
    }


    private void FixOverlappingItems() {
        //items should always be in order of closest to end -> furthest from end, so we can just shuffle each item back if it's too close to another one
        for (int i = 0; i < ItemsOnBelt.Count-1; i++) {
            float minDistAway = (ItemsOnBelt[i].Col.bounds.size.x/2) + (ItemsOnBelt[i+1].Col.bounds.size.x/2); //min allowed dist from one item's centerpoint to the next
            float currDistAway = Vector3.Distance(ItemsOnBelt[i].transform.position, ItemsOnBelt[i + 1].transform.position);
            if (currDistAway < minDistAway)
                ItemsOnBelt[i + 1].transform.position -= new Vector3(minDistAway-currDistAway, 0, 0);
        }
    }

    public class DistanceComparer : IComparer<BeltItem> {
        private Vector2 target;
        public DistanceComparer(Vector2 distanceToTarget) { target = distanceToTarget; }
        public int Compare(BeltItem a, BeltItem b) { return Vector2.Distance(a.transform.position, target).CompareTo(Vector2.Distance(b.transform.position, target)); }
    }


}
