using System;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using PoguScripts.GlobalEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public UnityEvent OnTimesUp = new UnityEvent(); //called once only when elapsedTime becomes bigger than timeLmit
    public GameData gameData;
    [SerializeField]
    public float timeLimit = 5; //Measured in seconds
    [SerializeField()]
    public bool pause { get; private set; }
    [SerializeField()]
    public bool timesUp { get; private set; }

    public bool enable = true;

    public Sprite greenBarSprite;
    public Sprite yellowBarSprite;
    public Sprite redBarSprite;

    public GameObject fillObject;
    private RectTransform fillRect;
    private Image fillImage;
    private float yellowCapPerc = 0.3f;
    private float redCapPerc = 0.7f;
    private float yellowCapTimeStamp = 0f;
    private float redCapPercTimeStamp = 0f;

    private bool isGreen = true;
    private bool isYellow = false;
    private bool isRed = false;
    private float totalWidth;
    private float currentWidth;
    [SerializeField]
    private float elapsedTime;

    public bool enableBroadcast = true;
    private void Start()
    {
        if (enable == false) return;
        fillRect = fillObject.GetComponent<RectTransform>();
        totalWidth = fillRect.rect.width;
        currentWidth = totalWidth;
        fillImage = fillObject.GetComponent<Image>();

        yellowCapTimeStamp = timeLimit * yellowCapPerc;
        redCapPercTimeStamp = timeLimit * redCapPerc;
        
        GlobalEvent.OnHit.AddListener(OnEventHit);
        GlobalEvent.OnMiss.AddListener(OnEventMiss);
    }

    private void OnDestroy()
    {
        GlobalEvent.OnHit.RemoveListener(OnEventHit);
        GlobalEvent.OnMiss.RemoveListener(OnEventMiss);
    }

    private void Update()
    {
        float timerElapse = elapsedTime / timeLimit;
        
        currentWidth = totalWidth * timerElapse;
            
        fillRect.sizeDelta = new Vector2(totalWidth - currentWidth, fillRect.sizeDelta.y);
        
        if(!pause && elapsedTime < timeLimit)
        {
            elapsedTime += gameData.GameSpeed * Time.deltaTime;

            if (elapsedTime > yellowCapTimeStamp && isYellow == false)
            {
                isYellow = true;
                fillImage.sprite = yellowBarSprite;
            }
                
            if (elapsedTime > redCapPercTimeStamp && isRed == false)
            {
                isRed = true;
                fillImage.sprite = redBarSprite;
            }
            if(!timesUp && elapsedTime > timeLimit)
            {
                timesUp = true;
                OnTimesUp.Invoke();
                if (enableBroadcast)
                {
                    PlayerManager.Instance.Defeat();
                    PlayerManager.Instance.LoadResultSceneForce();
                }
                    
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

    public float GetElapsedTime()
    {
        return elapsedTime;
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
