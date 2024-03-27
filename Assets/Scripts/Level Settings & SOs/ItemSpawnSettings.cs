using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Spawn Settings", menuName = "ScriptableObjects/Item Spawn Settings")]
public class ItemSpawnSettings : ScriptableObject {
    public float DayLengthInSeconds;
    public float SpawnInterval;
    public float ChanceOfBadCheese;
    public SpawnItem[] ItemsToSpawn;

    private float cumulativeSpawnChance = 0f;
    private float[] spawnChancesPerItem;
    
    [System.Serializable] public struct SpawnItem {
        public string Name;
        public float ChanceOfSpawningModifier;
    }

    public void Awake() {
        spawnChancesPerItem = new float[ItemsToSpawn.Length];
        for (int i = 0; i < ItemsToSpawn.Length; i++) {
            cumulativeSpawnChance += ItemsToSpawn[i].ChanceOfSpawningModifier;
            spawnChancesPerItem[i] = cumulativeSpawnChance;
        }
    }

    public string GetRandomItem() {
        float rand = UnityEngine.Random.Range(0f, cumulativeSpawnChance);
        for (int i = 0; i < spawnChancesPerItem.Length; i++){
            if (rand <= spawnChancesPerItem[i])
                return ItemsToSpawn[i].Name;
        }

        return ItemsToSpawn[ItemsToSpawn.Length-1].Name;
    }
}
