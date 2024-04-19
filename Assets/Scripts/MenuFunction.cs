using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class MenuFunction : MonoBehaviour
{
    public static MenuFunction Instance { get; private set; } //(still not sure what this does tbh)
    [SerializeField] TMP_Text playerInputField;
    public GameObject qualityCheck;
    public GameObject levelStart;
    string playerInput;


        // Start is called before the first frame update
        void Start()
    {
        playerInput = playerInputField.text;
    }

    public void StartGame()
    {
        LevelLoader.Instance.LoadDay(1);
    }

    public void StartLevel()
    {
        //LevelLoader.Instance.LoadDay(string playerInput);
    }
    
    // Update is called once per frame
    public void CheckCode()
    {
        playerInput = playerInputField.text;

        Debug.Log(playerInput); //This is not returning the text I put into the game. I assume it's because I'm not pulling it properly

        LevelValues level = LevelReference.Instance.GetDay(playerInput); //this basically does what your code below was doing. If a level exists, it'll give you the LevelValues that match the word of the day. If not, it'll be null (empty)
        if (level != null) { //if level == null, no level matched the word of the day. if level != null, it means a match was found
            levelStart.SetActive(true);
        }
        else {
            qualityCheck.SetActive(true);
        }

        //Lydia's code:

        /*bool matchExists = false;
        for (int i = 0; i < LevelReference.Instance.Levels.Length; i++)
        {
           if (LevelReference.Instance.Levels[i].WordOfTheDay == playerInput)
           {
                matchExists = true;
                levelStart.SetActive(true);
           }
        }

        if (!matchExists)
        {
            qualityCheck.SetActive(true);
        }*/
    }
}
