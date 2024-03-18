using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelValues {
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

    public LevelValues GetDay(int day) {
        if (day-1 < 0 || day > levels.Length + 1) {
            Debug.LogError($"Data for day {day} has been requested, but this is outside the range of available days in LevelReference.");
            return null;
        }

        return levels[day-1];
    }
}
