using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReference : MonoBehaviour {

    [SerializeField] SpriteRef[] sprites;
    [System.Serializable] struct SpriteRef {
        public string name;
        public Sprite sprite;
    }

    private Dictionary<string, Sprite> spriteDictionary;

    private static SpriteReference instance;
    public static SpriteReference Instance {
        get {
            if (instance == null)
                instance = Instantiate(Resources.Load("SpriteReference") as GameObject).GetComponent<SpriteReference>();
            return instance;
        }
    }

    private void Awake() {
        spriteDictionary = new Dictionary<string, Sprite>();
        foreach (SpriteRef spriteRef in sprites) { spriteDictionary.Add(spriteRef.name, spriteRef.sprite); }
    }

    public Sprite GetSprite(string name) { return spriteDictionary[name]; }
}
