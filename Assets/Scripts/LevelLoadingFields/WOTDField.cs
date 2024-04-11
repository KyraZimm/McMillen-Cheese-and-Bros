using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WOTDField : MonoBehaviour {
    public static WOTDField Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of WOTDField on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public void UpdateWOTD() {

    }
}
