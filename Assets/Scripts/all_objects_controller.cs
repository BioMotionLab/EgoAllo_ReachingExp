using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/* This script loads the arrangement file. The arrangement file should (at least) contain 6 position information for each object per trial.
 * Three Vector 3 information for encoding positions and 3 for test positions. */
 
public class all_objects_controller : MonoBehaviour
{

    // a for encoding phase | b for test phase | numbers represent objects
    public Vector3[] vecArray1a = new Vector3[cfg.num_trials];
    public Vector3[] vecArray1b = new Vector3[cfg.num_trials];
    public Vector3[] vecArray2a = new Vector3[cfg.num_trials];
    public Vector3[] vecArray2b = new Vector3[cfg.num_trials];
    public Vector3[] vecArray3a = new Vector3[cfg.num_trials];
    public Vector3[] vecArray3b = new Vector3[cfg.num_trials];

    string filepath = "Assets/Resources/arrangements.txt";

    void Awake()
    {

        StreamReader sr = new StreamReader(filepath);

        int i = 0;
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();

            string[] lines = line.Split('\t');

            float x1a = float.Parse(lines[0]);
            float y1a = float.Parse(lines[1]);
            float z1a = float.Parse(lines[2]);
            float x1b = float.Parse(lines[3]);
            float y1b = float.Parse(lines[4]);
            float z1b = float.Parse(lines[5]);

            float x2a = float.Parse(lines[6]);
            float y2a = float.Parse(lines[7]);
            float z2a = float.Parse(lines[8]);
            float x2b = float.Parse(lines[9]);
            float y2b = float.Parse(lines[10]);
            float z2b = float.Parse(lines[11]);

            float x3a = float.Parse(lines[12]);
            float y3a = float.Parse(lines[13]);
            float z3a = float.Parse(lines[14]);
            float x3b = float.Parse(lines[15]);
            float y3b = float.Parse(lines[16]);
            float z3b = float.Parse(lines[17]);

            string t = lines[18];

            vecArray1a[i] = new Vector3(x1a, y1a, z1a);
            vecArray1b[i] = new Vector3(x1b, y1b, z1b);
            vecArray2a[i] = new Vector3(x2a, y2a, z2a);
            vecArray2b[i] = new Vector3(x2b, y2b, z2b);
            vecArray3a[i] = new Vector3(x3a, y3a, z3a);
            vecArray3b[i] = new Vector3(x3b, y3b, z3b);

            /* The last column contains the target number, beacuse we need to know which
             * object we want to keep hidden in the test phase. This is also in the mother variable 
             * from the config file */
            cfg.targetList[i] = t;

            i = i + 1;

        }

        sr.Close();
        
        // In the config file we have the static mother variables.
        cfg.vecArraysA.Add(vecArray1a);
        cfg.vecArraysA.Add(vecArray2a);
        cfg.vecArraysA.Add(vecArray3a);

        cfg.vecArraysB.Add(vecArray1b);
        cfg.vecArraysB.Add(vecArray2b);
        cfg.vecArraysB.Add(vecArray3b);

    }

}
