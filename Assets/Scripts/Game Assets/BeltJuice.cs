using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltJuice : MonoBehaviour
{
    // Lydia wrote this after Kyra explained for loops
    public SpriteRenderer[] wheelSprites;
    private bool flipX;

    void Update()
    {
        
        if (LevelManager.Instance.CurrState != LevelManager.LevelState.Playing)
            return;

        //turn the wheels
        for (int i = 0; i < wheelSprites.Length; i++)
        {
            //flip x
            if (wheelSprites[i].flipX == false)
            {
                wheelSprites[i].flipX = true;
            }
            else
            {
                wheelSprites[i].flipX = false;
            }
        }
        
        // Play belt sounds 

    }



}
