using System;
using PoguScripts.Scriptable;
using TMPro;
using UnityEngine;

namespace PoguScripts
{
    public class GameManager : MonoBehaviour
    {
        public GameData gameData;
        public string text;
        public float gameSpeedIncrease = 0.2f;
        public float multiplier = 1;
        public TextMeshPro textUI;
        private void Start()
        {
            if (PlayerManager.Instance?.playerRounds == 0) textUI.text = "Controller: Spacebar ONLY! Get Ready!";
            if (PlayerManager.Instance?.playerRounds == 1) textUI.text = "Timing is the Key";
            if (PlayerManager.Instance?.playerRounds == 1 && gameData.Score == 100) textUI.text = "Wow! First Try";
            if (PlayerManager.Instance?.playerRounds == 2 && gameData.Score == 0 || gameData.Score == 100) textUI.text = "You can do this!!";
            if (PlayerManager.Instance?.playerRounds == 2 && gameData.Score == 200) textUI.text = "You're doing good!";
            if (PlayerManager.Instance?.playerRounds == 3 && gameData.Score == 300) textUI.text = "You're on fire!!!!";
            if (PlayerManager.Instance?.playerRounds == 4 && gameData.Score == 400) textUI.text = "Good Job!! -From Dev Team";
            if (PlayerManager.Instance?.playerRounds == 3 && gameData.Score == 0) textUI.text = "I know you can do this! Please one more time! yea?";
            foreach (var progress in gameData.gameProgressions)
            {
                if (progress.scoreLimit == gameData.Score && progress.isComplete == false)
                {
                    progress.isComplete = true;
                    textUI.text = progress.message;
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