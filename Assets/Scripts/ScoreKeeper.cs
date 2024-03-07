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
        ScoringParameters.ScoreScenario targetScenario = null;
        foreach (ScoringParameters.ScoreScenario scenario in LevelSettings.Instance.ScoringParameters.scoreScenarios) {
            if (scenario.inputItem.Match(item.AsScoreItem()) && scenario.desiredItem.Match(desiredItem))
                targetScenario = scenario;
        }

        if (targetScenario == null) {
            Debug.LogError($"There is no score scenario to compare the input {item.Type} to  the desired {desiredItem.item}. Please check the Settings GameObject and make sure all possible score scenarios are present.");
            return;
        }

        score += targetScenario.score;
        scoreText.text = "Score: " + score;

    }

}
