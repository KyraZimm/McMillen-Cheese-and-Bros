using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableConveyorBelt : ConveyorBelt {
    private float timeLastItemSpawned = 0;
    private DistanceComparer distanceSorter;

    protected override void Awake() {
        base.Awake();
        distanceSorter = new DistanceComparer(endPoint);
    }
    protected override void FixedUpdate() {
        base.FixedUpdate();

        //if ready, spawn new cheese
        if (Time.time - timeLastItemSpawned >= LevelManager.Instance.ItemSpawnSettings.SpawnInterval) {
            SpawnItem();
            timeLastItemSpawned = Time.time;
        }
    }

    private void SpawnItem() {
        //get random item to spawn
        string newItemToSpawn = LevelManager.Instance.ItemSpawnSettings.GetRandomItem();
        BeltItem newItem = Instantiate(ItemReference.Instance.ItemPrefabs[newItemToSpawn], startPoint, Quaternion.identity).GetComponent<BeltItem>();

        //if item is cheese, determine quality
        if (newItem is Cheese) {
            Cheese newCheese = newItem as Cheese;
            newCheese.SetQuality(Random.Range(0f, 1f) >= LevelManager.Instance.ItemSpawnSettings.ChanceOfBadCheese);
        }

        //initialize new item, put on belt
        newItem.Init(this);
        AddItemToBelt(newItem);
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

    public override void AddItemToBelt(BeltItem newItem) {
        if (newItem != null && !ItemsOnBelt.Contains(newItem)) {
            ItemsOnBelt.Add(newItem); //add to list
            ProjectOntoBelt(newItem.transform); //project position onto belt
            FixOverlappingItems(); //space items out so they don't overlap
        }
    }

    public override void RemoveItemFromBelt(BeltItem oldItem) {
        if (oldItem != null && ItemsOnBelt.Contains(oldItem))
            ItemsOnBelt.Remove(oldItem);
    }

    private void FixOverlappingItems() {
        ItemsOnBelt.Sort(distanceSorter); //sort list by distance from endpoint

        for (int i = 0; i < ItemsOnBelt.Count - 1; i++) {
            float minDistAway = (ItemsOnBelt[i].Col.bounds.size.x / 2) + (ItemsOnBelt[i + 1].Col.bounds.size.x / 2); //min allowed dist from one item's centerpoint to the next
            float currDistAway = Vector3.Distance(ItemsOnBelt[i].transform.position, ItemsOnBelt[i + 1].transform.position);
            if (currDistAway < minDistAway)
                ItemsOnBelt[i + 1].transform.position -= new Vector3(minDistAway - currDistAway, 0, 0);
        }
    }

    public class DistanceComparer : IComparer<BeltItem> {
        private Vector2 target;
        public DistanceComparer(Vector2 distanceToTarget) { target = distanceToTarget; }
        public int Compare(BeltItem a, BeltItem b) { return Vector2.Distance(a.transform.position, target).CompareTo(Vector2.Distance(b.transform.position, target)); }
    }
}
