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
}
