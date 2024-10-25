using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PressurePlateScript))]
public class Editor_PressurePlateA : Editor
{
    PressurePlateScript editorTarget;

    SerializedProperty index;
    SerializedProperty isInteractive;
    SerializedProperty TagCollisionEnter;
    SerializedProperty TagCollisionExit;
    SerializedProperty TagCollisionStay;
    SerializedProperty ShowCollisionEnterEvent;
    SerializedProperty ShowCollisionStayEvent;
    SerializedProperty ShowCollisionExitEvent;
    SerializedProperty DisableAudio;
    SerializedProperty DisableAnimations;

    void OnEnable()
    {
        editorTarget = target as PressurePlateScript;

        index = serializedObject.FindProperty("index");
        isInteractive = serializedObject.FindProperty("isInteractive");
        TagCollisionEnter = serializedObject.FindProperty("TagCollisionEnter");
        TagCollisionExit = serializedObject.FindProperty("TagCollisionExit");
        TagCollisionStay = serializedObject.FindProperty("TagCollisionStay");
        ShowCollisionEnterEvent = serializedObject.FindProperty("ShowCollisionEnterEvent");
        ShowCollisionStayEvent = serializedObject.FindProperty("ShowCollisionStayEvent");
        ShowCollisionExitEvent = serializedObject.FindProperty("ShowCollisionExitEvent");
        DisableAudio = serializedObject.FindProperty("DisableAudio");
        DisableAnimations = serializedObject.FindProperty("DisableAnimations");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        isInteractive.boolValue = EditorGUILayout.Toggle("Is Interactive: ", isInteractive.boolValue);

        if (isInteractive.boolValue)
        {
            EditorGUILayout.LabelField("Responsivity", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Interact with tag: ", GUILayout.MaxWidth(120));

            if (editorTarget.options != null && editorTarget.options.Length > 0)
            {
                index.intValue = EditorGUILayout.Popup(index.intValue, editorTarget.options);
                editorTarget.index = index.intValue;
            }
            else
            {
                EditorGUILayout.LabelField("No Tags Available");
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("On Tag Collision Enter:", GUILayout.MaxWidth(167));
            ShowCollisionEnterEvent.boolValue = EditorGUILayout.Toggle(ShowCollisionEnterEvent.boolValue);
            EditorGUILayout.EndHorizontal();
            if (ShowCollisionEnterEvent.boolValue)
            {
                EditorGUILayout.PropertyField(TagCollisionEnter);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("On Tag Collision Stay:", GUILayout.MaxWidth(167));
            ShowCollisionStayEvent.boolValue = EditorGUILayout.Toggle(ShowCollisionStayEvent.boolValue);
            EditorGUILayout.EndHorizontal();
            if (ShowCollisionStayEvent.boolValue)
            {
                EditorGUILayout.PropertyField(TagCollisionStay);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("On Tag Collision Exit:", GUILayout.MaxWidth(167));
            ShowCollisionExitEvent.boolValue = EditorGUILayout.Toggle(ShowCollisionExitEvent.boolValue);
            EditorGUILayout.EndHorizontal();
            if (ShowCollisionExitEvent.boolValue)
            {
                EditorGUILayout.PropertyField(TagCollisionExit);
            }

            EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(DisableAudio);

            EditorGUILayout.LabelField("Animations", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(DisableAnimations);
        }

        serializedObject.ApplyModifiedProperties();
    }
}








