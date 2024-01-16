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
        if (Input.GetMouseButtonDown(0)) {
            HeldItem = ItemUnderMouse();
            if (HeldItem != null)
                HeldItem.OnMouseSelect();
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (HeldItem != null)
                HeldItem.OnMouseDeselect();
            HeldItem = null;
        }
        

        if (HeldItem != null) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HeldItem.MoveToPos(pos);
        }
    }

    

    private BeltItem ItemUnderMouse() {
        Vector3 mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseOrigin, Vector3.forward, 11f);

        if (hit.collider != null) {
            BeltItem itemUnderMouse = hit.collider.GetComponent<BeltItem>();
            return itemUnderMouse;                
        }

        return null;
    }

}
