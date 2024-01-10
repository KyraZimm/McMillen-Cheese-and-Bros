using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    private void Start() {
        Test();
    }
    private void Test() {
        Instantiate(ItemReference.Instance.ItemPrefabs["Cheddar"]);
    }
}
