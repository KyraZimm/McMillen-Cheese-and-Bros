using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {
    public static LevelSettings Instance { get; private set; }

    //[SerializeField] private ScoringParameters scoringSettings;
    //[SerializeField] private ItemSpawnSettings itemSpawnSettings;

    public ScoringParameters ScoringParameters { get; private set; }
    public ItemSpawnSettings ItemSpawnSettings { get; private set; }

    public enum LevelState { Start, Playing, End }
    public LevelState CurrState { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of LevelSettings on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;

        CurrState = LevelState.Start;
    }

    public void PlayLevel() { CurrState = LevelState.Playing; }
    public void EndLevel() { CurrState = LevelState.End; }
    public void SetupLevel() { CurrState = LevelState.Start; }

    public void LoadNewSettings(ScoringParameters scoringParameters, ItemSpawnSettings itemSpawnSettings) {
        ScoringParameters = scoringParameters;
        ItemSpawnSettings = itemSpawnSettings;

        CurrState = LevelState.Start;
    }
}
