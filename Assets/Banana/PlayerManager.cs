using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager mInstance;
    public static PlayerManager Instance { get { return mInstance; } }

    private void Awake()
    {
        if (mInstance != null && mInstance != this)
            Destroy(gameObject);
        else
            mInstance = this;
        DontDestroyOnLoad(gameObject);
        GlobalEvent.OnChangedScore.AddListener(AddScore);
    }

    public int Score { get { return mGameData.Score; } }
    public int Life { get { return mGameData.Life; } }

    public GameData mGameData;

    public bool isGameWon { set; get; } = false;

    public void Load(GameData gameData)
    {
        mGameData = gameData;
    }

    public void AddScore(int score)
    {
        if(mGameData != null)
        {
            mGameData.Score += score;
            isGameWon = true;
        }
    }

    public void Defeat()
    {
        if (mGameData == null)
            return;
        if (mGameData.Life > 1)
            mGameData.Life--;
        isGameWon = false;
    }

    public void ResetData()
    {
        if (mGameData == null)
            return;
        mGameData.Life = 8;
        mGameData.Score = 0;
    }

    private void OnDestroy()
    {
        mGameData.Life = 8;
        mGameData.Score = 0;
    }
}
