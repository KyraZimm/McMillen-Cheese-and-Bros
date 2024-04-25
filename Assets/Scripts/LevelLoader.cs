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

    private static bool LEVEL_IS_LOADING = false;
    private LevelValues lastLoadedValues;


    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"A later instance of LevelLoader on {Instance.gameObject.name} was replaced by an earlier one on {gameObject.name}.");
            DestroyImmediate(this);
            return;
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
        else {
            int currDay = PlayerPrefs.GetInt("day", 1);
            LoadDay(currDay, true);
        }
    }

    public void LoadMainMenu() { SceneManager.LoadScene(0); }

    public void LoadDay(int day, bool saveGame) {
        day = Mathf.Clamp(day, 1, LevelReference.Instance.NumOfLevels);

        if (saveGame) {
            PlayerPrefs.SetInt("day", day);
            PlayerPrefs.Save();
        }

        LevelValues levelToLoad = LevelReference.Instance.GetDay(day);
        if (LEVEL_IS_LOADING)
            StopCoroutine(LoadDayCoroutine(lastLoadedValues));
        StartCoroutine(LoadDayCoroutine(levelToLoad));
    }

    private IEnumerator LoadDayCoroutine(LevelValues levelToLoad) {
        LEVEL_IS_LOADING = true;
        lastLoadedValues = levelToLoad;

        //if not on gameplay scene, load correct scene
        if (SceneManager.GetActiveScene().buildIndex != 1)
            SceneManager.LoadScene(1);

        //wait until scene is done loading
        while (SceneManager.GetActiveScene().buildIndex != 1) {
            Debug.Log("build index: " + SceneManager.GetActiveScene().buildIndex);
            yield return new WaitForEndOfFrame();
        }
            

        //load spawn settings and scoring parameters to the proper location
        LevelManager.Instance.LoadNewSettings(levelToLoad.ScoringParameters, levelToLoad.ItemSpawnSettings);

        //load fields
        List<ILevelLoadField> fieldsToLoad = FetchFieldsToLoad();
        foreach (ILevelLoadField field in fieldsToLoad)
            field.OnLevelLoad(levelToLoad);

        LEVEL_IS_LOADING = false;
    }

    private List<ILevelLoadField> FetchFieldsToLoad() {
        IEnumerable<ILevelLoadField> fields = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ILevelLoadField>();
        return new List<ILevelLoadField>(fields);
    }

    public void LoadNextDay() {
        int nextDay = PlayerPrefs.GetInt("day", 1) + 1;
        LoadDay(nextDay, true);
    }

    public void LoadCurrentDay() {
        int currDay = PlayerPrefs.GetInt("day", 1);
        LoadDay(currDay, true);
    }

}
