using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains variables and methods for terrain densities in a specific area in the scene.
/// </summary>
public class Cell {
    private Vector3 position;
    private CellSettings settings;
    private NoiseFilter noise;

    private Vector3[,,] worldPoints;
    private Vector3[,,] localPoints;
    private float[,,] densities;

    /// <summary>
    /// Constructor for the cell.
    /// </summary>
    /// <param name="position">The world position of the cell (left, bottom, back).</param>
    /// <param name="settings">Settings for the cell that should be applied to all cells.</param>
    public Cell(Vector3 position, CellSettings settings, NoiseFilter noise) {
        this.position = position;
        this.settings = settings;
        this.noise = noise;
        worldPoints = new Vector3[settings.resolution + 1, settings.resolution + 1, settings.resolution + 1];
        localPoints = new Vector3[settings.resolution + 1, settings.resolution + 1, settings.resolution + 1];
        densities = new float[settings.resolution + 1, settings.resolution + 1, settings.resolution + 1];
    }

    /// <summary>
    /// Generates the densities of equidistant points in the cell using a noise function.
    /// </summary>
    public void GenerateDensities() {
        float stepSize = settings.size / settings.resolution;

        // Loop through all points to generate from the position.
        for (int x = 0; x < settings.resolution + 1; x++) {
            for (int y = 0; y < settings.resolution + 1; y++) {
                for (int z = 0; z < settings.resolution + 1; z++) {
                    Vector3 localPosition = new Vector3(x * stepSize, y * stepSize, z * stepSize);
                    Vector3 worldPosition = new Vector3(x * stepSize + position.x,
                                                        y * stepSize + position.y,
                                                        z * stepSize + position.z);
                    
                    localPoints[x, y, z] = localPosition;
                    worldPoints[x, y, z] = worldPosition;

                    // Calculate noise/density at this point.
                    float density = noise.Evaluate(worldPosition);
                    densities[x, y, z] = density;
                }
            }
        }
    }

    /// <summary>
    /// Generates a game object with the appropriate mesh for this cell.
    /// </summary>
    /// <returns>A GameObject positioned at 0, 0, 0</returns>
    public GameObject GenerateObject() {
        // Create the overall cell object
        GameObject cellObject = new GameObject("cell");
        cellObject.transform.position = Vector3.zero;

        // Create the object holding the model and set it as a child of the cell object
        GameObject modelObject = new GameObject("model");
        modelObject.transform.parent = cellObject.transform;

        // Set up renderer/filter and generate the actual cell mesh and apply it to the model object
        MeshRenderer renderer = (MeshRenderer)modelObject.AddComponent<MeshRenderer>();
        MeshFilter filter = (MeshFilter)modelObject.AddComponent<MeshFilter>();
        filter.sharedMesh = new CellMesh(settings, densities, localPoints).Generate();
        renderer.sharedMaterial = new Material(Shader.Find("Standard"));

        return cellObject;
    }
}
