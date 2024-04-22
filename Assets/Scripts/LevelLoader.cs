using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelLoader : MonoBehaviour {
    public static LevelLoader Instance { get; private set; }

    [SerializeField] private bool startOnMainMenu;
    [SerializeField] private bool overrideSaveData;
    [SerializeField] private int overrideDayToStartOn;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"A later instance of LevelLoader on {Instance.gameObject.name} was replaced by an earlier one on {gameObject.name}.");
            DestroyImmediate(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);

#if !UNITY_EDITOR
        overrideSaveData = false;
        startOnMainMenu = true;
#endif

        if (!PlayerPrefs.HasKey("day")) {
            PlayerPrefs.SetInt("day", 1);
            PlayerPrefs.Save();
        }
    }

    private void Start() {
        if (startOnMainMenu)
            LoadMainMenu();
        else if (overrideSaveData)
            LoadDay(overrideDayToStartOn, false);
        else
            LoadDay(PlayerPrefs.GetInt("day"), false);
    }

    private List<ILevelLoadField> FetchFieldsToLoad() {
        IEnumerable<ILevelLoadField> fields = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ILevelLoadField>();
        return new List<ILevelLoadField>(fields);
    }

    public void LoadMainMenu() { SceneManager.LoadScene(0); }

    public void LoadDay(int day, bool saveGame) {
        if (saveGame) {
            PlayerPrefs.SetInt("day", day);
            PlayerPrefs.Save();
        }

        LevelValues levelToLoad = LevelReference.Instance.GetDay(day);
        LoadDay(levelToLoad);
    }

    private void LoadDay(LevelValues levelToLoad) {

        //if not on gameplay scene, load correct scene
        if (SceneManager.GetActiveScene().buildIndex != 1)
            SceneManager.LoadScene(1);

        //load spawn settings and scoring parameters to the proper location
        LevelManager.Instance.LoadNewSettings(levelToLoad.ScoringParameters, levelToLoad.ItemSpawnSettings);

        //load fields
        List<ILevelLoadField> fieldsToLoad = FetchFieldsToLoad();
        foreach (ILevelLoadField field in fieldsToLoad)
            field.OnLevelLoad(levelToLoad);
    }

}
