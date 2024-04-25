using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    private void Awake() {
        Button button = gameObject.GetComponent<Button>();

        if (button == null) {
            Debug.LogError("There is no Button root on the same GameObject as StartButton. Either move this component or add a UI button.");
            return;
        }

        button.onClick.AddListener(StartGame);
    }

    private void StartGame() {
        LevelLoader.Instance.LoadCurrentDay();
    }
}
