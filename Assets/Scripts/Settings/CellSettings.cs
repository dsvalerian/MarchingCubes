using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic settings to be used for generating cells.
/// </summary>
public class CellSettings : Settings {
    [Range(1f, 256f)]
    public float size = 64f;
    [Range(1, 32)]
    public int resolution = 16;
    [Range(0f, 1f)]
    public float surfaceLevel = 0.5f;

    // Cache previous values of settings.
    private float pSize;
    private int pResolution;
    private float pSurfaceLevel;

    /// <summary>
    /// Initialization when the script is loaded.
    /// </summary>
    private new void Start() {
        base.Start();
        UpdateCachedVariables();
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update() {
        if (VariablesChanged()) {
            OnVariableChange();
            UpdateCachedVariables();
        }
    }

    protected override bool VariablesChanged() {
        return pSize != size ||
                pResolution != resolution ||
                pSurfaceLevel != surfaceLevel;
    }

    protected override void UpdateCachedVariables() {
        pSize = size;
        pResolution = resolution;
        pSurfaceLevel = surfaceLevel;
    }
}
