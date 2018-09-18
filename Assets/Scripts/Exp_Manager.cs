using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp_Manager : MonoBehaviour {

    // for the UI
    public Text HintText;
    public Text PlayerText;

    /* there are variables to store key time variables. The time for encoding onset/offset
     * as well as delay onset/offset etc. Don't forget to stream write these variables out as well */
    float t_encon;
    float t_encoff;
    float t_delon;
    float t_deloff;
    float t_teston;

    // guess what the beep does...
    AudioSource beep;
    
   
  
    void Start () {
        PlayerText.text = "";

        // on some systems - time is running like crazy - which is why we ensure that it is scaled normally
        Time.timeScale = 1f;
        beep = GameObject.Find("beep").GetComponent<AudioSource>();

        // currently the experimental manager relies on a one time start and then we walk through the trial loop
        StartCoroutine(DoTrials());
    }

    void Update () {

        /* some stuff for the UI so we can sleep well because we know touch was registered etc. - Could also
         * just debug log it... */
        
        if (cfg.touched){
            HintText.text = "touch registered!";
        } else if (cfg.in_start_pos)
        {
            HintText.text = "in start position!";
        } else
        {
            HintText.text = "";
        }
        
	}

    //----- HERE WE HAVE SOME SMALL FUNCTION SNIPPETS WHICH WE WILL USE IN THE EXPERIMENTAL 

    // to hide objects (during delay phase - so that we don't need to switch the scene)
    void HideObjects()
    {
        for (int i = 0; i < cfg.targets.Length; i++)
        {
            MeshRenderer render = GameObject.FindGameObjectWithTag(cfg.targets[i]).GetComponent<MeshRenderer>();
            render.enabled = false;
        }
    }
    // show them again
    void ShowObjects()
    {
        for (int i = 0; i < cfg.targets.Length; i++)
        {
            MeshRenderer render = GameObject.FindGameObjectWithTag(cfg.targets[i]).GetComponent<MeshRenderer>();
            render.enabled = true;
        }
    }

    // be able to hide a specific object - important for test phase where one object is missing
    void HideTarget(string target)
    {
        MeshRenderer render = GameObject.FindGameObjectWithTag(target).GetComponent<MeshRenderer>();
        render.enabled = false;
    }

    // show it again
    void ShowTarget(string target)
    {
        MeshRenderer render = GameObject.FindGameObjectWithTag(target).GetComponent<MeshRenderer>();
        render.enabled = true;
    }

    // for moving to encoding formation
    void MoveToPosA(int trial)
    {
        for (int i = 0; i < cfg.targets.Length; i++)
        {
            GameObject.FindGameObjectWithTag(cfg.targets[i]).transform.position = cfg.vecArraysA[i][trial];
        }
    }

    // for moving to testing formation
    void MoveToPosB(int trial)
    {
        for (int i = 0; i < cfg.targets.Length; i++)
        {
            GameObject.FindGameObjectWithTag(cfg.targets[i]).transform.position = cfg.vecArraysB[i][trial];
        }
    }



    IEnumerator DoTrials()
    {
        for (int i=0; i < cfg.num_trials; i++)   // go through every trial
        {

            ShowObjects();          // make sure to show previously hidden objects
            MoveToPosA(i);          // move them all to the next encoding position
            cfg.in_trial = true;    // mark trial begin
            
            /* during the inter trial intervall we show a blank screen (here: black). But of course
            we could also just use another masking camera with culling which we enable/disable */
            GameObject.Find("Canvas").transform.Find("BlackScreen").gameObject.SetActive(false);

            //----- ENCODING SCENE (self-paced)
                // subjects can decide how long they want and press space when ready
            Debug.Log("Trial number " + i.ToString());
            t_encon = Time.time * 1000;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            
            t_encoff = Time.time * 1000;
            Debug.Log("t_encon was: " + t_encon.ToString() + " and t_encoff was: " + t_encoff.ToString());

            //----- DELAY PHASE (1800ms)
            HideObjects(); // hide all of them
            t_delon = Time.time * 1000;
            yield return new WaitForSeconds(1.8f);
            t_deloff = Time.time * 1000;

            //----- TEST SCENE (1000ms)
            MoveToPosB(i);
            ShowObjects(); // show them again with one missing!
            HideTarget(cfg.targetList[i]); // here we hide the one missing - we access the target list
            Debug.Log(cfg.targetList[i].ToString());
            yield return new WaitForSeconds(1f);
            HideObjects();

            //----- TEST: REACHING
            beep.Play(0); // after the beep you are allowed to move
            t_teston = Time.time * 1000;
            yield return new WaitUntil(() => !cfg.in_trial && cfg.in_start_pos); 

            
            //----- END OF TRIAL
            PlayerText.text = "Press SPACE to start next trial";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            PlayerText.text = "";
            GameObject.Find("Canvas").transform.Find("BlackScreen").gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

        }
    }
}

