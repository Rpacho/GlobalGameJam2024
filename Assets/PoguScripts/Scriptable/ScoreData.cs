using System.Collections.Generic;
using UnityEngine;

namespace PoguScripts.Scriptable
{
    [CreateAssetMenu(menuName = "Game ScoreData", fileName = "ScoreData")]
    public class ScoreData : ScriptableObject
    {
        public int bestScore = 0;
        public Queue<int> scoreList = new Queue<int>();

        public void Push(int score)
        {
            if (scoreList.Count > 10) scoreList.Dequeue();
            scoreList.Enqueue(score);
            foreach (var scoreItem in scoreList)
            {
                if (scoreItem > bestScore)
                {
                    bestScore = scoreItem;
                }
            }
        }
    }
}