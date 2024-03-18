using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*[CustomPropertyDrawer(typeof(LevelValuesArray))]
public class LevelReferenceEditor : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

        float height = EditorGUIUtility.singleLineHeight;
        if (property.isExpanded)
            height += EditorGUI.GetPropertyHeight(property);

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

        if (property.isExpanded) {
            EditorGUI.indentLevel++;

            

            EditorGUI.indentLevel--;
        }

        

        EditorGUI.EndProperty();
    }
}*/

[CustomPropertyDrawer(typeof(LevelReference))]
public class LevelReferenceEditor : Editor {

    public override void OnInspectorGUI() {
        SerializedProperty levelArray = serializedObject.FindProperty("levels");

        Debug.Log(levelArray.arraySize);
    }
}
