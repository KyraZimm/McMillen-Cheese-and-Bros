using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomPropertyDrawer(typeof(EnumDataExtension<,>))]
public class EnumDataExtensionDrawer : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        SerializedProperty content = property.FindPropertyRelative("content");
        SerializedProperty enumType = property.FindPropertyRelative("enumType");

        float height = EditorGUIUtility.singleLineHeight;
        if (property.isExpanded) {
            if (content.arraySize != enumType.enumNames.Length)
                content.arraySize = enumType.enumNames.Length;

            for (int i = 0; i < content.arraySize; i++) {
                height += EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i));
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

            SerializedProperty content = property.FindPropertyRelative("content");
            SerializedProperty enumType = property.FindPropertyRelative("enumType");

            float addY = EditorGUIUtility.singleLineHeight;
            for (int i = 0; i < content.arraySize; i++) {
                Rect rect = new Rect(position.x, position.y + addY, position.width, EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i)));
                addY += rect.height;
                EditorGUI.PropertyField(rect, content.GetArrayElementAtIndex(i), new GUIContent(enumType.enumNames[i]), true);

                Debug.Log($"content array size: {content.arraySize}");
            }
        }

        EditorGUI.EndProperty();
    }
}
