using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPrefab {
    public string name;
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
    public Dictionary<string, GameObject> ItemPrefabs;

    private void Awake() {
        ItemPrefabs = new Dictionary<string, GameObject>();
        foreach (ItemPrefab item in itemsToLoad)
            ItemPrefabs.Add(item.name, item.prefab);
    }
}
