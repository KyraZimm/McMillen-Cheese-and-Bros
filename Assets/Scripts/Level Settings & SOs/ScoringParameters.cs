using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scoring Parameters", menuName = "ScriptableObjects/Scoring Parameters SO")]
public class ScoringParameters : ScriptableObject {
    [System.Serializable] public class ScoreScenario {
        public ScoreItem inputItem;
        public ScoreItem desiredItem;
        public float score;
    }

    public List<ScoreScenario> scoreScenarios;
}

[System.Serializable] public class ScoreItem {
    //see the ScoredItemsEditor class for inspector-based changes made here

    public ItemTag item;
    [SerializeField] private bool isGoodCheese = false;

    public bool Match(ScoreItem otherItem) {
        if (otherItem.item == ItemTag.Cheddar || otherItem.item == ItemTag.Gruyere)
            return (otherItem.item == this.item && otherItem.isGoodCheese == this.isGoodCheese);
        else
            return otherItem.item == this.item;
    }

    public ScoreItem(ItemTag itemToScore, bool scoreAsGoodCheese) {
        if (itemToScore != ItemTag.Cheddar && itemToScore != ItemTag.Gruyere)
            scoreAsGoodCheese = false;

        item = itemToScore;
        isGoodCheese = scoreAsGoodCheese;
    }

}

