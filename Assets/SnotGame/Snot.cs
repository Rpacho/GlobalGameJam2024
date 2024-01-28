using System;
using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Snot : MonoBehaviour
{
    public GameData gameData;
    public Timer timer;
    public bool start = false;
    public bool won = false;
    public float sniffspeed = 3.0f;
    private Vector3 scale;
    public AudioClip happyClip;
    public AudioClip sob;
    public List<AudioClip> cry;
    private void Start()
    {
        GlobalEvent.OnHit.AddListener(OnTimeHit);
        GlobalEvent.OnMiss.AddListener(OnTimeMiss);
        scale = Vector3.one;
        scale.y = 0.0f;
        PlayerManager.Instance.PlaySFX(cry[Random.Range(0,cry.Count)]);
    }

    private void OnTimeMiss()
    {
        PlayerManager.Instance?.StopSFX();
        PlayerManager.Instance?.PlaySFX(sob);
    }

    private void OnDestroy()
    {
        GlobalEvent.OnHit.RemoveListener(OnTimeHit);
        GlobalEvent.OnMiss.RemoveListener(OnTimeMiss);
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
        PlayerManager.Instance?.StopSFX();
        PlayerManager.Instance?.PlaySFX(happyClip);
        won = true;
    }
}
