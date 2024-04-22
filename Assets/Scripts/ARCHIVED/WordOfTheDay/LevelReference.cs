/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelValues {
    public string WordOfTheDay;
}
public class LevelReference : MonoBehaviour {
    public LevelValues GetDay(string wordOfTheDay) {
        //HACK: Unity bug fix. Forum post for ref: https://forum.unity.com/threads/textmesh-pro-ugui-hidden-characters.505493/
        string wotd = wordOfTheDay.Trim((char)8203);

        foreach (LevelValues level in levels) {
            if (string.Compare(wotd, level.WordOfTheDay, StringComparison.OrdinalIgnoreCase) == 0)
                return level;
        }

        Debug.LogError($"There is no level with the word of the day: {wotd} in Resources > LevelReference. Is this entry spelled correctly?");
        return null;
    }
}*/
