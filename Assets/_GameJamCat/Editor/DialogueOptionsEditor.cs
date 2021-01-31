using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameJamCat
{
    [CustomEditor(typeof(DialogueOptions))]
    public class DialogueOptionsEditor : Editor
    {
        SerializedProperty _path;

        public void OnEnable()
        {
            _path = serializedObject.FindProperty("_pathToCSV");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();
            var dialogueOptions = target as DialogueOptions;

            if(GUILayout.Button("Select CSV"))
            {
                SelectCSV();
            }

            if(GUILayout.Button("Load CSV"))
            {
                LoadCSV(dialogueOptions);
            }

            serializedObject.ApplyModifiedProperties();

        }

        private void SelectCSV()
        {
            _path.stringValue = EditorUtility.OpenFilePanel("Select CSV", Application.dataPath, "csv");
        }

        private void LoadCSV(DialogueOptions options)
        {
            if (string.IsNullOrEmpty(_path.stringValue)) return;
            var list = Sinbad.CsvUtil.LoadObjects<CatCustomisation>(_path.stringValue);
            Undo.RecordObject(options, "Load options");
            options._catCustomizationOptions = list;
        }
    }
}
