using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public float Score { get; private set; }
    [SerializeField] TMP_Text scoreText;
    public static ScoreKeeper Instance { get; private set; }

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of ScoreKeeper on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;

        Score = 0;
        scoreText.text = "Score: " + Score;
    }

    public void ModifyScore(ScoreItem item, ScoreItem desiredItem) {
        ScoringParameters.ScoreScenario targetScenario = null;
        foreach (ScoringParameters.ScoreScenario scenario in LevelSettings.Instance.ScoringParameters.scoreScenarios) {
            if (scenario.inputItem.Match(item) && scenario.desiredItem.Match(desiredItem))
                targetScenario = scenario;
        }

        //if there is no target scenario, assume the score is unchanged
        if (targetScenario == null) {
            Debug.LogWarning($"There is no score scenario to compare the input {item.item} to  the desired {desiredItem.item}. If this is incorrect, please check your Scoring Parameters.");
            return;
        }

        Score += targetScenario.score;
        scoreText.text = "Score: " + Score;

    }

}
