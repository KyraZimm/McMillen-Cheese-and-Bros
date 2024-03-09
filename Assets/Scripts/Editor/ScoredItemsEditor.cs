using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomPropertyDrawer(typeof(ScoreItem))]
public class ScoredItemsEditor : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        SerializedProperty item = property.FindPropertyRelative("item");

        float height = EditorGUIUtility.singleLineHeight;
        if (property.isExpanded) {
            height += EditorGUI.GetPropertyHeight(item);
            ItemTag tagValue = (ItemTag)item.intValue;
            if (tagValue == ItemTag.Cheddar || tagValue == ItemTag.Gruyere) {
                SerializedProperty isGoodCheese = property.FindPropertyRelative("isGoodCheese");
                height += EditorGUI.GetPropertyHeight(isGoodCheese);
            }
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

        if (property.isExpanded) {
            EditorGUI.indentLevel++;

            SerializedProperty item = property.FindPropertyRelative("item");

            Rect rect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUI.GetPropertyHeight(item));
            EditorGUI.PropertyField(rect, item);

            ItemTag tagValue = (ItemTag)item.intValue;
            if (tagValue == ItemTag.Cheddar || tagValue == ItemTag.Gruyere) {
                SerializedProperty isGoodCheese = property.FindPropertyRelative("isGoodCheese");
                Rect cheeseRect = new Rect(position.x, position.y + (2 * EditorGUIUtility.singleLineHeight), position.width, EditorGUI.GetPropertyHeight(isGoodCheese));
                EditorGUI.PropertyField(cheeseRect, isGoodCheese);
            }

            EditorGUI.indentLevel--;

        }

        EditorGUI.EndProperty();
    }
}
