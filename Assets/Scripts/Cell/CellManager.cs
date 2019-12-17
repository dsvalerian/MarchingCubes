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

    public Vector3 cellPosition;

    /// <summary>
    /// Called whenever the script is loaded into the editor or values are changed.
    /// </summary>
    private void OnValidate() {
        Initialize();
        ClearCells();
        GenerateCellAt(cellPosition);
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

    public void ClearCells() {
        foreach (Transform child in this.transform) {
            child.transform.position = new Vector3(10000, 10000, 10000);
        }
    }

    /// <summary>
    /// Creates a cell and generates densities for it at a specific location.
    /// </summary>
    /// <param name="position"></param>
    public void GenerateCellAt(Vector3 position) {
        Cell cell = new Cell(position, cellSettings, noise);
        cell.GenerateDensities();
        GameObject cellObject = cell.GenerateObject();
        cellObject.transform.position = position;
        cellObject.transform.parent = this.transform;
    }
}
