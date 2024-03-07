using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public float score;
    [SerializeField] TMP_Text scoreText;
    public static ScoreKeeper Instance { get; private set; }

    void Awake()
    {
        score = 0;
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of ScoreKeeper on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public void ModifyScore(BeltItem item, ScoreItem desiredItem) {
        ScoringParameters.ScoreScenario targetScenario;
        foreach (ScoringParameters.ScoreScenario scenario in LevelSettings.Instance.ScoringParameters.scoreScenarios) {
            if (scenario.inputItem.Match(item.AsScoreItem()) && scenario.desiredItem.Match(desiredItem))
                targetScenario = scenario;
        }


    }

}
