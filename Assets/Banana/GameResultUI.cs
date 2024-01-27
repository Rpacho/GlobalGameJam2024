using PoguScripts.GlobalEvents;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultUI : MonoBehaviour
{
    public GameObject[] mHealthObjs = new GameObject[4];

    private TextMeshProUGUI mScoreBar;
    private TextMeshProUGUI mGameResultBar;

    private bool isGameOver = false;
    private bool isInitialized = false;

    private Timer mTimer;

    private void Start()
    {
        isGameOver = isInitialized = false;
        // mScoreBar = transform.Find("Panel").Find("Score").GetComponent<TextMeshProUGUI>();
        // mGameResultBar = transform.Find("Panel").Find("Result").GetComponent<TextMeshProUGUI>();
        mTimer = transform.Find("Timer").GetComponent<Timer>();

        UpdateHealth();
        UpdateScore();
        UpdateResult();

        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized)
            return;
        if(mTimer.GetTimeRemaining() == 0)
        {
            Result();
            isInitialized = false;
        }
    }

    private void UpdateHealth()
    {
        if (mHealthObjs == null || mHealthObjs.Length == 0 || PlayerManager.Instance == null)
            return;
        for (int i = 0; i < mHealthObjs.Length; i++)
        {
            if (mHealthObjs[i] == null)
            {
                Debug.LogError($"Error! {i} Health Sprite is empty!");
                return;
            }
        }

        for (int i = 0; i < mHealthObjs.Length; i++)
        {
            mHealthObjs[i].SetActive(false);
        }

        for (int i = 0; i < PlayerManager.Instance.Life; i++)
        {
            mHealthObjs[i].SetActive(true);
        }

        if(PlayerManager.Instance.Life <= 0)
            isGameOver = true;
    }

    private void UpdateScore()
    {
        if (PlayerManager.Instance == null)
            return;
        // mScoreBar.text = PlayerManager.Instance.Score.ToString();
    }

    private void UpdateResult()
    {
        if (PlayerManager.Instance == null)
            return;
        // mGameResultBar.text = (PlayerManager.Instance.isGameWon) ? "Win!" : (PlayerManager.Instance.Life == 0) ? "Game Over!" : "Lose!";
    }

    private void Result()
    {
        if (isInitialized == false)
            return;

        Debug.Log("Hello World");
        if(isGameOver)
        {
            // Go back
            //GlobalEvent.OnChangeGameStage.Invoke(PoguScripts.Enums.GameStage.NONE);
            //PlayerManager.Instance.ResetData();
        }
        else
        {
            // Go Next
            //GlobalEvent.OnChangeGameStage.Invoke(PoguScripts.Enums.GameStage.NONE);
        }
    }
}
