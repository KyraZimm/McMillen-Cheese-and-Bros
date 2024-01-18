using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scoring Parameters", menuName = "Scoring Parameters SO")]
public class ScoringParameters : ScriptableObject
{
//QUESTION: Do we want to score all items that stay on the belt and bin items that go into their appropriate final destinations (meaning item type = desireditem per location)?
//OR: Are all possible combinations and destinations scored in some way?
//Example of the former: throwing out something recyclible would be 0 points. recycling a recyclable would be +2 points.
//Example of latter: Putting a recyclable in the trash is 0 points, recycling it is +2 points, composting it is -1 points.


// Good cheese
    public float goodCheeseCorrectBelt;
    public float goodCheeseWrongBelt;
    public float goodCheeseAnyBin;

// Bad cheese
    public float badCheeseEitherBelt;
    public float badCheeseInTrash;
    public float badCheeseInCompost;
    public float badCheeseInRecycling;

// Not cheese
    public float notCheeseOnBelt;
    public float notCheeseInTrash;
    public float recyclingInRecycling;
    public float recyclingInCompost;
    public float compostInRecycling;
    public float compostInCompost;


    ///SUGGESTIONS FROM KYRA
    //instead of making a separate float for each possible situation we could have for scoring, we can create a searchable dictionary or list for our scoring paramters.
    //The goal is to make a function where we just have to plug in the item type we have and the item type we need, and get a score for that item as a result, instead of doing a bunch of manual work to compare items.
    //Otherwise, if we have a float for each possible scenario, and each float has a different name, it gets really hard to scale and reconfigure things.

    //So for example, I'll make a new scoring struct:
    [System.Serializable] public struct ScoreScenario {
        public ItemTag itemInput;
        public bool inputIsGood; //for if you're checking a good/bad cheese
        public ItemTag desiredItem;
        public bool desiredInputIsGood; //for if the bin specifically needs good/bad cheese
        public float score;
    }

    //and then add a section to the inspector where the designer can plug in score values:
    public List<ScoreScenario> scoreScenarios;
    //this means you can put a bunch of stuff in the inspector that says "If the player gives you a cheddar, but you wanted a recycling item, you give them -1 points."
    //"If the player gives you a bad cheddar, but you needed a good cheddar, give them -2 points."
    //etc

    //then, in ScoreKeeper, we can write a function with some parameters:
    private void ModifyScore(BeltItem item, ItemTag desiredItem) {
        //and then inside this function, we can search our ScoreScenario inputs to find the condition that matches our input item and desired item, and return the score value we want.
    }
}
