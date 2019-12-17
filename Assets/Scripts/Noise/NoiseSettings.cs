using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic settings to be used when generating noise.
/// </summary>
public class NoiseSettings : MonoBehaviour {
    public float surfaceLevel = 0.5f;
    public float strength = 1.0f;
    public int layers = 1;
    public float roughness = 1.0f;
    public float roughnessGain = 2.0f;
    public float amplitudeGain = 0.5f;

    private void OnValidate() {
        CellManager manager = this.gameObject.GetComponent<CellManager>();
        manager.ClearCells();
        manager.GenerateCellAt(manager.cellPosition);
    }
}
