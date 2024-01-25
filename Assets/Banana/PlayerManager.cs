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

        mCanvas = transform.Find("Canvas").GetComponent<Canvas>();
        mScore = mCanvas.transform.Find("Score").GetComponent<TextMeshProUGUI>();

        mScore.text = 0.ToString();
    }

    private Canvas mCanvas;

    private TextMeshProUGUI mScore;
    private Image mHealth;
    private Sprite[] mHealthSprite = new Sprite[9];
    private bool isGameOver = false;

    public GameData mGameData;

    public void Load(GameData gameData)
    {
        mGameData = gameData;
    }

    public void UpdateHealth()
    {
        if(mGameData != null)
        {
            mHealth.sprite = mHealthSprite[mHealthSprite.Length - mGameData.Life];
        }
    }

    public void AddScore(int score)
    {
        if(mGameData != null)
        {
            mGameData.Score += score;
            mScore.text = mGameData.Score.ToString();
        }
    }

    public void Defeat()
    {
        if (mGameData == null)
            return;
        if (mGameData.Life > 1)
        {
            mGameData.Life--;
            UpdateHealth();
        }
        else
            isGameOver = true;
    }

    public void ResetData()
    {
        if (mGameData == null)
            return;
        isGameOver = false;
        mGameData.Life = 8;
        mGameData.Score = 0;
        UpdateHealth();
        mScore.text = mGameData.Score.ToString();
    }

    private void OnDestroy()
    {
        
    }
}
