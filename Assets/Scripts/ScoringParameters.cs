using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scoring Parameters", menuName = "ScriptableObjects/Scoring Parameters SO")]
public class ScoringParameters : ScriptableObject {
    [System.Serializable] public struct ScoreScenario {
        public ScoreItem inputItem;
        public ScoreItem desiredItem;
        public float score;
    }

    public List<ScoreScenario> scoreScenarios;
}

[System.Serializable] public class ScoreItem {
    public ItemTag item;
    public bool isGoodCheese;
}
