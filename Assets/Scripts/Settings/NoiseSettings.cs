using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic settings to be used when generating noise.
/// </summary>
public class NoiseSettings : Settings {
    [Range(0.0001f, 2f)]
    public float strength = 1f;
    public float scale = 1f;
    public int layers = 1;
    public float roughness = 1f;
    public float roughnessGain = 2f;
    public float amplitudeGain = 0.5f;

    // Cache previous values so we can tell when values have changed.
    private float pStrength;
    private float pScale;
    private int pLayers;
    private float pRoughness;
    private float pRoughnessGain;
    private float pAmplitudeGain;

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
        // Check if values have changed.
        if (VariablesChanged()) {
            OnVariableChange();
            UpdateCachedVariables();
        }
    }

    protected override bool VariablesChanged() {
        return pStrength != strength || 
                pScale != scale || 
                pLayers != layers || 
                pRoughness != roughness || 
                pRoughnessGain != roughnessGain || 
                pAmplitudeGain != amplitudeGain;
    }

    protected override void UpdateCachedVariables() {
        pStrength = strength;
        pScale = scale;
        pLayers = layers;
        pRoughness = roughness;
        pRoughnessGain = roughnessGain;
        pAmplitudeGain = amplitudeGain;
    }
}
