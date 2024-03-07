using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {
    public static LevelSettings Instance { get; private set; }

    [SerializeField] private ScoringParameters scoringSettings;
    [SerializeField] private ItemSpawnSettings itemSpawnSettings;

    public ScoringParameters ScoringParameters { get { return scoringSettings; } }
    public ItemSpawnSettings ItemSpawnSettings { get { return itemSpawnSettings; } }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of LevelSettings on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }
}
