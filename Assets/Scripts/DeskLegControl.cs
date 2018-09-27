using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class to ensure that the legs of the table touch the floor
/// </summary>
public class DeskLegControl : MonoBehaviour {
    public GameObject floor;

    public void Start() {
        StretchLegsToTouchFloor();
    }


    /// <summary>
    /// Calculates the correct scale and position for the legs of the table
    /// </summary>
    private void StretchLegsToTouchFloor() {
        throw new NotImplementedException();
    }
}
