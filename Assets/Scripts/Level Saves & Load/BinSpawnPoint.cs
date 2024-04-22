using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSpawnPoint : MonoBehaviour, ILevelLoadField {
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

    void ILevelLoadField.OnLevelLoad(LevelValues levelToLoad) {
        int currBins = transform.childCount;
        if (currBins > 0) {
            for (int i = currBins - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }

        Transform newBinLayout = Instantiate(levelToLoad.BinPrefab, transform).transform;
        newBinLayout.localPosition = Vector3.zero;
    }
}
