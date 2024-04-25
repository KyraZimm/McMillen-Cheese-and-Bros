using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelValues {
    [HideInInspector][SerializeField] public string name;
    [HideInInspector] public int Day;
    public ScoringParameters ScoringParameters;
    public ItemSpawnSettings ItemSpawnSettings;
    public GameObject BinPrefab;
}

public class LevelReference : MonoBehaviour {
    private static LevelReference instance;
    public static LevelReference Instance {
        get {
            if (instance == null)
                instance = Instantiate(Resources.Load("LevelReference") as GameObject).GetComponent<LevelReference>();
            return instance;
        }
    }

    [SerializeField] private LevelValues[] levels;
    public int NumOfLevels { get { return levels.Length; } }


#if UNITY_EDITOR
    private void OnValidate() {
        for (int i = 0; i < levels.Length; i++) {
            levels[i].name = "Day " + (i + 1).ToString();
            levels[i].Day = i+1;
        }
    }
#endif

    public LevelValues GetDay(int day) {
        if (day-1 < 0 || day > levels.Length + 1) {
            Debug.LogError($"Data for day {day} has been requested, but this is outside the range of available days in LevelReference.");
            return null;
        }

        return levels[day-1];
    }

}
