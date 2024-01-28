using PoguScripts.GlobalEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnotGameManager : MonoBehaviour
{
    public Snot snot;
    public Timer timer;
    public GameObject timingBar;
    public float startDelay = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEvent.OnMiss.AddListener(OnTimeMiss);
        timer.Pause();
        StartCoroutine(HaltForSeconds(startDelay));
    }

    public void OnTimeMiss()
    {
        timingBar.SetActive(false);
    }

    IEnumerator HaltForSeconds(float seconds)
    {
        Debug.Log("Pause Game for a few seconds.");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Game start!");
        snot.start = true;
        timer.Unpause();
        timingBar.SetActive(true);

    }
}
