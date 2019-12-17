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
/*
        if (GUILayout.Button("Generate Cube of Cells")) {
            // Loop through x y z
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    for (int z = 0; z < 4; z++) {
                        cellManager.transform.position = new Vector3(256 * x, 256 * y, 256 * z);
                        cellManager.GenerateCell();
                    }
                }
            }
        }
*/
        if (GUILayout.Button("Clear Cells")) {
            cellManager.ClearCells();
        }
    }
}
