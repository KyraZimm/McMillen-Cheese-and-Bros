using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public int Day;
}
public class SaveDataManager : MonoBehaviour {
    private static SaveDataManager instance;
    public static SaveDataManager Instance {
        get {
            if (instance == null)
                instance = Instantiate(Resources.Load("SaveDataManager") as GameObject).GetComponent<SaveDataManager>();
            return instance;
        }
    }

    private const string SAVE_DATA_PATH = "";


}
