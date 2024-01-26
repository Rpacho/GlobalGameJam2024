using System;
using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using PoguScripts.Audion;
using PoguScripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager mInstance;
    private AudioSFX _audioSfx;
    public static PlayerManager Instance { get { return mInstance; } }
    private void Awake()
    {
        _audioSfx = GetComponent<AudioSFX>();
        ResetData();
        if (mInstance != null && mInstance != this)
            Destroy(gameObject);
        else
            mInstance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void StopSFX() => _audioSfx.Stop();
    public void PlaySFX(AudioClip clip, float delay = 0)
    {
        _audioSfx.PlayAudioClip(clip, delay);
    }
    private void OnEnable()
    {
        GlobalEvent.OnHit.AddListener(delegate { GainScoreEachRound();
            LoadResultScene();
        });
        GlobalEvent.OnMiss.AddListener(delegate { DecrementLife();
            LoadResultScene();
        });
    }

    private void OnDisable()
    {
        GlobalEvent.OnHit.RemoveListener(delegate { GainScoreEachRound();
            LoadResultScene();
        });
        GlobalEvent.OnMiss.RemoveListener(delegate { DecrementLife();
            LoadResultScene();
        });
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
        if (mGameData.Life >= 1)
            mGameData.Life--;
        isGameWon = false;
    }

    public void ResetData()
    {
        if (mGameData == null)
            return;
        mGameData.Life = 3;
        mGameData.prevLifePoints = 3;
        mGameData.prevScore = 0;
        mGameData.Score = 0;
        mGameData.CurrentGameStage = GameStage.NONE;
    }

    private void GainScoreEachRound()
    {
        AddScore(100);
    }

    private void OnDestroy()
    {

    }

    public void LoadResultScene()
    {
        StartCoroutine(WaitForSec());
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1.5f);
        StopSFX();
        SceneManager.LoadScene("Result");
    }
    
    private void DecrementLife()
    {
        Defeat();
    }
}
