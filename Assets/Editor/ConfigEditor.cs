using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Configuration))]

public class ConfigEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Configuration config = (Configuration)target;


        EditorGUILayout.LabelField("Position Setting", EditorStyles.boldLabel);
        //  config.ConfigurationName = EditorGUILayout.TextField("ConfigurationName", config.ConfigurationName);
        config.L3Position = EditorGUILayout.Popup("L3Position", config.L3Position, config.positions);
        config.L2Position = EditorGUILayout.Popup("L2Position", config.L2Position, config.positions);
        config.L1Position = EditorGUILayout.Popup("L1Position", config.L1Position, config.positions);


        config.R1Position = EditorGUILayout.Popup("R1Position", config.R1Position, config.positions);
        config.R2Position = EditorGUILayout.Popup("R2Position", config.R2Position, config.positions);
        config.R3Position = EditorGUILayout.Popup("R3Position", config.R3Position, config.positions);


        EditorGUILayout.LabelField("Button Setting", EditorStyles.boldLabel);
        config.L3Button = EditorGUILayout.Popup("L3Button", config.L3Button, config.buttons);
        config.L2Button = EditorGUILayout.Popup("L2Button", config.L2Button, config.buttons);
        config.L1Button = EditorGUILayout.Popup("L1Button", config.L1Button, config.buttons);


        config.R1Button = EditorGUILayout.Popup("R1Button", config.R1Button, config.buttons);
        config.R2Button = EditorGUILayout.Popup("R2Button", config.R2Button, config.buttons);
        config.R3Button = EditorGUILayout.Popup("R3Button", config.R3Button, config.buttons);
        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
