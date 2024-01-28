using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snot : MonoBehaviour
{
    public GameData gameData;
    public Timer timer;
    public bool start = false;
    public bool won = false;
    public float sniffspeed = 3.0f;
    private Vector3 scale;
    private void Start()
    {
        GlobalEvent.OnHit.AddListener(OnTimeHit);
        scale = Vector3.one;
        scale.y = 0.0f;
    }
    public void FixedUpdate()
    {
        if(start && !won)
        {
            if (timer.GetTimeRemaining() > 0.0f)
            {
                scale.y = timer.GetElapsedTime() / timer.timeLimit;
                transform.localScale = scale;
            }
        }
        else if(won)
        {
            //Debug.Log("[Snot] Won!");
            if(scale.y > 0.0f)
            {
                scale.y -= gameData.GameSpeed * sniffspeed * Time.deltaTime;
                transform.localScale = scale;
            }
        }
    }

    public void OnTimeHit()
    {
        won = true;
    }
}
