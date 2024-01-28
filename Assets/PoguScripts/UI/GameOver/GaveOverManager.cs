using System;
using PoguScripts.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PoguScripts.UI.GameOver
{
    public class GaveOverManager : MonoBehaviour
    {
        public GameData gameData;
        public ScoreData scoreData;
        public TextMeshProUGUI bestScore;
        public TextMeshProUGUI currentScore;
        public GameObject newBestScore;
        private bool isNewBestScore = false;


        private void Awake()
        {
            PlayerManager.Instance.BGMReset();
            isNewBestScore = gameData.Score > scoreData.bestScore;
            if(isNewBestScore)
                newBestScore.SetActive(true);
            scoreData.Push(gameData.Score);
            bestScore.text = scoreData.bestScore.ToString();
            currentScore.text = gameData.Score.ToString();
        }

        public void ReturnToTitle()
        {
            PlayerManager.Instance.ResetData();
            SceneManager.LoadScene("MainScene");
        }
    }
}