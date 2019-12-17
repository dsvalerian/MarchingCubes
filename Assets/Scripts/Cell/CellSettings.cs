using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic settings to be used for generating cells.
/// </summary>
public class CellSettings : MonoBehaviour {
    [Range(1f, 512f)]
    public float size = 32;
    [Range(1, 16)]
    public int resolution = 8;
}
