using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CellManager))]
public class CellManagerEditor : Editor {
    /// <summary>
    /// Override what is visible on the inspector window for this script.
    /// </summary>
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        // Custom properties
        CellManager cellManager = (CellManager)target;

        if (GUILayout.Button("Generate Cell")) {
            cellManager.GenerateCell();
        }

        if (GUILayout.Button("Clear Cells")) {
            cellManager.ClearCells();
        }
    }
}
