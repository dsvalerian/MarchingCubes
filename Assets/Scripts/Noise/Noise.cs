using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for evaluating layered noise at any point in 3D space.
/// </summary>
public class Noise {
    private NoiseSettings settings;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="settings">The settings to be used for generating noise values.</param>
    public Noise(NoiseSettings settings) {
        this.settings = settings;
    }

    /// <summary>
    /// Calculates the layered noise value at a point in 3D space.
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="z">z coordinate</param>
    /// <returns></returns>
    public float Evaluate(float x, float y, float z) {
        x *= .01f;
        y *= .01f;
        z *= .01f;

        return Perlin3D(x, y, z);
       // return -y;
    }

    /// <summary>
    /// Calculates the layered noise value at a point in 3D space.
    /// </summary>
    /// <param name="position">The 3D coordinates</param>
    /// <returns></returns>
    public float Evaluate(Vector3 position) {
        return Evaluate(position.x, position.y, position.z);
    }

    /// <summary>
    /// Evaluates the scalar value of a 3D point where each coordinate is in [0, 1].
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="z">z coordinate</param>
    private float Perlin3D(float x, float y, float z) {
        float ab = Mathf.PerlinNoise(x, y);
        float bc = Mathf.PerlinNoise(y, z);
        float ac = Mathf.PerlinNoise(x, z);

        float ba = Mathf.PerlinNoise(y, x);
        float cb = Mathf.PerlinNoise(z, y);
        float ca = Mathf.PerlinNoise(z, x);

        float abc = ab + bc + ac + ba + cb + ca;
        return abc / 6f;
    }
}
