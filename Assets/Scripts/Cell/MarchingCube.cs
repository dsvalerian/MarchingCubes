using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCube {
    public Vector3 position;
    public Vector3[] points;
    public float[] densities;

    public MarchingCube(Vector3Int indexInCell, Vector3[,,] cellPoints, float[,,] cellDensities) {
        points = CalculateCubePoints(indexInCell, cellPoints);
        densities = CalculateCubeDensities(indexInCell, cellDensities);
        position = points[0];
    }

    public Vector3 GetEdgeMidpoint(int edgeNumber) {
        Vector3[] edgePoints = GetEdgePoints(edgeNumber);

        if (edgePoints == null) {
            Debug.LogError("edgepoints are null");
        }
        return (edgePoints[0] + edgePoints[1]) / 2;
    }

    private Vector3[] CalculateCubePoints(Vector3Int index, Vector3[,,] cellPoints) {
        Vector3[] cubePoints = new Vector3[8];
        cubePoints[0] = cellPoints[index.x, index.y, index.z];
        cubePoints[1] = cellPoints[index.x, index.y + 1, index.z];
        cubePoints[2] = cellPoints[index.x + 1, index.y + 1, index.z];
        cubePoints[3] = cellPoints[index.x + 1, index.y, index.z];
        cubePoints[4] = cellPoints[index.x, index.y, index.z + 1];
        cubePoints[5] = cellPoints[index.x, index.y + 1, index.z + 1];
        cubePoints[6] = cellPoints[index.x + 1, index.y + 1, index.z + 1];
        cubePoints[7] = cellPoints[index.x + 1, index.y, index.z + 1];

        return cubePoints;
    }

    private float[] CalculateCubeDensities(Vector3Int index, float[,,] cellDensities) {
        float[] cubeDensities = new float[8];
        cubeDensities[0] = cellDensities[index.x, index.y, index.z];
        cubeDensities[1] = cellDensities[index.x, index.y + 1, index.z];
        cubeDensities[2] = cellDensities[index.x + 1, index.y + 1, index.z];
        cubeDensities[3] = cellDensities[index.x + 1, index.y, index.z];
        cubeDensities[4] = cellDensities[index.x, index.y, index.z + 1];
        cubeDensities[5] = cellDensities[index.x, index.y + 1, index.z + 1];
        cubeDensities[6] = cellDensities[index.x + 1, index.y + 1, index.z + 1];
        cubeDensities[7] = cellDensities[index.x + 1, index.y, index.z + 1];

        return cubeDensities;
    }

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
}