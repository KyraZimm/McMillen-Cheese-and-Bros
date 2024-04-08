using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSpawnPoint : MonoBehaviour {
    public static BinSpawnPoint Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of BinSpawnPoint on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public void LoadBinLayout(GameObject binPrefab) {
        int currBins = transform.childCount;
        if (currBins > 0) {
            for (int i = currBins; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }

        Transform newBinLayout = Instantiate(binPrefab, transform).transform;
        newBinLayout.localPosition = Vector3.zero;
    }
}
