using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseTracker : MonoBehaviour {
    public static MouseTracker Instance { get; private set; }
    public BeltItem HeldItem { get; private set; }

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of MouseTracker was destroyed on {Instance.gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    private void Update() {
        
    }

    private void CheckForItem() {
        Vector3 mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseOrigin.z -= 10;

        RaycastHit hit;
        Physics.Raycast(mouseOrigin, Vector3.forward, out hit);

        if (hit.collider != null) {
            BeltItem itemUnderMouse = hit.collider.GetComponent<BeltItem>();
            if (itemUnderMouse != null)
                itemUnderMouse.OnMouseSelect();
        }
    }
}
