using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour {

    /* We need to know where the finger (red ball) is. Moreover, in a correct trial,
     * subjects should start at the "starting position". The bool cfg.in_start_pos tells
     * us that by switching via a trigger capsule. We then need to know that the subject
     * is doing a reaching movement (exiting the capsule) and touches the table (triggering
     * table capsule - bool cfg.touched). These combinations allow us to control for situations
     * where subjects accidently touch the table move their hand back and forth etc. */

    float timeTouch;

    private void OnTriggerEnter(Collider other)
    {

        // If we touch the table...
        if (other.gameObject.name == "Table")
        {
            cfg.touched = true;                         // mark as touched
            timeTouch = Time.time * 1000;               // save time
            Debug.Log("time touched: " + timeTouch);    
            cfg.in_trial = false;                       // we're not in the beginning of the trial anymore
        }

        // If our hand is at starting position...
        if (other.gameObject.name == "StartCylinderTrigger")
        {
            cfg.in_start_pos = true;                    // mark as starting position
            Debug.Log("back to start");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // similarly we mark the ending of a table touch
        if (other.gameObject.name == "Table")
        {
            cfg.touched = false;                          
        }

        // and mark when we leave the starting position
        if (other.gameObject.name == "StartCylinderTrigger")
        {
            cfg.in_start_pos = false;
            Debug.Log("left start pos");
            cfg.in_trial = true;
        }
    }
  

}
