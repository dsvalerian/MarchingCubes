using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a mesh from volumetric data using marching cubes.
/// </summary>
public class CellMesh {
    private CellSettings settings;
    private float[,,] densities;
    private Vector3[,,] points;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="settings">The settings used for the Cell</param>
    /// <param name="densities">The calculated densities from the cell</param>
    /// <param name="points">The local points/positions of the densities</param>
    public CellMesh(CellSettings settings, float[,,] densities, Vector3[,,] points) {
        this.settings = settings;
        this.densities = densities;
        this.points = points;
    }

    /// <summary>
    /// Generate a 3D mesh for the densities using a marching cubes algorithm.
    /// </summary>
    /// <returns>A Mesh object</returns>
    public Mesh Generate() {
        List<Vector3> vertices = new List<Vector3>();

        // Loop through the volumetric data.
        for (int xIndex = 0; xIndex < densities.GetLength(0) - 1; xIndex++) {
            for (int yIndex = 0; yIndex < densities.GetLength(1) - 1; yIndex++) {
                for (int zIndex = 0; zIndex < densities.GetLength(2) - 1; zIndex++) {
                    // Create a marching cube for this position.
                    Vector3Int cubeIndex = new Vector3Int(xIndex, yIndex, zIndex);
                    MarchingCube cube = new MarchingCube(cubeIndex, points, densities);

                    // Determine which triangles to draw for this cube.
                    int cubeCase = ConcatenateIntoByte(GetActiveDensityBitArray(cube.densities));
                    int[] connectedEdges = Tables.triangles[cubeCase];
                    Vector3[] cubeTriangleVertices = GetTriangleVertices(cube, connectedEdges);

                    // Add the triangle vertices for this cube to the list of all triangle vertices
                    for (int i = 0; i < cubeTriangleVertices.Length; i++) {
                        vertices.Add(cubeTriangleVertices[i]);
                    }
                }
            }
        }

        // Add all of the triangle indeces, which should simply be [0, numVertices] because we are 
        // not reusing any vertices between triangles.
        List<int> triangles = new List<int>();
        for (int i = 0; i < vertices.Count; i++) {
            triangles.Add(vertices.Count - 1 - i);
        }

        // Create the mesh itself and assign all the data.
        Mesh cellMesh = new Mesh();
        cellMesh.vertices = vertices.ToArray();
        cellMesh.triangles = triangles.ToArray();
        cellMesh.RecalculateNormals();

        return cellMesh;
    }

    /// <summary>
    /// Get the vertices of the triangles to be drawn.
    /// Each triangle is drawn between 3 points, each of which are on an edge in the marching cube.
    /// </summary>
    /// <param name="cube">The marching cube for which we are calculating triangles.</param>
    /// <param name="edgeNumbers">The numbers of the edges to be connected. Every 3 edges is
    ///     one triangle.</param>
    /// <returns>The coordinates of every triangle vertex. Every 3 is one triangle.</returns>
    private Vector3[] GetTriangleVertices(MarchingCube cube, int[] edgeNumbers) {
        List<Vector3> triangleVertices = new List<Vector3>();

        // Loop through each triangle
        for (int i = 0; i < edgeNumbers.Length; i++) {
            if (edgeNumbers[i] != -1) {
                triangleVertices.Add(cube.GetEdgeMidpoint(edgeNumbers[i]));
            }
        }

        return triangleVertices.ToArray();
    }

    /// <summary>
    /// Convert an array of int bits into a byte.
    /// Example: [0, 0, 0, 0, 1, 0, 1, 1] = 0b00001011 = 11
    /// </summary>
    /// <param name="bitValues">The 0/1 bits</param>
    /// <returns>An integer that the binary array represents</returns>
    private int ConcatenateIntoByte(int[] bitValues) {
        int result = 0;

        for (int i = bitValues.Length - 1; i >= 0; i--) {
            // Left-shift the byte by 1 and add the activity of the value to the byte
            int bit = IsActive(bitValues[i]);
            result <<= 1;
            result += bit;
        }

        return result;
    }

    /// <summary>
    /// Get an array of active density bits. If a density is active, the bit is 1. 0 otherwise.
    /// </summary>
    /// <param name="densityValues">The densities to check for activity</param>
    /// <returns>Array of 0/1 bits</returns>
    private int[] GetActiveDensityBitArray(float[] densityValues) {
        int[] bitArray = new int[densityValues.Length];
        for (int i = 0; i < densityValues.Length; i++) {
            bitArray[i] = IsActive(densityValues[i]);
        }

        return bitArray;
    }

    /// <summary>
    /// Determines whether a density "activates" its associated voxel point or not.
    /// </summary>
    /// <param name="densityValue">The density of the voxel point</param>
    /// <returns>1 if the density is enough to activate, 0 otherwise</returns>
    private int IsActive(float densityValue) {
        if (densityValue > settings.surfaceLevel) {
            return 1;
        }

        return 0;
    }
}
