using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    public ScoringParameters ScoringParameters { get; private set; }
    public ItemSpawnSettings ItemSpawnSettings { get; private set; }

    public enum LevelState { Start, Playing, End }
    public LevelState CurrState { get; private set; }
    public float TimeAtLastStateChange { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of LevelManager on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;

        ChangeLevelState(LevelState.Start);
    }

    private void Update() {
        if (CurrState == LevelState.Playing) {
            if (Time.time - TimeAtLastStateChange >= ItemSpawnSettings.DayLengthInSeconds)
                ChangeLevelState(LevelState.End);
        }
    }

    public void ChangeLevelState(LevelState newState) {
        CurrState = newState;
        TimeAtLastStateChange = Time.time;
    }

    public void LoadNewSettings(ScoringParameters scoringParameters, ItemSpawnSettings itemSpawnSettings) {
        ScoringParameters = scoringParameters;
        ItemSpawnSettings = itemSpawnSettings;

        ChangeLevelState(LevelState.Start);
    }


    //for ease of use with UnityEvent API:
    public void PlayLevel() { ChangeLevelState(LevelState.Playing); }
    public void EndLevel() { ChangeLevelState(LevelState.End); }
    public void SetupLevel() { ChangeLevelState(LevelState.Start); }
}
