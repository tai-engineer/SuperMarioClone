using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MarioInput))]
public class MarioInputEditor : Editor
{
    bool _isPrefab = false;
    bool _isNotInstance = false;

    SerializedProperty _jump;
    SerializedProperty _horizontal;

    GUIContent _jumpContent;
    GUIContent _horizontalContent;
    void OnEnable()
    {
        _isPrefab = AssetDatabase.Contains(target);
        _isNotInstance = PrefabUtility.GetCorrespondingObjectFromSource(target) == null;

        _jump = serializedObject.FindProperty("Jump");
        _horizontal = serializedObject.FindProperty("Horizontal");

        _jumpContent = new GUIContent("Jump");
        _horizontalContent = new GUIContent("Horizontal");
    }

    public override void OnInspectorGUI()
    {
        if (_isPrefab || _isNotInstance)
        {
            DrawDefaultInspector();
        }
        else
        {
            EditorGUILayout.PropertyField(_jump, _jumpContent);
            EditorGUILayout.PropertyField(_horizontal, _horizontalContent);

            EditorGUILayout.HelpBox("Modify the prefab and not this instance", MessageType.Warning);

            if(GUILayout.Button("Select Prefab"))
            {
                Selection.activeObject = PrefabUtility.GetCorrespondingObjectFromSource(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
