using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Scoring Parameters", menuName = "ScriptableObjects/Scoring Parameters SO")]
public class ScoringParameters : ScriptableObject {
    [System.Serializable] public struct ScoreScenario {
        public EnumDataExtension<ScoreItem, ItemTag> inputItem;
        public EnumDataExtension<ScoreItem, ItemTag> desiredItem;
        public float score;
    }

    public List<ScoreScenario> scoreScenarios;
}

[System.Serializable] public class ScoreItem {
    public bool isGoodCheese;
}

