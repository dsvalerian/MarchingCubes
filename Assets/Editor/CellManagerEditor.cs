using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CellManager))]
public class CellManagerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        // Custom properties
        CellManager cellManager = (CellManager)target;
        if (GUILayout.Button("Generate Cell")) {
            for (int x = 0; x < 6; x++) {
                for (int y = 0; y < 6; y++) {
                    cellManager.GenerateCellAt(new Vector3(x * 128, y * 128, 0f));
                }
            }
        }
    }
}
