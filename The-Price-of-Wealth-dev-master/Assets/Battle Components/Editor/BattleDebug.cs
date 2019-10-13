using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Battle))]
public class BattleDebug : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Battle myScript = (Battle)target;
        if(GUILayout.Button("Test Message"))
        {
            BattleStatic.instance.messageLog.SendMessage("SetMessage","Test");
        }
    }
}