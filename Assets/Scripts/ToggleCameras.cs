using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ToggleCameras : MonoBehaviour {

    // this script is to set choose between pictorial and visual spaces. Pressing "P" will change the scene ton pictorial space and pressing v will change the scene to visual space.
    Camera Cam;

    // A boolean to see whether we are in pictorial space or visual space. This is helpful for masking in the Exp_Manager script. 
    public bool Visual;

	void Start () {

        Cam = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("p"))
        {
            Cam.cullingMask =  ~(1 << 9);
            Visual = false;
        }
        else if(Input.GetKeyDown("v")){

            Visual = true;
            Cam.cullingMask |= (1 << 9);
            Cam.cullingMask = ~(1 << 8);
        }
		
	}
}
