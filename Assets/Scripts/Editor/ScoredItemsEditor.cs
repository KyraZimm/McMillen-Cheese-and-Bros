using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(ScoreItem))]
public class ScoredItemsEditor : Editor {
    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("item"));
        //ScoreItem item = (ScoreItem)target;


        serializedObject.ApplyModifiedProperties();
    }
}
