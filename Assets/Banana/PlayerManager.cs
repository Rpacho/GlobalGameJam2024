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
    public AudioSource bgm;
    public int playerRounds = 0;
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
        playerRounds++;
        if(mGameData != null)
        {
            mGameData.Score += score;
            isGameWon = true;
        }
    }

    public void Defeat()
    {
        playerRounds++;
        if (mGameData == null)
            return;
        if (mGameData.Life >= 1)
            mGameData.Life--;
        isGameWon = false;
    }

    public void ResetData()
    {
        BGMReset();
        if (mGameData == null)
            return;
        mGameData.Life = 3;
        mGameData.prevLifePoints = 3;
        mGameData.prevScore = 0;
        mGameData.Score = 0;
        mGameData.CurrentGameStage = GameStage.NONE;
        mGameData.GameSpeed = 1f;
        foreach (var progression in mGameData.gameProgressions)
        {
            progression.Reset();
        }

        playerRounds = 0;
    }

    public void BGMReset()
    {
        bgm.pitch = 1f;
    }

    private void GainScoreEachRound()
    {
        AddScore(100);
    }



    public void LoadResultScene()
    {
        StartCoroutine(WaitForSec());
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1.5f);
        LoadResultSceneForce();
    }

    public void LoadResultSceneForce()
    {
        StopSFX();
        SceneManager.LoadScene("Result");
    }

    private void DecrementLife()
    {
        Defeat();
    }

    public void SetBGMSpeedUp()
    {
        bgm.pitch += 0.1f;
    }

    private float musicVolumeWhenStartGame = 0.65f;
    public void SetVolumeDownMusic()
    {
        // float minus = (float)(mGameData.Volume * musicVolumeWhenStartGame);
        // float volume = mGameData.Volume - minus;
        // _audioSfx._audioMixer.SetFloat("Music", Mathf.Log10((volume)) * 20);
    }

    public void SetVolumeUpMusic()
    {
        // float minus = (float)(mGameData.Volume * musicVolumeWhenStartGame);
        // float volume = mGameData.Volume + minus;
        // _audioSfx._audioMixer.SetFloat("Music", Mathf.Log10((volume)) * 20);
    }
    
    
}
