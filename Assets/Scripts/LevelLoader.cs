using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public static LevelLoader Instance { get; private set; }

    [SerializeField] private bool startOnMainMenu;
    [SerializeField] private int defaultDay;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"A later instance of LevelLoader on {Instance.gameObject.name} was replaced by an earlier one on {gameObject.name}.");
            DestroyImmediate(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);

        LoadDay(defaultDay);
    }

    public void LoadMainMenu() {

    }

    public void LoadDay(int day) {
        

        LevelValues levelToLoad = LevelReference.Instance.GetDay(day);
        LevelManager.Instance.LoadNewSettings(levelToLoad.ScoringParameters, levelToLoad.ItemSpawnSettings);
    }

}
