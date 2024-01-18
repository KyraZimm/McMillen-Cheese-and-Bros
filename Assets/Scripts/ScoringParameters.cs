using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scoring Parameters", menuName = "Scoring Parameters SO")]
public class ScoringParameters : ScriptableObject
{
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
