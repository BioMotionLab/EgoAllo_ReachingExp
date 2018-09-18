using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* There is no motion tracking here in Gießen compatible with Unity (it will change mid October though...). 
 * Therefore we controll the height of the "finger" here with the arrow keys (up down) and the forward -
 * backward movement of the finger (again...the red ball) via mouse. It is not supposed to be realistic and 
 * should only be used for debugging.
 * To start the experiment, lift the red ball first (arrow up) and drag it. You can start a trial with the space key. */

public class mouseDrag : MonoBehaviour {

    public float speed;

   void OnMouseDrag()
    {

        float h = speed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float v = speed * Input.GetAxis("Mouse Y") * Time.deltaTime;

        Vector3 objectPos = new Vector3(h, 0, v);

        transform.Translate(objectPos);
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
           
            transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);

        }
    }

}
