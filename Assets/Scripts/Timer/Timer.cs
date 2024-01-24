using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using PoguScripts.GlobalEvents;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static UnityEvent OnTimesUp = new UnityEvent(); //called once only when elapsedTime becomes bigger than timeLmit
    public GameData gameData;
    [SerializeField]
    public float timeLimit = 5; //Measured in seconds
    [SerializeField()]
    public bool pause { get; private set; }
    [SerializeField()]
    public bool timesUp { get; private set; }

    [SerializeField]
    private float elapsedTime;
    private void Start()
    {
        GlobalEvent.OnHit.AddListener(OnEventHit);
        GlobalEvent.OnMiss.AddListener(OnEventMiss);
    }

    private void Update()
    {
        if(!pause && elapsedTime < timeLimit)
        {
            elapsedTime += gameData.GameSpeed * Time.deltaTime;
            if(!timesUp && elapsedTime > timeLimit)
            {
                timesUp = true;
                OnTimesUp.Invoke();
                Pause();
                Debug.Log("[" + gameObject.name + "]" + "Times up!");
            }
        }
    }

    /// <summary>
    /// Returns the time in real life seconds before elapsed time reaches the time limit.
    /// </summary>
    /// <returns></returns>
    public float GetTimeRemaining()
    {
        if(gameData.GameSpeed <= 0.0f)
        {
            Debug.Log("[" + gameObject.name + "]" + "GameSpeed is smaller/equal to 0!");
            return 0.0f;
        }
        float timeRemaining = (timeLimit - elapsedTime) / gameData.GameSpeed;
        if(timeRemaining < 0.0f)
        {
            timeRemaining = 0.0f;
        }
        return timeRemaining;
    }

    /// <summary>
    /// Pause the timer.
    /// </summary>
    public void Pause()
    {
        pause = true;
        Debug.Log("[" + gameObject.name + "]" + "Paused!");
    }

    /// <summary>
    /// Unpause the timer.
    /// </summary>
    public void Unpause()
    {
        pause = false;
        Debug.Log("[" + gameObject.name + "]" + "Unpaused!");
    }

    /// <summary>
    /// Resets the timer so it can count again.
    /// </summary>
    public void ResetTimer()
    {
        elapsedTime = 0.0f;
        timesUp = false;
        Unpause();
    }

    public void OnEventHit()
    {
        Pause();
    }
    public void OnEventMiss()
    {
        Pause();
    }
}
