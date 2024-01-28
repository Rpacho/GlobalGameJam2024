using System;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts
{
    public class GameManager : MonoBehaviour
    {
        public GameData gameData;
        public string text;
        public float gameSpeedIncrease = 0.2f;
        public float multiplier = 1;
        private void Start()
        {
            foreach (var progress in gameData.gameProgressions)
            {
                if (progress.scoreLimit == gameData.Score && progress.isComplete == false)
                {
                    progress.isComplete = true;
                    text = progress.message;
                    SpeedUpTheGame();
                }
            }
        }

        private void SpeedUpTheGame()
        {
            gameData.GameSpeed = gameData.GameSpeed + gameSpeedIncrease * multiplier;
            PlayerManager.Instance.SetBGMSpeedUp();
        }
    }
    
}