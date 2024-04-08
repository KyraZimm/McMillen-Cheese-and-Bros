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
        LevelValues levelToLoad = LevelReference.Instance.GetDay(day);
        LoadDay(levelToLoad);
    }

    public void LoadDay(string wordOfTheDay) {
        LevelValues levelToLoad = LevelReference.Instance.GetDay(wordOfTheDay);
        LoadDay(levelToLoad);
    }

    private void LoadDay(LevelValues levelToLoad) {

        //if not on gameplay scene, load correct scene
        if (SceneManager.GetActiveScene().buildIndex != 1)
            SceneManager.LoadScene(1);

        //load spawn settings and scoring parameters to the proper location
        LevelManager.Instance.LoadNewSettings(levelToLoad.ScoringParameters, levelToLoad.ItemSpawnSettings);

        //bins should be instantiated in the proper place. If for some reason the spawn point has been removed, spawn bins in scene hierarchy
        if (BinSpawnPoint.Instance != null)
            BinSpawnPoint.Instance.LoadBinLayout(levelToLoad.BinPrefab);
        else
            Instantiate(levelToLoad.BinPrefab);

        //word of the day upload
    }

}
