using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelValues {
    public ScoringParameters ScoringParameters;
    public ItemSpawnSettings ItemSpawnSettings;
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
}
