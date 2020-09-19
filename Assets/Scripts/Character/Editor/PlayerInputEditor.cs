using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerInput))]
public class PlayerInputEditor : Editor
{
    bool _isPrefab = false;
    bool _isNotInstance = false;

    SerializedProperty _jump;
    SerializedProperty _horizontal;
    SerializedProperty _dash;
    SerializedProperty _meleeAttack;
    SerializedProperty _shooting;

    GUIContent _jumpContent;
    GUIContent _horizontalContent;
    GUIContent _dashContent;
    GUIContent _meleeAttackContent;
    GUIContent _shootingContent;
    void OnEnable()
    {
        _isPrefab = AssetDatabase.Contains(target);
        _isNotInstance = PrefabUtility.GetCorrespondingObjectFromSource(target) == null;

        _jump = serializedObject.FindProperty("Jump");
        _horizontal = serializedObject.FindProperty("Horizontal");
        _dash = serializedObject.FindProperty("Dash");
        _meleeAttack = serializedObject.FindProperty("MeleeAttack");
        _shooting = serializedObject.FindProperty("Shooting");

        _jumpContent = new GUIContent("Jump");
        _horizontalContent = new GUIContent("Horizontal");
        _dashContent = new GUIContent("Dash");
        _meleeAttackContent = new GUIContent("MeleeAttack");
        _shootingContent = new GUIContent("Shooting");
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
            EditorGUILayout.PropertyField(_dash, _dashContent);
            EditorGUILayout.PropertyField(_meleeAttack, _meleeAttackContent);
            EditorGUILayout.PropertyField(_shooting, _shootingContent);

            EditorGUILayout.HelpBox("Modify the prefab and not this instance", MessageType.Warning);

            if(GUILayout.Button("Select Prefab"))
            {
                Selection.activeObject = PrefabUtility.GetCorrespondingObjectFromSource(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
