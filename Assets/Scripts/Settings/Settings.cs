using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Settings : MonoBehaviour {
    private CellManager manager;

    /// <summary>
    /// Initialization when script is loaded.
    /// </summary>
    protected void Start() {
        manager = this.gameObject.GetComponent<CellManager>();
    }

    /// <summary>
    /// Is called whenever a variable is changed in the script.
    /// </summary>
    protected void OnVariableChange() {
        manager.ClearCells();
        manager.GenerateCell();
    }

    /// <summary>
    /// Checks if variables have been changed.
    /// </summary>
    /// <returns>True if variables have been changed, false otherwise</returns>
    protected abstract bool VariablesChanged();

    /// <summary>
    /// Updates the values of the cached variables.
    /// </summary>
    protected abstract void UpdateCachedVariables();
}
