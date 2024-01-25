using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooldownTimer : MonoBehaviour
{
    public UnityEvent OnReady = new UnityEvent(); //called once only when elapsedTime becomes bigger than timeLmit
    [SerializeField]
    public float timeLimit = 5; //Measured in seconds
    [SerializeField()]
    public bool pause { get; private set; }
    [SerializeField()]
    public bool timesUp { get; private set; }

    [SerializeField]
    private float elapsedTime;
    public bool cooldownReady = true;

    private void Update()
    {
        if (!pause && elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            if (!cooldownReady && elapsedTime > timeLimit)
            {
                cooldownReady = true;
                OnReady.Invoke();
                Pause();
                Debug.Log("[" + gameObject.name + "]" + "Cooldown Ready!");
            }
        }
    }

    /// <summary>
    /// Returns the time in real life seconds before elapsed time reaches the time limit.
    /// </summary>
    /// <returns></returns>
    public float GetTimeRemaining()
    {
        float timeRemaining = (timeLimit - elapsedTime);
        if (timeRemaining < 0.0f)
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
    /// Starts the cooldown.
    /// </summary>
    public void ResetTimer()
    {
        elapsedTime = 0.0f;
        cooldownReady = false;
        Unpause();
    }
}
