using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//IMPORTANT: THIS WAS AN OLD DRAFT, DO NOT USE. KEEPING FOR REFERENCE UNTIL I CAN DELETE

/*[CustomEditor(typeof(ScoreItem))]
public class ScoredItemsEditor : Editor {
    public ItemTag itemType;
    public override void OnInspectorGUI() {
        serializedObject.Update();

        //ScoreItem item = (ScoreItem)target;

        GUILayout.Label("Test");

        *//*itemType = (ItemTag)EditorGUILayout.EnumPopup("Item", itemType);
        if (itemType == ItemTag.Cheddar || itemType == ItemTag.Gruyere)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isGoodCheese"));
*//*

        serializedObject.ApplyModifiedProperties();
    }
}*/

/*[CustomPropertyDrawer(typeof(ScoreItem))]
public class ScoredItemsEditor : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        ScoreItem itemToScore = (ScoreItem)attribute;

        var enumRect = new Rect(position.x + 20, position.y, position.width - 90, position.height);
        var boolRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
        EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("item"), GUIContent.none);

        if (itemToScore != null && (itemToScore.item == ItemTag.Cheddar || itemToScore.item == ItemTag.Gruyere))
            EditorGUI.PropertyField(boolRect, property.FindPropertyRelative("isCheeseGood"), GUIContent.none);
        else
            itemToScore.isGoodCheese = false;

        EditorGUI.EndProperty();
    }
}*/
