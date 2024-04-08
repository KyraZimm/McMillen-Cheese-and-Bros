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
    }

    private void Start() {
        if (startOnMainMenu)
            LoadMainMenu();
        else
            LoadDay(defaultDay);
    }

    public void LoadMainMenu() { SceneManager.LoadScene(0); }

    public void LoadDay(int day) {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            SceneManager.LoadScene(1);

        LevelValues levelToLoad = LevelReference.Instance.GetDay(day);
        LevelManager.Instance.LoadNewSettings(levelToLoad.ScoringParameters, levelToLoad.ItemSpawnSettings);

        BinSpawnPoint.Instance.LoadBinLayout(levelToLoad.BinPrefab);
    }

}
