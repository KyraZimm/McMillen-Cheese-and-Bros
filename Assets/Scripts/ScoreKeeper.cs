using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour, ILevelLoadField {
    public static ScoreKeeper Instance { get; private set; }

    [SerializeField] TMP_Text winsText;
    [SerializeField] TMP_Text learningExpsText;

    //public float Score { get; private set; }
    public float Wins { get; private set; }
    public float LearningExps { get; private set; }

    void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of ScoreKeeper on {Instance.gameObject.name} was replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;

       /* Score = 0;
        score.text = "Score: " + Score.ToString();*/

        Wins = 0;
        LearningExps = 0;

        winsText.text = "Wins: " + Wins.ToString();
        learningExpsText.text = "Learning Experiences: " + LearningExps.ToString();
    }

    void ILevelLoadField.OnLevelLoad(LevelValues levelToLoad) {
        //reset values
        Wins = 0;
        LearningExps = 0;

        //update board
        winsText.text = "Wins: " + Wins.ToString();
        learningExpsText.text = "Learning Experiences: " + LearningExps.ToString();
    }

    public void ModifyScore(ScoreItem item, ScoreItem desiredItem) {

        //NOTE: this should undergo major refactors later, if it's not reverted at some point

        /*ScoringParameters.ScoreScenario targetScenario = null;
        foreach (ScoringParameters.ScoreScenario scenario in LevelManager.Instance.ScoringParameters.scoreScenarios) {
            if (scenario.inputItem.Match(item) && scenario.desiredItem.Match(desiredItem))
                targetScenario = scenario;
        }

        //if there is no target scenario, assume the score is unchanged
        if (targetScenario == null) {
            Debug.LogWarning($"There is no score scenario to compare the input {item.item} to the desired {desiredItem.item}. If this is incorrect, please check your Scoring Parameters.");
            return;
        }

        Score += targetScenario.score;
        scoreText.text = "Score: " + Score;*/

        if (desiredItem.Match(item)) {
            Wins++;
            winsText.text = "Wins: " + Wins.ToString();
        }
        else {
            LearningExps++;
            learningExpsText.text = "Learning Experiences: " + LearningExps.ToString();
        }

    }

}
