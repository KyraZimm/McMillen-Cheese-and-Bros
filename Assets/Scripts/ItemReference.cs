using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPrefab {
    public ItemTag name;
    public GameObject prefab;
}

public class ItemReference : MonoBehaviour {
    private static ItemReference instance;
    public static ItemReference Instance {
        get {
            if (instance == null)
                instance = Instantiate(Resources.Load("ItemReference") as GameObject).GetComponent<ItemReference>();
            return instance;
        }
    }

    [SerializeField] private List<ItemPrefab> itemsToLoad;
    public Dictionary<ItemTag, GameObject> ItemPrefabs;

    private void Awake() {
        ItemPrefabs = new Dictionary<ItemTag, GameObject>();
        foreach (ItemPrefab item in itemsToLoad)
            ItemPrefabs.Add(item.name, item.prefab);
        
        //NOTE: add check to prevent items from being double-counted
    }
}
