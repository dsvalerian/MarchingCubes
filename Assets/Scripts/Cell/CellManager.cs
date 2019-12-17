using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages cells in the scene.
/// </summary>
public class CellManager : MonoBehaviour {
    public GameObject cells;
    public bool realtimeUpdate = true;

    private CellSettings cellSettings;
    private NoiseSettings noiseSettings;
    private NoiseFilter noise;
    private Vector3 previousPosition;

    /// <summary>
    /// Initialization when the script is loaded.
    /// </summary>
    private void Start() {
        GetComponents();
        noise = new NoiseFilter(noiseSettings);
        previousPosition = this.transform.position;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update() {
        // Check if position of cube has changed, only if realtimeUpdate is enabled.
        if (realtimeUpdate) {
            if (this.transform.position != previousPosition) {
                // Clear and generate a new cell at the position.
                ClearCells();
                GenerateCell();
            }
        }
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

    /// <summary>
    /// Get rid of all of the cells that are children of the "cells" object.
    /// </summary>
    public void ClearCells() {
        List<Transform> childList = new List<Transform>();

        // Add all children to be destroyed to the list.
        foreach (Transform child in cells.transform) {
            childList.Add(child);
        }

        // Destroy everything in the list.
        foreach (Transform child in childList) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
