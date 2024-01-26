using System;
using System.Collections;
using PoguScripts.Scriptable;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace PoguScripts.UI.ScoreAndHealth
{
    public class ScoreCountingText : MonoBehaviour
    {
        public GameData gameData;
        public TextMeshPro text;
        private bool isCounting = false;
        private int prevScore;
        private int differenceScore;
        private int currentScore;
        private int counter;
        private float timeElapse = 0;
        public float durationOfEachCount = 0.01f;
        IEnumerator Start()
        {
            prevScore = gameData.prevScore;
            counter = prevScore;
            differenceScore = gameData.Score - gameData.prevScore;
            currentScore = gameData.Score;
            text.text = counter.ToString();
            yield return new WaitForSeconds(0f);
            StartCounting();
        }

        private void StartCounting()
        {
            if(currentScore > prevScore)
                isCounting = true;
        }

        private void Update()
        {
            if (isCounting)
            {
                timeElapse += Time.deltaTime * gameData.GameSpeed;
                if (timeElapse < durationOfEachCount) return;
                    
                if (counter < currentScore)
                {
                    counter++;
                    timeElapse = 0f;
                    text.text = counter.ToString();
                }
                else
                {
                    isCounting = false;
                    gameData.prevScore = currentScore;
                }
            }
        }
    }
}