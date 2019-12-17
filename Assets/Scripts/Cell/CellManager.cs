using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages cells in the scene.
/// </summary>
public class CellManager : MonoBehaviour {
    private CellSettings cellSettings;
    private NoiseSettings noiseSettings;
    private Noise noise;

    public GameObject cells;

    /// <summary>
    /// Called whenever the script is loaded into the editor or values are changed.
    /// </summary>
    private void OnValidate() {
        Initialize();
    }

    /// <summary>
    /// Initializes the properties.
    /// </summary>
    private void Initialize() {
        GetComponents();
        noise = new Noise(noiseSettings);
    }

    /// <summary>
    /// Grabs necessary attached components to this game object.
    /// </summary>
    private void GetComponents() {
        cellSettings = this.gameObject.GetComponent<CellSettings>();
        noiseSettings = this.gameObject.GetComponent<NoiseSettings>();

        if (cellSettings == null || noiseSettings == null) {
            Debug.LogError("Some components not attached.", this);
        }
    }

    /// <summary>
    /// Creates a cell and generates densities for it at a specific location.
    /// </summary>
    /// <param name="position"></param>
    public void GenerateCell() {
        Cell cell = new Cell(this.transform.position, cellSettings, noise);
        cell.GenerateDensities();
        GameObject cellObject = cell.GenerateObject();
        cellObject.transform.position = this.transform.position;
        cellObject.transform.parent = cells.transform;
    }

    public void ClearCells() {
        List<Transform> childList = new List<Transform>();

        // Add all children to be destroyed to the list.
        foreach (Transform child in cells.transform) {
            childList.Add(child);
        }

        // Destroy everything in the list.
        foreach (Transform child in childList) {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }
}
