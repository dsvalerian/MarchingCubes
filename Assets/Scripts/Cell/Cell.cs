using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains variables and methods for terrain densities in a specific area in the scene.
/// </summary>
public class Cell {
    private Vector3 position;
    private CellSettings settings;
    private Noise noise;

    private Vector3[,,] points;
    private float[,,] densities;

    /// <summary>
    /// Constructor for the cell.
    /// </summary>
    /// <param name="position">The world position of the cell (left, bottom, back).</param>
    /// <param name="settings">Settings for the cell that should be applied to all cells.</param>
    public Cell(Vector3 position, CellSettings settings, Noise noise) {
        this.position = position;
        this.settings = settings;
        this.noise = noise;
        points = new Vector3[settings.resolution, settings.resolution, settings.resolution];
        densities = new float[settings.resolution, settings.resolution, settings.resolution];
    }

    /// <summary>
    /// Generates the densities of equidistant points in the cell using a noise function.
    /// </summary>
    public void GenerateDensities() {
        float stepSize = settings.size / settings.resolution;

        // Loop through all points to generate from the position.
        for (int x = 0; x < settings.resolution; x++) {
            for (int y = 0; y < settings.resolution; y++) {
                for (int z = 0; z < settings.resolution; z++) {
                    Vector3 worldPosition = new Vector3(x * stepSize + position.x,
                                                        y * stepSize + position.y,
                                                        z * stepSize + position.z);
                    //worldPosition = position;
                    points[x, y, z] = worldPosition;

                    // Calculate noise/density at this point.
                    float density = noise.Evaluate(worldPosition);
                    densities[x, y, z] = density;
                }
            }
        }
    }

    public GameObject GenerateObject() {
        GameObject cellParent = new GameObject("cell");
        cellParent.transform.position = Vector3.zero;

        GameObject cellObject = new GameObject("model");
        float offset = settings.size / 2;
        cellObject.transform.parent = cellParent.transform;
        //cellObject.transform.localPosition = new Vector3(offset, offset, offset);

        MeshRenderer renderer = (MeshRenderer)cellObject.AddComponent<MeshRenderer>();
        MeshFilter filter = (MeshFilter)cellObject.AddComponent<MeshFilter>();
        filter.sharedMesh = new CellMesh(densities, points).Generate();
        renderer.sharedMaterial = new Material(Shader.Find("Standard"));

        return cellParent;
    }
}
