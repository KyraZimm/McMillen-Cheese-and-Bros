using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {
    public static LevelSettings Instance { get; private set; }

    [SerializeField] private ScoringParameters scoringSettings;
    [SerializeField] private ItemSpawnSettings itemSpawnSettings;

    public ScoringParameters ScoringParameters { get { return scoringSettings; } }
    public ItemSpawnSettings ItemSpawnSettings { get { return itemSpawnSettings; } }
}
