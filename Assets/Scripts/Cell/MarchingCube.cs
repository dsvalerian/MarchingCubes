using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used as a helper class for the marching cubes algorithm. Represents one cube.
/// </summary>
public class MarchingCube {
    public Vector3[] points;
    public float[] densities;

    private Vector3Int indexInCell;
    private CellSettings settings;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexInCell">The position of the cube in terms of cell arrays</param>
    /// <param name="cellPoints">Local points/positions of the densities</param>
    /// <param name="cellDensities">The calculated densities for each point in the cell</param>
    public MarchingCube(CellSettings settings, Vector3Int indexInCell, Vector3[,,] cellPoints, 
            float[,,] cellDensities) {
        this.indexInCell = indexInCell;
        this.settings = settings;
        points = CalculateCubePoints(cellPoints);
        densities = CalculateCubeDensities(cellDensities);
    }

    /// <summary>
    /// Get the midpoint of an edge.
    /// </summary>
    /// <param name="edgeNumber">Which edge [0-11] of the cube to calculate for</param>
    /// <returns>The local position of the midpoint</returns>
    public Vector3 GetEdgeMidpoint(int edgeNumber) {
        Vector3[] edgePoints = GetEdgePoints(edgeNumber);
        float[] edgeDensities = GetEdgeDensities(edgeNumber);

        if (edgeDensities[0] < edgeDensities[1]) {
            float zeroDistance = (settings.surfaceLevel - edgeDensities[0]) / 
                (edgeDensities[0] - edgeDensities[1]);
            zeroDistance /= 100;

            return Vector3.Lerp(edgePoints[0], edgePoints[1], zeroDistance);
        }
        else {
            float zeroDistance = (settings.surfaceLevel - edgeDensities[1]) / 
                (edgeDensities[1] - edgeDensities[0]);
            zeroDistance /= 100;

            return Vector3.Lerp(edgePoints[1], edgePoints[0], zeroDistance);
        }
        
        
        //Debug.Log(zeroDistance);

        //return (edgePoints[0] + edgePoints[1]) / 2;
        
    }

    /// <summary>
    /// Calculate which points in the cell are used as vertices of this cube.
    /// </summary>
    /// <param name="cellPoints">All of the local positions of the cell points</param>
    /// <returns>The local positions of the cube vertices</returns>
    private Vector3[] CalculateCubePoints(Vector3[,,] cellPoints) {
        Vector3[] cubePoints = new Vector3[8];
        cubePoints[0] = cellPoints[indexInCell.x, indexInCell.y, indexInCell.z];
        cubePoints[1] = cellPoints[indexInCell.x, indexInCell.y + 1, indexInCell.z];
        cubePoints[2] = cellPoints[indexInCell.x + 1, indexInCell.y + 1, indexInCell.z];
        cubePoints[3] = cellPoints[indexInCell.x + 1, indexInCell.y, indexInCell.z];
        cubePoints[4] = cellPoints[indexInCell.x, indexInCell.y, indexInCell.z + 1];
        cubePoints[5] = cellPoints[indexInCell.x, indexInCell.y + 1, indexInCell.z + 1];
        cubePoints[6] = cellPoints[indexInCell.x + 1, indexInCell.y + 1, indexInCell.z + 1];
        cubePoints[7] = cellPoints[indexInCell.x + 1, indexInCell.y, indexInCell.z + 1];

        return cubePoints;
    }

    /// <summary>
    /// Calculate which densities in the cell are used as vertex densities of this cube.
    /// </summary>
    /// <param name="cellDensities">All of the densities associated with points in the cell</param>
    /// <returns>The densities associated with the cube vertices</returns>
    private float[] CalculateCubeDensities(float[,,] cellDensities) {
        float[] cubeDensities = new float[8];
        cubeDensities[0] = cellDensities[indexInCell.x, indexInCell.y, indexInCell.z];
        cubeDensities[1] = cellDensities[indexInCell.x, indexInCell.y + 1, indexInCell.z];
        cubeDensities[2] = cellDensities[indexInCell.x + 1, indexInCell.y + 1, indexInCell.z];
        cubeDensities[3] = cellDensities[indexInCell.x + 1, indexInCell.y, indexInCell.z];
        cubeDensities[4] = cellDensities[indexInCell.x, indexInCell.y, indexInCell.z + 1];
        cubeDensities[5] = cellDensities[indexInCell.x, indexInCell.y + 1, indexInCell.z + 1];
        cubeDensities[6] = cellDensities[indexInCell.x + 1, indexInCell.y + 1, indexInCell.z + 1];
        cubeDensities[7] = cellDensities[indexInCell.x + 1, indexInCell.y, indexInCell.z + 1];

        return cubeDensities;
    }

    /// <summary>
    /// Get the endpoint vertices of an edge.
    /// </summary>
    /// <param name="edgeNumber">The number of the edge in the cube to use</param>
    /// <returns>Array of 2 vertex positions</returns>
    private Vector3[] GetEdgePoints(int edgeNumber) {
        switch(edgeNumber) {
            case 0:
                return new Vector3[] {points[0], points[1]};
            case 1:
                return new Vector3[] {points[1], points[2]};
            case 2:
                return new Vector3[] {points[2], points[3]};
            case 3:
                return new Vector3[] {points[3], points[0]};
            case 4:
                return new Vector3[] {points[4], points[5]};
            case 5:
                return new Vector3[] {points[5], points[6]};
            case 6:
                return new Vector3[] {points[6], points[7]};
            case 7:
                return new Vector3[] {points[7], points[4]};
            case 8:
                return new Vector3[] {points[0], points[4]};
            case 9:
                return new Vector3[] {points[1], points[5]};
            case 10:
                return new Vector3[] {points[2], points[6]};
            case 11:
                return new Vector3[] {points[3], points[7]};
            default:
                Debug.LogError(edgeNumber);
                return null;
        }
    }

    /// <summary>
    /// Get the densities of the endpoints of an edge.
    /// </summary>
    /// <param name="edgeNumber">The number of the edge in the cube to use</param>
    /// <returns>Array of 2 densities</returns>
    private float[] GetEdgeDensities(int edgeNumber) {
        switch(edgeNumber) {
            case 0:
                return new float[] {densities[0], densities[1]};
            case 1:
                return new float[] {densities[1], densities[2]};
            case 2:
                return new float[] {densities[2], densities[3]};
            case 3:
                return new float[] {densities[3], densities[0]};
            case 4:
                return new float[] {densities[4], densities[5]};
            case 5:
                return new float[] {densities[5], densities[6]};
            case 6:
                return new float[] {densities[6], densities[7]};
            case 7:
                return new float[] {densities[7], densities[4]};
            case 8:
                return new float[] {densities[0], densities[4]};
            case 9:
                return new float[] {densities[1], densities[5]};
            case 10:
                return new float[] {densities[2], densities[6]};
            case 11:
                return new float[] {densities[3], densities[7]};
            default:
                Debug.LogError(edgeNumber);
                return null;
        }
    }
}