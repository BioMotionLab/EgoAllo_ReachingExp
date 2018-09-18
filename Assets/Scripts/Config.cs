using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This can be used as a global configuration file or even with parameters we
 * want to use globally. Yes static variables are a boo boo and super bad... */

public class cfg
{
    public const int num_trials = 7;
    public const int num_objects = 3;

    // bool family we used in the touch detector for example
    public static bool touched = false;
    public static bool in_start_pos = false;
    public static bool in_trial = false;

    public static List<Vector3[]> vecArraysA = new List<Vector3[]>();
    public static List<Vector3[]> vecArraysB = new List<Vector3[]>();

    // names of the targets - here we are really creative and call them 1, 2, 3
    public static string[] targets = new string[] {"1", "2", "3"};
    public static string[] targetList = new string[num_trials];


    // In a first vanilla version we shuffled the array with this. But we can delete this part.
    public static void randperm<Type>(Type[] theArray)
    {
        for (int i = 0; i < theArray.Length; i++)
        {
            int swappedIndex = i + Random.Range(0, theArray.Length - i);
            Type temp = theArray[i];
            theArray[i] = theArray[swappedIndex];
            theArray[swappedIndex] = temp;
        }
    }
}


public class Config : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}
}
