using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public ScoringParameters scoring;
    public float score;
    [SerializeField] TMP_Text scoreText;
    public static ScoreKeeper Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //check for messages from other scripts (BeltJunction, TrashBin, RecyclingBin, CompostBin)
        //reference the scriptable object for how much the score changes because of that message
        //change the score by that amount


    }

    /*public void ModifyScore(BeltItem itemToScore, ItemTag desiredItemType) {
        
        // ERROR, HOW DO ESTABLISH WHAT DESIREDITEMTYPE IS? desiredItemType = BeltJunction.beltSetting;
        // At the moment, this only accounts for the belt junction as a recieving destination that has an ideal target.
        // This does not account for any of the bins, be it trash, recycling, or compost.

        Cheese cheese = itemToScore as Cheese;
        if (itemToScore is Cheese) {
            if (!cheese.IsGood) {
                //a bad cheese was sent to the belts
                score = score + scoring.badCheeseEitherBelt;
            }
            if (cheese.IsGood && itemToScore.Type == desiredItemType) {
                //a good cheese was sent to the correct belt
                score = score + scoring.goodCheeseCorrectBelt;
            }
            if (cheese.IsGood && itemToScore.Type != desiredItemType) {
                //a good cheese was sent to the incorrect belt
                score = score + scoring.goodCheeseWrongBelt;
            }
        }
        else {
            // a non-cheese item got sent to the belts
            score = score + scoring.notCheeseOnBelt;
        }

        scoreText.text = "Score: " + score;
    }*/

    public void ModifyScore(BeltItem item, ItemTag desiredItem, bool desiresGoodCheese) {
        ScoringParameters.ScoreScenario targetScenario;
        /*foreach (ScoringParameters.ScoreScenario scenario in scoring.scoreScenarios) {
            if (item.Match(scenario.itemInput, scenario.inputIsGood) && item.Match(scenario.itemInput, scenario.inputIsGood))
                targetScenario = scenario;
        }*/

        /*if (targetScenario == null) {
            Debug.LogError($"ERROR: there is no scoring precedent for comparing an input of {item.Type} against {desiredItem}. Please add these values to the ScoringParameters used.");
            return;
        }*/


    }

}
